
namespace DTDanmaku
{
	using DTLib;
	using DTDanmakuLib;
	using Bridge;

	public class DTDanmakuInitializer
	{
		private static IKeyboard bridgeKeyboard;
		private static IMouse bridgeMouse;
		private static IKeyboard previousKeyboard;
		private static IMouse previousMouse;
		
		private static IDisplay<IDTDanmakuAssets> display;
		
		private static IFrame<IDTDanmakuAssets> frame;
		
		private static void ClearCanvas()
		{
			Script.Write("DTDanmakuBridgeDisplayJavascript.clearCanvas()");
		}
		
		public static void Start(int fps, bool debugMode)
		{
			frame = DTDanmaku.GetFirstFrame(
				fps: fps,
				rng: new DTDeterministicRandom(seed: 0),
				guidGenerator: new GuidGenerator(guidString: "73"),
				debugMode: debugMode);

			bridgeKeyboard = new BridgeKeyboard();
			bridgeMouse = new BridgeMouse();
			
			display = new DTDanmakuBridgeDisplay();
			
			previousKeyboard = new EmptyKeyboard();
			previousMouse = new EmptyMouse();
			
			ClearCanvas();
			frame.Render(display);
		}
		
		public static void ComputeAndRenderNextFrame()
		{
			IKeyboard currentKeyboard = new CopiedKeyboard(bridgeKeyboard);
			IMouse currentMouse = new CopiedMouse(bridgeMouse);
			
			frame = frame.GetNextFrame(currentKeyboard, currentMouse, previousKeyboard, previousMouse, display);
			ClearCanvas();
			frame.Render(display);
			
			previousKeyboard = new CopiedKeyboard(currentKeyboard);
			previousMouse = new CopiedMouse(currentMouse);
		}
	}
}
