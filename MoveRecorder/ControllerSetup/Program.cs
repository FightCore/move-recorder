using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.DualShock4;

Console.WriteLine("Connecting controller");
var client = new ViGEmClient();
var ds4 = client.CreateDualShock4Controller();
ds4.Connect();

var inputDictionary = new Dictionary<string, DualShock4Button>()
{
	{ "A", DualShock4Button.Circle },
	{ "B", DualShock4Button.Cross },
	{ "X", DualShock4Button.Triangle },
	{ "Y", DualShock4Button.Square},
	{ "Z", DualShock4Button.ShoulderLeft },
	{ "L", DualShock4Button.ThumbLeft },
	{ "R", DualShock4Button.ThumbRight },
	{ "Start", DualShock4Button.Options },
};

foreach (var (dolphinInput, button) in inputDictionary)
{
	Console.WriteLine("Select the " + dolphinInput);
	Console.ReadLine();
	Thread.Sleep(500);
	ds4.SetButtonState(button, true);
	Thread.Sleep(500);
	ds4.SetButtonState(button, false);
}

var dpadDictionary = new Dictionary<string, DualShock4DPadDirection>()
{
	{ "Up", DualShock4DPadDirection.North},
	{ "Down", DualShock4DPadDirection.South},
	{ "Left", DualShock4DPadDirection.West},
	{ "Right", DualShock4DPadDirection.East}
};

foreach (var (name, direction) in dpadDictionary)
{
	Console.WriteLine("Select the Dpad" + name);
	Console.ReadLine();
	Thread.Sleep(500);
	ds4.SetDPadDirection(direction);
	Thread.Sleep(500);
	ds4.SetDPadDirection(DualShock4DPadDirection.None);
}


var axisDictionary = new List<(string, int, short value)>
{
	("Up", 0, short.MaxValue),
	("Down", 0, short.MinValue),
	("Left", 1, short.MinValue),
	("Right", 1, short.MaxValue),
	("C-Stick Up", 2, short.MaxValue),
	("C-Stick Down", 2, short.MinValue),
	("C-Stick Left", 3, short.MinValue),
	("C-Stick Right", 3, short.MaxValue),
};

foreach (var (name, axis, value) in axisDictionary)
{
	Console.WriteLine("Select the " + name);
	Console.ReadLine();
	Thread.Sleep(500);
	ds4.SetAxisValue(axis, value);
	Thread.Sleep(500);
	ds4.SetDPadDirection(DualShock4DPadDirection.None);
}
