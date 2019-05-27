
namespace DTDanmakuLib
{
	using DTLib;
	using System;
	using System.Collections.Generic;

	public class EnemyObjectUpdater
	{
		public class UpdateResult
		{
			public List<EnemyObject> NewEnemyObjects;
			public List<Tuple<long, long>> NewPowerUps;
			public bool ShouldEndLevel;
			public List<string> NewSoundEffectsToPlay;
			public long? BossHealthMeterNumber;
			public long? BossHealthMeterMilliPercentage;
		}

		public static UpdateResult Update(
				List<EnemyObject> currentEnemyObjects,
				long playerXMillis,
				long playerYMillis,
				long elapsedMillisecondsPerIteration,
				bool isPlayerDestroyed,
				Dictionary<string, EnemyObjectTemplate> enemyObjectTemplates,
				IDTDeterministicRandom rng)
        {			
			var actionResult = new ResultOfAction();
			
			List<EnemyObject> finalEnemyList = new List<EnemyObject>();
			List<EnemyObject> currentEnemyListToEnumerate = currentEnemyObjects;
			
			while (true)
			{
				int currentEnemyListToEnumerateCount = currentEnemyListToEnumerate.Count;
				for (int i = 0; i < currentEnemyListToEnumerateCount; i++)
				{
					var enemyObject = currentEnemyListToEnumerate[i];
					
					if (enemyObject.IsDestroyed)
						continue;

					ObjectAction newObjectAction = HandleAction(
						action: enemyObject.Action,
						obj: enemyObject,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						isPlayerDestroyed: isPlayerDestroyed,
						enemyObjectTemplates: enemyObjectTemplates,
						rng: rng,
						actionResult: actionResult);

					if (newObjectAction != null)
						enemyObject.Action = newObjectAction;

					if (!enemyObject.IsDestroyed)
					{
						finalEnemyList.Add(enemyObject);
					}
				}
				
				if (actionResult.NewEnemyObjects != null && actionResult.NewEnemyObjects.Count > 0)
				{
					currentEnemyListToEnumerate = actionResult.NewEnemyObjects;
					actionResult.NewEnemyObjects = null;
				}
				else
					break;
			}

			List<EnemyObject> finalEnemyListThatAreNotDead = new List<EnemyObject>();
			int length = finalEnemyList.Count;
			for (int i = 0; i < length; i++)
			{
				EnemyObject obj = finalEnemyList[i];
				if (!obj.IsDestroyed)
				{
					finalEnemyListThatAreNotDead.Add(obj);
				}
			}

			return new UpdateResult
			{
				NewEnemyObjects = finalEnemyListThatAreNotDead,
				NewPowerUps = actionResult.NewPowerUps != null ? actionResult.NewPowerUps : new List<Tuple<long, long>>(),
				ShouldEndLevel = actionResult.ShouldEndLevel,
				NewSoundEffectsToPlay = actionResult.NewSoundEffectsToPlay != null ? actionResult.NewSoundEffectsToPlay : new List<string>(),
				BossHealthMeterNumber = actionResult.BossHealthMeterNumber,
				BossHealthMeterMilliPercentage = actionResult.BossHealthMeterMilliPercentage
			};
        }

		private class ResultOfAction
		{
			public ResultOfAction()
			{
				this.ShouldEndLevel = false;
				this.NewEnemyObjects = null;
				this.NewPowerUps = null;
				this.NewSoundEffectsToPlay = null;
				this.BossHealthMeterNumber = null;
				this.BossHealthMeterMilliPercentage = null;
			}

			public bool ShouldEndLevel;
			public List<EnemyObject> NewEnemyObjects;
			public List<Tuple<long, long>> NewPowerUps;
			public List<string> NewSoundEffectsToPlay;
			public long? BossHealthMeterNumber;
			public long? BossHealthMeterMilliPercentage;
		}

