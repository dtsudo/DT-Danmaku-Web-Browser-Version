
namespace DTDanmakuLib
{
	using System.Collections.Generic;

	public class EnemyObjectMover
	{
		public static void UpdateEnemyPositions(
			List<EnemyObject> enemyObjects,
			long elapsedMillisecondsPerIteration)
		{
			foreach (EnemyObject enemyObject in enemyObjects)
			{
				if (enemyObject.IsDestroyed)
					continue;

				var speed = enemyObject.SpeedInMillipixelsPerMillisecond;
				if (speed > 0)
				{
					DTDanmakuMath.Offset offset = DTDanmakuMath.GetOffset_PositiveSpeed(
						speedInMillipixelsPerMillisecond: speed * 2,
						movementDirectionInMillidegrees: enemyObject.MovementDirectionInMillidegrees,
						elapsedMillisecondsPerIteration: elapsedMillisecondsPerIteration);

					enemyObject.XMillis += offset.DeltaXInMillipixels;
					enemyObject.YMillis += offset.DeltaYInMillipixels;
				}
			}
		}
	}
}
