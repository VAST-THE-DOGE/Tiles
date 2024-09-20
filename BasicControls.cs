using System.Runtime.InteropServices;

namespace Tiles;

public static class BasicGuiManager
{
	public static Bitmap[]? MenuIcons;
	public static Bitmap[]? TileIcons;
	public static Bitmap NO_IMAGE_ICON;
	public static bool ExtraEffects = true;
	public static int LineSize = 25;

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

public class MyFlowPanel : FlowLayoutPanel
{
	public MyFlowPanel()
	{
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();
	}
}

//TODO: good paint events for the thumb:
/*using (var g = Graphics.FromImage(MovingThumb))
		{
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			g.Clear(Color.Yellow);
			g.DrawRectangle(new Pen(Color.Gray), 3,3,MovingThumb.Width - 6,MovingThumb.Height - 6);
			g.DrawRectangle(new Pen(Color.Yellow), 6,18,MovingThumb.Width - 12,3);
			g.DrawRectangle(new Pen(Color.Yellow), 6,33,MovingThumb.Width - 12,3);
			g.DrawRectangle(new Pen(Color.Yellow), 6,48,MovingThumb.Width - 12,3);
		}
		using (var g = Graphics.FromImage(MovingThumb))
		{
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			g.Clear(Color.Yellow);
			g.DrawRectangle(new Pen(Color.Gray), 3,3,MovingThumb.Width - 6,MovingThumb.Height - 6);
			g.DrawRectangle(new Pen(Color.DimGray), 6,18,MovingThumb.Width - 12,3);
			g.DrawRectangle(new Pen(Color.DimGray), 6,33,MovingThumb.Width - 12,3);
			g.DrawRectangle(new Pen(Color.DimGray), 6,48,MovingThumb.Width - 12,3);
		}*/

public class TheCoolScrollBar : UserControl
{
	private bool isDragging = false;
	public int thumbHeight = 50;
	private int thumbPosition = 0;

	public TheCoolScrollBar()
	{
		this.SetStyle(ControlStyles.ResizeRedraw, true);
		this.SetStyle(ControlStyles.UserPaint, true);
		this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
	}

	public int ThumbPosition
	{
		get { return thumbPosition; }
		set
		{
			thumbPosition = value;
			this.Invalidate();
			Scroll?.Invoke(this, EventArgs.Empty); // Trigger the scroll event
		}
	}

	public void RefreshThumb(int newThumb)
	{
		thumbPosition = newThumb;
		this.Invalidate();
	}

	public event EventHandler Scroll;

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		var g = e.Graphics;
		g.Clear(Color.Gray); // Background color

		// Draw the thumb
		var thumbRect = new Rectangle(0, thumbPosition, this.Width, thumbHeight);
		g.FillRectangle(Brushes.Yellow, thumbRect);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (e.Button == MouseButtons.Left)
		{
			var thumbRect = new Rectangle(0, thumbPosition, this.Width, thumbHeight);
			if (thumbRect.Contains(e.Location))
			{
				isDragging = true;
			}
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (isDragging)
		{
			thumbPosition = e.Y - thumbHeight / 2;
			thumbPosition = Math.Max(0, Math.Min(thumbPosition, this.Height - thumbHeight));
			this.Invalidate();
			Scroll?.Invoke(this, EventArgs.Empty); // Trigger the scroll event
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (e.Button == MouseButtons.Left)
		{
			isDragging = false;
		}
	}
}

public class CustomFlowLayoutPanel : FlowLayoutPanel
{
	public CustomFlowLayoutPanel()
	{
		this.SetStyle(ControlStyles.ResizeRedraw, true);
		this.SetStyle(ControlStyles.UserPaint, true);
		this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
	}
	
	private const int WM_NCCALCSIZE = 0x0083;

	[DllImport("user32.dll")]
	private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);

		if (m.Msg == WM_NCCALCSIZE)
		{
			// Hide both horizontal and vertical scrollbars
			ShowScrollBar(this.Handle, (int)ScrollBarDir.SB_BOTH, false);
		}
	}

	private enum ScrollBarDir
	{
		SB_HORZ = 0,
		SB_VERT = 1,
		SB_BOTH = 3
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