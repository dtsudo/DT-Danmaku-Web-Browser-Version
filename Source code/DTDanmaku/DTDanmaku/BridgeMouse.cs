
namespace DTDanmaku
{
	using DTLib;
	using Bridge;
	
	public class BridgeMouse : IMouse
	{
		public BridgeMouse()
		{
		}
		
		public int GetX()
		{
			return Script.Write<int>("DTDanmakuBridgeMouseJavascript.getMouseX()");
		}

		public int GetY()
		{
			return Script.Write<int>("DTDanmakuBridgeMouseJavascript.getMouseY()");
		}

		public bool IsLeftMouseButtonPressed()
		{
			return Script.Write<bool>("DTDanmakuBridgeMouseJavascript.isLeftMouseButtonPressed()");
		}

		public bool IsRightMouseButtonPressed()
		{
			return Script.Write<bool>("DTDanmakuBridgeMouseJavascript.isRightMouseButtonPressed()");
		}
	}
}
