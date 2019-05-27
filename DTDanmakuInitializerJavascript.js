
((function () {
	"use strict";
	
	var urlParams = (new URL(document.location)).searchParams;
	
	var showFps = urlParams.get("showfps") !== null
		? (urlParams.get("showfps") === "true")
		: false;
	var fps = urlParams.get("fps") !== null
		? parseInt(urlParams.get("fps"), 10)
		: 30;
	var debugMode = urlParams.get("debugmode") !== null
		? (urlParams.get("debugmode") === "true")
		: false;
	
	DTDanmaku.DTDanmakuInitializer.Start(fps, debugMode);
	
	var computeAndRenderNextFrame;
	
	var numberOfFrames = 0;
	var startTimeMillis = Date.now();
	
	var nextTimeToAct = Date.now() + (1000.0 / fps);
	
	var hasAddedFpsLabel = false;
	
	computeAndRenderNextFrame = function () {
		if (nextTimeToAct > Date.now())
			return;
		
		if (nextTimeToAct < Date.now() - 5.0*(1000.0 / fps))
			nextTimeToAct = Date.now() - 5.0*(1000.0 / fps);
		
		nextTimeToAct = nextTimeToAct + (1000.0 / fps);
		
		DTDanmaku.DTDanmakuInitializer.ComputeAndRenderNextFrame();
		
		numberOfFrames++;
		
		if (showFps) {
			if (!hasAddedFpsLabel) {
				var fpsLabelNode = document.getElementById("dtDanmakuFpsLabel");
				if (fpsLabelNode !== null) {
					fpsLabelNode.textContent = "FPS: ";
					hasAddedFpsLabel = true;
				}
			}
		}
		
		var currentTimeMillis = Date.now();
		if (currentTimeMillis - startTimeMillis > 2000) {
			var actualFps = numberOfFrames / 2;
			var fpsNode = document.getElementById("dtDanmakuFps");
			
			if (showFps) {
				if (fpsNode !== null)
					fpsNode.textContent = actualFps.toString();
			}
				
			numberOfFrames = 0;
			startTimeMillis = currentTimeMillis;
		}
	};
	
	setInterval(computeAndRenderNextFrame, 10);
	
})());
