
/*
	Requires DTDanmakuBridgeImagesJavascript.js and DTDanmakuBridgeSoundsJavascript.js to already be on the page.
*/
DTDanmakuBridgeDisplayJavascript = ((function () {
	"use strict";
	
	var drawImage = function (imageName, x, y) {
		var canvas = document.getElementById("dtDanmakuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		var img = DTDanmakuBridgeImagesJavascript.danmakuImages[imageName];
		
		if (img === null || img === undefined)
			throw "DTDanmakuBridgeDisplayJavascript: Image not found";
		
		context.drawImage(img, x, y);
	};
	
	var drawImageRotatedCounterclockwise = function (imageName, x, y, milliDegrees) {
		var canvas = document.getElementById("dtDanmakuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		var img = DTDanmakuBridgeImagesJavascript.danmakuImages[imageName];
		
		if (img === null || img === undefined)
			throw "DTDanmakuBridgeDisplayJavascript: Image not found";
		
		var translateX = x + img.width / 2;
		var translateY = y + img.height / 2;
		
		context.translate(translateX, translateY);
		context.rotate(-milliDegrees / 1000.0 * (2.0 * Math.PI / 360.0));
		context.translate(-translateX, -translateY);
		
		context.drawImage(img, x, y);
		
		context.setTransform(1, 0, 0, 1, 0, 0);
	};
	
	var drawRectangle = function (x, y, width, height, red, green, blue, alpha, fill) {
		var canvas = document.getElementById("dtDanmakuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		context.fillStyle = "rgba(" + red.toString() + ", " + green.toString() + ", " + blue.toString() + ", " + (alpha / 255).toString() + ")";
		context.strokeStyle = "rgba(" + red.toString() + ", " + green.toString() + ", " + blue.toString() + ", " + (alpha / 255).toString() + ")";
		
		if (fill)
			context.fillRect(x, y, width, height);
		else
			context.strokeRect(x, y, width, height);
	};
	
	var clearCanvas = function () {
		var canvas = document.getElementById("dtDanmakuCanvas");
		
		if (canvas === null)
			return;
		
		var context = canvas.getContext("2d");
		
		context.clearRect(0, 0, canvas.width, canvas.height);
	};
	
	var playSound = function (soundName, volume) {
		var sound = DTDanmakuBridgeSoundsJavascript.danmakuSounds[soundName];
		var finalVolume = sound.volume * (volume / 100.0);
		
		if (finalVolume > 1.0)
			finalVolume = 1.0;
		if (finalVolume < 0.0)
			finalVolume = 0.0;
		
		var audio = sound.audioArray[0];
		
		for (var i = 0; i < sound.audioArray.length; i++) {
			if (i === sound.audioArray.length - 1)
				sound.audioArray[i] = audio;
			else
				sound.audioArray[i] = sound.audioArray[i+1];
		}
		
		audio.volume = finalVolume;
		audio.play();
	};
	
	return {
		drawImage: drawImage,
		drawImageRotatedCounterclockwise: drawImageRotatedCounterclockwise,
		drawRectangle: drawRectangle,
		clearCanvas: clearCanvas,
		playSound: playSound
	};
})());
