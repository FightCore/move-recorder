using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace MoveRecorder
{
	public class GameCubeButton
	{
		public static readonly DualShock4Button A = DualShock4Button.Circle;
		public static readonly DualShock4Button B = DualShock4Button.Cross;
		public static readonly DualShock4Button X = DualShock4Button.Triangle;
		public static readonly DualShock4Button Y = DualShock4Button.Square;
		public static readonly DualShock4Button Z = DualShock4Button.ShoulderLeft;
		public static readonly DualShock4Button Start = DualShock4Button.Options;
		public static readonly DualShock4Button L = DualShock4Button.ThumbLeft;
		public static readonly DualShock4Button R = DualShock4Button.ThumbRight;

		public static readonly DualShock4DPadDirection DpadUp = DualShock4DPadDirection.North;
		public static readonly DualShock4DPadDirection DpadDown = DualShock4DPadDirection.South;
		public static readonly DualShock4DPadDirection DpadLeft = DualShock4DPadDirection.West;
		public static readonly DualShock4DPadDirection DpadRight = DualShock4DPadDirection.East;
		public static readonly DualShock4DPadDirection DpadNeutral = DualShock4DPadDirection.None;

		public static readonly (int Index, short Value) Up = (0, short.MaxValue);
		public static readonly (int Index, short Value) Down = (0, short.MinValue);
		public static readonly (int Index, short Value) Left = (1, short.MinValue);
		public static readonly (int Index, short Value) Right = (1, short.MaxValue);

		public static readonly (int Index, short Value) CStickUp = (2, short.MaxValue);
		public static readonly (int Index, short Value) CStickDown = (2, short.MinValue);
		public static readonly (int Index, short Value) CStickLeft = (3, short.MinValue);
		public static readonly (int Index, short Value) CStickRight = (3, short.MaxValue);
	}
}
