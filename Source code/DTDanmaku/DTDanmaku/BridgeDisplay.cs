
namespace DTDanmaku
{
	using DTLib;
	using DTDanmakuLib;
	using System;
	using Bridge;
	
	public abstract class BridgeDisplay<T> : IDisplay<T> where T : class, IAssets
	{
		public BridgeDisplay()
		{
		}

		public void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			int red = color.R;
			int green = color.G;
			int blue = color.B;
			int alpha = color.Alpha;
					
			Script.Call("DTDanmakuBridgeDisplayJavascript.drawRectangle", x, y, width, height, red, green, blue, alpha, fill);
		}
		
		public void DebugPrint(int x, int y, string debugText)
		{
		}
		
		public abstract T GetAssets();
	}

	public class DTDanmakuBridgeDisplay : BridgeDisplay<IDTDanmakuAssets>
	{
		private DTDanmakuAssets assets;

		private class DTDanmakuAssets : IDTDanmakuAssets
		{
			public void DrawInitialLoadingScreen()
			{
			}
			
			public bool LoadImages()
			{
				return true;
			}

			public long GetWidth(DTDanmakuImage image)
			{
				switch (image)
				{
					case DTDanmakuImage.TitleScreen: return 366;
					case DTDanmakuImage.InstructionScreen: return 354;
					case DTDanmakuImage.Version: return 47;
					case DTDanmakuImage.YouWin: return 202;
					case DTDanmakuImage.GameOver: return 210;
					case DTDanmakuImage.PlayerShip: return 99;
					case DTDanmakuImage.PlayerShipInvulnerable: return 99;
					case DTDanmakuImage.PlayerBullet: return 9;
					case DTDanmakuImage.PlayerLifeIcon: return 33;
					case DTDanmakuImage.PowerUp: return 34;
					case DTDanmakuImage.Pause: return 128;
					case DTDanmakuImage.Continue: return 161;
					case DTDanmakuImage.Quit: return 76;
					case DTDanmakuImage.BasicEnemyShip: return 93;
					case DTDanmakuImage.SniperEnemyShip: return 104;
					case DTDanmakuImage.EliteSniperEnemyShip: return 104;
					case DTDanmakuImage.OrbiterEnemyShip: return 143;
					case DTDanmakuImage.EliteOrbiterEnemyShip: return 144;
					case DTDanmakuImage.OrbiterSatellite: return 46;
					case DTDanmakuImage.EliteOrbiterSatellite: return 46;
					case DTDanmakuImage.BarrageEnemyShip: return 97;
					case DTDanmakuImage.EliteBarrageEnemyShip: return 97;
					case DTDanmakuImage.Boss: return 208;
					case DTDanmakuImage.EnemyBullet: return 9;
					case DTDanmakuImage.SniperEnemyBullet: return 32;
					case DTDanmakuImage.EliteSniperEnemyBullet: return 32;
					case DTDanmakuImage.OrbiterEnemyBullet: return 12;
					case DTDanmakuImage.EliteOrbiterEnemyBullet: return 12;
					case DTDanmakuImage.BarrageEnemyBullet: return 9;
					case DTDanmakuImage.EliteBarrageEnemyBullet: return 9;
					case DTDanmakuImage.BossPhase1EnemyBullet: return 12;
					case DTDanmakuImage.BossPhase2EnemyBullet: return 32;
					case DTDanmakuImage.BossPhase3EnemyBullet: return 9;
					case DTDanmakuImage.Explosion1: return 192;
					case DTDanmakuImage.Explosion2: return 152;
					case DTDanmakuImage.Explosion3: return 82;
					case DTDanmakuImage.Explosion4: return 92;
					case DTDanmakuImage.Explosion5: return 120;
					case DTDanmakuImage.Explosion6: return 133;
					case DTDanmakuImage.Explosion7: return 138;
					case DTDanmakuImage.Explosion8: return 143;
					case DTDanmakuImage.Explosion9: return 149;
					case DTDanmakuImage.SoundOff: return 50;
					case DTDanmakuImage.SoundOn: return 50;
					default: throw new Exception();
				}
			}

			public long GetHeight(DTDanmakuImage image)
			{
				switch (image)
				{
					case DTDanmakuImage.TitleScreen: return 182;
					case DTDanmakuImage.InstructionScreen: return 226;
					case DTDanmakuImage.Version: return 17;
					case DTDanmakuImage.YouWin: return 129;
					case DTDanmakuImage.GameOver: return 129;
					case DTDanmakuImage.PlayerShip: return 75;
					case DTDanmakuImage.PlayerShipInvulnerable: return 75;
					case DTDanmakuImage.PlayerBullet: return 54;
					case DTDanmakuImage.PlayerLifeIcon: return 26;
					case DTDanmakuImage.PowerUp: return 33;
					case DTDanmakuImage.Pause: return 34;
					case DTDanmakuImage.Continue: return 34;
					case DTDanmakuImage.Quit: return 34;
					case DTDanmakuImage.BasicEnemyShip: return 84;
					case DTDanmakuImage.SniperEnemyShip: return 84;
					case DTDanmakuImage.EliteSniperEnemyShip: return 84;
					case DTDanmakuImage.OrbiterEnemyShip: return 147;
					case DTDanmakuImage.EliteOrbiterEnemyShip: return 147;
					case DTDanmakuImage.OrbiterSatellite: return 46;
					case DTDanmakuImage.EliteOrbiterSatellite: return 46;
					case DTDanmakuImage.BarrageEnemyShip: return 84;
					case DTDanmakuImage.EliteBarrageEnemyShip: return 84;
					case DTDanmakuImage.Boss: return 168;
					case DTDanmakuImage.EnemyBullet: return 54;
					case DTDanmakuImage.SniperEnemyBullet: return 30;
					case DTDanmakuImage.EliteSniperEnemyBullet: return 30;
					case DTDanmakuImage.OrbiterEnemyBullet: return 26;
					case DTDanmakuImage.EliteOrbiterEnemyBullet: return 26;
					case DTDanmakuImage.BarrageEnemyBullet: return 17;
					case DTDanmakuImage.EliteBarrageEnemyBullet: return 17;
					case DTDanmakuImage.BossPhase1EnemyBullet: return 26;
					case DTDanmakuImage.BossPhase2EnemyBullet: return 30;
					case DTDanmakuImage.BossPhase3EnemyBullet: return 17;
					case DTDanmakuImage.Explosion1: return 192;
					case DTDanmakuImage.Explosion2: return 150;
					case DTDanmakuImage.Explosion3: return 91;
					case DTDanmakuImage.Explosion4: return 102;
					case DTDanmakuImage.Explosion5: return 124;
					case DTDanmakuImage.Explosion6: return 134;
					case DTDanmakuImage.Explosion7: return 140;
					case DTDanmakuImage.Explosion8: return 144;
					case DTDanmakuImage.Explosion9: return 151;
					case DTDanmakuImage.SoundOff: return 50;
					case DTDanmakuImage.SoundOn: return 50;
					default: throw new Exception();
				}
			}
			
			public void DrawImage(DTDanmakuImage image, int x, int y)
			{
				Script.Call("DTDanmakuBridgeDisplayJavascript.drawImage", TranslateToString(image), x, y);
			}
			
			public void DrawImageRotatedCounterclockwise(DTDanmakuImage image, int x, int y, int milliDegrees)
			{
				Script.Call("DTDanmakuBridgeDisplayJavascript.drawImageRotatedCounterclockwise", TranslateToString(image), x, y, milliDegrees);
			}
			
			public bool LoadSounds()
			{
				return true;
			}
			
			public void PlaySound(DTDanmakuSound sound, int volume)
			{
				string soundName;
				
				switch (sound)
				{
					case DTDanmakuSound.PlayerShoot: soundName = "playerShoot"; break;
					case DTDanmakuSound.PlayerDeath: soundName = "playerDeath"; break;
					case DTDanmakuSound.EnemyDeath: soundName = "enemyDeath"; break;
					default: throw new Exception();
				}
				
				Script.Call("DTDanmakuBridgeDisplayJavascript.playSound", soundName, volume);
			}
			
			private static string TranslateToString(DTDanmakuImage image)
			{
				switch (image)
				{
					case DTDanmakuImage.TitleScreen: return "titleScreen";
					case DTDanmakuImage.InstructionScreen: return "instructionScreen";
					case DTDanmakuImage.Version: return "version";
					case DTDanmakuImage.YouWin: return "youWin";
					case DTDanmakuImage.GameOver: return "gameOver";
					case DTDanmakuImage.PlayerShip: return "playerShip";
					case DTDanmakuImage.PlayerShipInvulnerable: return "playerShipInvulnerable";
					case DTDanmakuImage.PlayerBullet: return "playerBullet";
					case DTDanmakuImage.PlayerLifeIcon: return "playerLifeIcon";
					case DTDanmakuImage.PowerUp: return "powerUp";
					case DTDanmakuImage.Pause: return "pause";
					case DTDanmakuImage.Continue: return "continueImg";
					case DTDanmakuImage.Quit: return "quit";
					case DTDanmakuImage.BasicEnemyShip: return "basicEnemyShip";
					case DTDanmakuImage.SniperEnemyShip: return "sniperEnemyShip";
					case DTDanmakuImage.EliteSniperEnemyShip: return "eliteSniperEnemyShip";
					case DTDanmakuImage.OrbiterEnemyShip: return "orbiterEnemyShip";
					case DTDanmakuImage.EliteOrbiterEnemyShip: return "eliteOrbiterEnemyShip";
					case DTDanmakuImage.OrbiterSatellite: return "orbiterSatellite";
					case DTDanmakuImage.EliteOrbiterSatellite: return "eliteOrbiterSatellite";
					case DTDanmakuImage.BarrageEnemyShip: return "barrageEnemyShip";
					case DTDanmakuImage.EliteBarrageEnemyShip: return "eliteBarrageEnemyShip";
					case DTDanmakuImage.Boss: return "boss";
					case DTDanmakuImage.EnemyBullet: return "enemyBullet";
					case DTDanmakuImage.SniperEnemyBullet: return "sniperEnemyBullet";
					case DTDanmakuImage.EliteSniperEnemyBullet: return "eliteSniperEnemyBullet";
					case DTDanmakuImage.OrbiterEnemyBullet: return "orbiterEnemyBullet";
					case DTDanmakuImage.EliteOrbiterEnemyBullet: return "eliteOrbiterEnemyBullet";
					case DTDanmakuImage.BarrageEnemyBullet: return "barrageEnemyBullet";
					case DTDanmakuImage.EliteBarrageEnemyBullet: return "eliteBarrageEnemyBullet";
					case DTDanmakuImage.BossPhase1EnemyBullet: return "bossPhase1EnemyBullet";
					case DTDanmakuImage.BossPhase2EnemyBullet: return "bossPhase2EnemyBullet";
					case DTDanmakuImage.BossPhase3EnemyBullet: return "bossPhase3EnemyBullet";
					case DTDanmakuImage.Explosion1: return "explosion1";
					case DTDanmakuImage.Explosion2: return "explosion2";
					case DTDanmakuImage.Explosion3: return "explosion3";
					case DTDanmakuImage.Explosion4: return "explosion4";
					case DTDanmakuImage.Explosion5: return "explosion5";
					case DTDanmakuImage.Explosion6: return "explosion6";
					case DTDanmakuImage.Explosion7: return "explosion7";
					case DTDanmakuImage.Explosion8: return "explosion8";
					case DTDanmakuImage.Explosion9: return "explosion9";
					case DTDanmakuImage.SoundOff: return "soundOff";
					case DTDanmakuImage.SoundOn: return "soundOn";
					default: throw new Exception();
				}
			}
		}

		public DTDanmakuBridgeDisplay()
		{
			this.assets = new DTDanmakuAssets();
		}

		public override IDTDanmakuAssets GetAssets()
		{
			return this.assets;
		}
	}
}