		private static ObjectAction HandleAction(
			ObjectAction action,
			EnemyObject obj,
			long playerXMillis,
			long playerYMillis,
			long elapsedMillisecondsPerIteration,
			bool isPlayerDestroyed,
			Dictionary<string, EnemyObjectTemplate> enemyObjectTemplates,
			IDTDeterministicRandom rng,
			ResultOfAction actionResult)
		{				
			bool? isParentDestroyed;
			if (obj.ParentObject == null)
				isParentDestroyed = null;
			else
				isParentDestroyed = obj.ParentObject.IsDestroyed;

			switch (action.ObjectActionType)
			{
				case ObjectAction.Type.Move:
				case ObjectAction.Type.StrafeMove:
					long desiredX = action.MoveToXMillis.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
					long desiredY = action.MoveToYMillis.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
					long? directionInMillidegrees = DTDanmakuMath.GetMovementDirectionInMillidegrees(currentX: obj.XMillis, currentY: obj.YMillis, desiredX: desiredX, desiredY: desiredY);

					if (directionInMillidegrees != null)
					{
						obj.MovementDirectionInMillidegrees = directionInMillidegrees.Value;
						if (action.ObjectActionType == ObjectAction.Type.Move)
							obj.FacingDirectionInMillidegrees = directionInMillidegrees.Value;
						else if (action.ObjectActionType == ObjectAction.Type.StrafeMove)
							obj.FacingDirectionInMillidegrees = 180L * 1000L;
						else
							throw new Exception();
					}

					return null;
					
				case ObjectAction.Type.SetSpeed:
				case ObjectAction.Type.IncreaseSpeed:
				case ObjectAction.Type.DecreaseSpeed:
					long speed = action.SpeedInMillipixelsPerMillisecond.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					if (action.ObjectActionType == ObjectAction.Type.SetSpeed)
						obj.SpeedInMillipixelsPerMillisecond = speed;
					else if (action.ObjectActionType == ObjectAction.Type.IncreaseSpeed)
						obj.SpeedInMillipixelsPerMillisecond += speed;
					else if (action.ObjectActionType == ObjectAction.Type.DecreaseSpeed)
						obj.SpeedInMillipixelsPerMillisecond -= speed;
					else
						throw new Exception();

					if (obj.SpeedInMillipixelsPerMillisecond < 0)
						obj.SpeedInMillipixelsPerMillisecond = 0;

					return null;

				case ObjectAction.Type.SetPosition:
					long newXMillisPosition = action.SetXMillisPosition.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
					long newYMillisPosition = action.SetYMillisPosition.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
					obj.XMillis = newXMillisPosition;
					obj.YMillis = newYMillisPosition;
				
				return null;

				case ObjectAction.Type.SetFacingDirection:

					long newFacingDirection = action.SetFacingDirectionInMillidegrees.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					obj.FacingDirectionInMillidegrees = newFacingDirection;

					return null;

				case ObjectAction.Type.Destroy:
					obj.IsDestroyed = true;

					return null;

				case ObjectAction.Type.DestroyParent:
					if (obj.ParentObject == null)
						throw new Exception();

					obj.ParentObject.IsDestroyed = true;

					return null;

				case ObjectAction.Type.SpawnChild:
					long childXMillis = action.SpawnChildXMillis.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
					long childYMillis = action.SpawnChildYMillis.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					EnemyObjectTemplate childObjectTemplate = enemyObjectTemplates[action.SpawnChildObjectTemplateName];

					var childObjectNumericVariables = new Dictionary<string, IMathExpression>();
					var childObjectBooleanVariables = new Dictionary<string, BooleanExpression>();

					if (action.SpawnChildInitialChildNumericVariables != null)
					{
						for (int i = 0; i < action.SpawnChildInitialChildNumericVariables.Count; i++)
							childObjectNumericVariables.Add(action.SpawnChildInitialChildNumericVariables[i].Name, action.SpawnChildInitialChildNumericVariables[i].Value);
					}
					if (action.SpawnChildInitialChildBooleanVariables != null)
					{
						for (int i = 0; i < action.SpawnChildInitialChildBooleanVariables.Count; i++)
							childObjectBooleanVariables.Add(action.SpawnChildInitialChildBooleanVariables[i].Name, action.SpawnChildInitialChildBooleanVariables[i].Value);
					}

					var newEnemyObject = new EnemyObject(
						template: childObjectTemplate,
						initialXMillis: childXMillis,
						initialYMillis: childYMillis,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						isPlayerDestroyed: isPlayerDestroyed,
						parent: obj,
						initialNumericVariables: childObjectNumericVariables,
						initialBooleanVariables: childObjectBooleanVariables,
						rng: rng);

					if (actionResult.NewEnemyObjects == null)
						actionResult.NewEnemyObjects = new List<EnemyObject>();
					
					actionResult.NewEnemyObjects.Add(newEnemyObject);
						
					return null;

				case ObjectAction.Type.SpawnPowerUp:

					if (actionResult.NewPowerUps == null)
						actionResult.NewPowerUps = new List<Tuple<long, long>>();
					
					actionResult.NewPowerUps.Add(new Tuple<long, long>(obj.XMillis, obj.YMillis));
					
					return null;
					
				case ObjectAction.Type.SetNumericVariable:
					
					obj.NumericVariables[action.SetVariableName] = action.SetNumericVariableValue.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
						
					return null;

				case ObjectAction.Type.SetBooleanVariable:
					obj.BooleanVariables[action.SetVariableName] = action.SetBooleanVariableValue.Evaluate(
						obj.GetEnemyObjectExpressionInfo(),
						isParentDestroyed: isParentDestroyed,
						isPlayerDestroyed: isPlayerDestroyed,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					return null;

				case ObjectAction.Type.SetParentNumericVariable:

					if (obj.ParentObject == null)
						throw new Exception();

					obj.ParentObject.NumericVariables[action.SetVariableName] = action.SetNumericVariableValue.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					return null;

				case ObjectAction.Type.SetParentBooleanVariable:
				{
					if (obj.ParentObject == null)
						throw new Exception();

					obj.ParentObject.BooleanVariables[action.SetVariableName] = action.SetBooleanVariableValue.Evaluate(
						obj.GetEnemyObjectExpressionInfo(),
						isParentDestroyed: obj.ParentObject.IsDestroyed,
						isPlayerDestroyed: isPlayerDestroyed,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					return null;
				}
				case ObjectAction.Type.EndLevel:

					actionResult.ShouldEndLevel = true;
					return null;

				case ObjectAction.Type.PlaySoundEffect:
				{
					if (actionResult.NewSoundEffectsToPlay == null)
						actionResult.NewSoundEffectsToPlay = new List<string>();
					
					actionResult.NewSoundEffectsToPlay.Add(action.SoundEffectName);
					return null;
				}
				case ObjectAction.Type.DisplayBossHealthBar:
				{
					long bossHealthMeterNumber = action.BossHealthBarMeterNumber.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);
						
					long bossHealthMeterMilliPercentage = action.BossHealthBarMilliPercentage.Evaluate(
						obj,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					actionResult.BossHealthMeterNumber = bossHealthMeterNumber;
					actionResult.BossHealthMeterMilliPercentage = bossHealthMeterMilliPercentage;
					return null;
				}
				case ObjectAction.Type.SetSpriteName:

					obj.SpriteName = action.SpriteName;

					return null;

				case ObjectAction.Type.Conditional:
				{
					bool shouldExecute = action.Conditional.Evaluate(
						obj.GetEnemyObjectExpressionInfo(),
						isParentDestroyed: isParentDestroyed,
						isPlayerDestroyed: isPlayerDestroyed,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					if (shouldExecute)
					{
						var newObjectAction = HandleAction(
							action: action.ConditionalAction,
							obj: obj,
							playerXMillis: playerXMillis,
							playerYMillis: playerYMillis,
							elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
							isPlayerDestroyed: isPlayerDestroyed,
							enemyObjectTemplates: enemyObjectTemplates,
							rng: rng,
							actionResult: actionResult);

						if (newObjectAction == null)
							return null;
							
						return ObjectAction.Condition(action.Conditional, newObjectAction);
					}
					else
					{
						return null;
					}
				}
				case ObjectAction.Type.ConditionalNextAction:
				{
					ObjectAction newObjectAction = HandleAction(
							action: action.ConditionalNextActionCurrentAction,
							obj: obj,
							playerXMillis: playerXMillis,
							playerYMillis: playerYMillis,
							elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
							isPlayerDestroyed: isPlayerDestroyed,
							enemyObjectTemplates: enemyObjectTemplates,
							rng: rng,
							actionResult: actionResult);
					
					bool shouldMoveToNext = action.ConditionalNextActionConditional.Evaluate(
						obj.GetEnemyObjectExpressionInfo(),
						isParentDestroyed: isParentDestroyed,
						isPlayerDestroyed: isPlayerDestroyed,
						playerXMillis: playerXMillis,
						playerYMillis: playerYMillis,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
						rng: rng);

					if (shouldMoveToNext)
						return action.ConditionalNextActionNextAction;
					
					if (newObjectAction != null)
						return ObjectAction.ConditionalNextAction(newObjectAction, action.ConditionalNextActionConditional, action.ConditionalNextActionNextAction);
						
					return null;
				}
				case ObjectAction.Type.Union:
				{
					var newUnionActions = new List<ObjectAction>();
					int numActions = 0;

					int unionActionCount = action.UnionActions.Count;
					for (var i = 0; i < unionActionCount; i++)
					{
						var unionActionI = action.UnionActions[i];
						
						ObjectAction newObjectAction = HandleAction(
								action: unionActionI,
								obj: obj,
								playerXMillis: playerXMillis,
								playerYMillis: playerYMillis,
								elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration,
								isPlayerDestroyed: isPlayerDestroyed,
								enemyObjectTemplates: enemyObjectTemplates,
								rng: rng,
								actionResult: actionResult);

						if (newObjectAction != null)
						{
							if (newObjectAction.ObjectActionType != ObjectAction.Type.Noop)
							{
								newUnionActions.Add(newObjectAction);
								numActions++;
							}
						}
						else
						{
							newUnionActions.Add(unionActionI);
							numActions++;
						}
					}
					
					if (numActions == 0)
						return ObjectAction.Noop();
					if (numActions == 1)
						return newUnionActions[0];
					return ObjectAction.Union_ImmutableList(newUnionActions);
				}
				case ObjectAction.Type.Noop:
					
					return null;
					
				default:
					throw new Exception();
			}
		}
	}
}
