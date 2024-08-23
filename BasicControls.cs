namespace Tiles;

public static class BasicGuiManager
{
	public static Bitmap[]? MenuIcons;
	public static Bitmap[]? TileIcons;
	public static bool ExtraEffects = true;
	public static int LineSize = 30;
}

public class StandardButton : Button
{
	private const int ImageId = 28;

	public StandardButton()
	{
		Resize += (_, _) => HelperStuff.UpdateFont(this);
		TextChanged += (_, _) => HelperStuff.UpdateFont(this);

		BackgroundImage = BasicGuiManager.MenuIcons?[ImageId] ?? new Bitmap(10, 10);
		BackColor = Color.SlateGray;

		FlatStyle = FlatStyle.Flat;
		FlatAppearance.BorderColor = Color.Yellow;
		FlatAppearance.BorderSize = 3;

		if (BasicGuiManager.ExtraEffects)
		{
			HelperStuff.SetupMouseEffects(this, true, true, true);
		}
	}
}

public class StandardLabel : Label
{
	private const int ImageId = 29;

	public StandardLabel()
	{
		Resize += (_, _) => HelperStuff.UpdateFont(this);
		TextChanged += (_, _) => HelperStuff.UpdateFont(this);

		BackgroundImage = BasicGuiManager.MenuIcons?[ImageId] ?? new Bitmap(10, 10);
		BackColor = Color.Tan;
	}
}