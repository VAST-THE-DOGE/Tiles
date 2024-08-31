namespace Tiles;

public static class BasicGuiManager
{
	public static Bitmap[]? MenuIcons;
	public static Bitmap[]? TileIcons;
	public static Bitmap NO_IMAGE_ICON;
	public static bool ExtraEffects = true;
	public static int LineSize = 50;

	public static void SetAllControlImages(this Control control)
	{
		foreach (Control c in control.Controls)
		{
			c.SetAllControlImages();
		}

		switch (control)
		{
			case StandardButton c:
				c.BackgroundImage = MenuIcons?[StandardButton.ImageId] ?? NO_IMAGE_ICON;
				break;
			case StandardLabel c:
				c.BackgroundImage = MenuIcons?[StandardLabel.ImageId] ?? NO_IMAGE_ICON;
				break;
			case StandardBackground c:
				c.BackgroundImage = HelperStuff.ResizeImage(MenuIcons?[StandardBackground.ImageId] ?? NO_IMAGE_ICON,
					LineSize, LineSize, false);
				break;
			case StandardBackgroundControl c:
				c.BackgroundImage =
					HelperStuff.ResizeImage(MenuIcons?[StandardBackgroundControl.ImageId] ?? NO_IMAGE_ICON, LineSize,
						LineSize, false);
				break;
			case StandardBorder c:
				c.Visible = false;
				c.BackgroundImage = (c.PanelSide) switch
				{
					BorderType.Top or BorderType.Bottom => HelperStuff.ResizeImage(
						MenuIcons?[(int)c.PanelSide] ?? NO_IMAGE_ICON, c.Height, c.Height, false),
					BorderType.Left or BorderType.Right => HelperStuff.ResizeImage(
						MenuIcons?[(int)c.PanelSide] ?? NO_IMAGE_ICON, c.Width, c.Width, false),
					_ => HelperStuff.ResizeImage(MenuIcons?[(int)c.PanelSide] ?? NO_IMAGE_ICON, c.Width, c.Height,
						false),
				};
				c.Visible = true;
				break;
		}

		;
	}
}

public class StandardButton : Button
{
	public const int ImageId = 28;

	public StandardButton()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();

		Resize += (_, _) => HelperStuff.UpdateFontNew(this);
		TextChanged += (_, _) => HelperStuff.UpdateFontNew(this);

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
	public const int ImageId = 29;

	public StandardLabel()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();

		Resize += (_, _) => HelperStuff.UpdateFontNew(this);
		TextChanged += (_, _) => HelperStuff.UpdateFontNew(this);
	}
}

public class StandardBackground : Panel
{
	public const int ImageId = 8;

	public StandardBackground()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();

		Resize += (_, _) => HelperStuff.UpdateFontNew(this);
		TextChanged += (_, _) => HelperStuff.UpdateFontNew(this);
	}
}

public class StandardBorder : Panel
{
	public BorderType? PanelSide = BorderType.None;

	public StandardBorder()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();

		Margin = new Padding(0);
		Dock = DockStyle.Fill;
		BackgroundImageLayout = ImageLayout.Tile;
	}
}

public class StandardBackgroundControl : UserControl
{
	public const int ImageId = 8;

	public StandardBackgroundControl()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();
	}
}

public class MyForm : Form
{
	public MyForm()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();
	}
}

public class MyTableLayoutPanel : TableLayoutPanel
{
	public MyTableLayoutPanel()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();
	}
}

public enum BorderType
{
	None = 8,
	Top = 3,
	TopLeft = 4,
	Left = 0,
	BottomLeft = 5,
	Bottom = 1,
	BottomRight = 6,
	Right = 2,
	TopRight = 7
}