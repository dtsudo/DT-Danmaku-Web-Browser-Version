
DTDanmakuBridgeSoundsJavascript = ((function () {
	"use strict";
	
	var prefix = "Data/Audio/";
	
	var enemyDeathAudioArray = [];
	var playerDeathAudioArray = [];
	var playerShootAudioArray = [];
	
	for (var i = 0; i < 10; i++) {
		enemyDeathAudioArray.push(new Audio(prefix + "EnemyDeath.ogg"));
		playerDeathAudioArray.push(new Audio(prefix + "PlayerDeath.ogg"));
		playerShootAudioArray.push(new Audio(prefix + "PlayerShoot.ogg"));
	}
	
	return {
		danmakuSounds: {
			enemyDeath: { audioArray: enemyDeathAudioArray, volume: 0.10 },
			playerDeath: { audioArray: playerDeathAudioArray, volume: 0.20 },
			playerShoot: { audioArray: playerShootAudioArray, volume: 0.20 }
		}
	};
})());
