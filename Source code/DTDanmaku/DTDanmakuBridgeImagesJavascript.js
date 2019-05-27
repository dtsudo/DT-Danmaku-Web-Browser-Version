
DTDanmakuBridgeImagesJavascript = ((function () {
	"use strict";
	
	var prefix = "Data/Images/";
	
	var titleScreen = new Image();
	titleScreen.src = prefix + "TitleScreen.png";
	var instructionScreen = new Image();
	instructionScreen.src = prefix + "InstructionScreen.png";
	var version = new Image();
	version.src = prefix + "Version.png";
	var youWin = new Image();
	youWin.src = prefix + "YouWin.png"; 
	var gameOver = new Image();
	gameOver.src = prefix + "GameOver.png";
	var playerShip = new Image();
	playerShip.src = prefix + "PlayerShip.png";
	var playerShipInvulnerable = new Image();
	playerShipInvulnerable.src = prefix + "PlayerShipInvulnerable.png";
	var playerBullet = new Image();
	playerBullet.src = prefix + "PlayerBullet.png";
	var playerLifeIcon = new Image();
	playerLifeIcon.src = prefix + "PlayerLifeIcon.png";
	var powerUp = new Image();
	powerUp.src = prefix + "PowerUp.png";
	var pause = new Image();
	pause.src = prefix + "Paused.png";
	var continueImg = new Image();
	continueImg.src = prefix + "Continue.png";
	var quit = new Image();
	quit.src = prefix + "Quit.png";
	var basicEnemyShip = new Image();
	basicEnemyShip.src = prefix + "BasicEnemyShip.png";
	var sniperEnemyShip = new Image();
	sniperEnemyShip.src = prefix + "SniperEnemyShip.png";
	var eliteSniperEnemyShip = new Image();
	eliteSniperEnemyShip.src = prefix + "EliteSniperEnemyShip.png";
	var orbiterEnemyShip = new Image();
	orbiterEnemyShip.src = prefix + "OrbiterEnemyShip.png";
	var eliteOrbiterEnemyShip = new Image();
	eliteOrbiterEnemyShip.src = prefix + "EliteOrbiterEnemyShip.png";
	var orbiterSatellite = new Image();
	orbiterSatellite.src = prefix + "OrbiterSatellite.png";
	var eliteOrbiterSatellite = new Image();
	eliteOrbiterSatellite.src = prefix + "EliteOrbiterSatellite.png";
	var barrageEnemyShip = new Image();
	barrageEnemyShip.src = prefix + "BarrageEnemyShip.png";
	var eliteBarrageEnemyShip = new Image();
	eliteBarrageEnemyShip.src = prefix + "EliteBarrageEnemyShip.png";
	var boss = new Image();
	boss.src = prefix + "Boss.png";
	var enemyBullet = new Image();
	enemyBullet.src = prefix + "EnemyBullet.png";
	var sniperEnemyBullet = new Image();
	sniperEnemyBullet.src = prefix + "SniperEnemyBullet.png";
	var eliteSniperEnemyBullet = new Image();
	eliteSniperEnemyBullet.src = prefix + "EliteSniperEnemyBullet.png";
	var orbiterEnemyBullet = new Image();
	orbiterEnemyBullet.src = prefix + "OrbiterEnemyBullet.png";
	var eliteOrbiterEnemyBullet = new Image();
	eliteOrbiterEnemyBullet.src = prefix + "EliteOrbiterEnemyBullet.png";
	var barrageEnemyBullet = new Image();
	barrageEnemyBullet.src = prefix + "BarrageEnemyBullet.png";
	var eliteBarrageEnemyBullet = new Image();
	eliteBarrageEnemyBullet.src = prefix + "EliteBarrageEnemyBullet.png";
	var bossPhase1EnemyBullet = new Image();
	bossPhase1EnemyBullet.src = prefix + "BossPhase1EnemyBullet.png";
	var bossPhase2EnemyBullet = new Image();
	bossPhase2EnemyBullet.src = prefix + "BossPhase2EnemyBullet.png";
	var bossPhase3EnemyBullet = new Image();
	bossPhase3EnemyBullet.src = prefix + "BossPhase3EnemyBullet.png";
	var explosion1 = new Image();
	explosion1.src = prefix + "Explosion1.png";
	var explosion2 = new Image();
	explosion2.src = prefix + "Explosion2.png";
	var explosion3 = new Image();
	explosion3.src = prefix + "Explosion3.png";
	var explosion4 = new Image();
	explosion4.src = prefix + "Explosion4.png";
	var explosion5 = new Image();
	explosion5.src = prefix + "Explosion5.png";
	var explosion6 = new Image();
	explosion6.src = prefix + "Explosion6.png";
	var explosion7 = new Image();
	explosion7.src = prefix + "Explosion7.png";
	var explosion8 = new Image();
	explosion8.src = prefix + "Explosion8.png";
	var explosion9 = new Image();
	explosion9.src = prefix + "Explosion9.png";
	var soundOff = new Image();
	soundOff.src = prefix + "SoundOff.png";
	var soundOn = new Image();
	soundOn.src = prefix + "SoundOn.png";
	
	return {
		danmakuImages: {
			titleScreen: titleScreen,
			instructionScreen: instructionScreen,
			version: version,
			youWin: youWin,
			gameOver: gameOver,
			playerShip: playerShip,
			playerShipInvulnerable: playerShipInvulnerable,
			playerBullet: playerBullet,
			playerLifeIcon: playerLifeIcon,
			powerUp: powerUp,
			pause: pause,
			continueImg: continueImg,
			quit: quit,
			basicEnemyShip: basicEnemyShip,
			sniperEnemyShip: sniperEnemyShip,
			eliteSniperEnemyShip: eliteSniperEnemyShip,
			orbiterEnemyShip: orbiterEnemyShip,
			eliteOrbiterEnemyShip: eliteOrbiterEnemyShip,
			orbiterSatellite: orbiterSatellite,
			eliteOrbiterSatellite: eliteOrbiterSatellite,
			barrageEnemyShip: barrageEnemyShip,
			eliteBarrageEnemyShip: eliteBarrageEnemyShip,
			boss: boss,
			enemyBullet: enemyBullet,
			sniperEnemyBullet: sniperEnemyBullet,
			eliteSniperEnemyBullet: eliteSniperEnemyBullet,
			orbiterEnemyBullet: orbiterEnemyBullet,
			eliteOrbiterEnemyBullet: eliteOrbiterEnemyBullet,
			barrageEnemyBullet: barrageEnemyBullet,
			eliteBarrageEnemyBullet: eliteBarrageEnemyBullet,
			bossPhase1EnemyBullet: bossPhase1EnemyBullet,
			bossPhase2EnemyBullet: bossPhase2EnemyBullet,
			bossPhase3EnemyBullet: bossPhase3EnemyBullet,
			explosion1: explosion1,
			explosion2: explosion2,
			explosion3: explosion3,
			explosion4: explosion4,
			explosion5: explosion5,
			explosion6: explosion6,
			explosion7: explosion7,
			explosion8: explosion8,
			explosion9: explosion9,
			soundOff: soundOff,
			soundOn: soundOn
		}
	};
})());