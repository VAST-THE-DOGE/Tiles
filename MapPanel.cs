namespace Tiles;

public class MapPanel : PictureBox
{
	private const int TileSize = 64;

	private readonly Color DarkColor = Color.FromArgb(255, 0, 0, 50);
	private readonly Color RainColor = Color.FromArgb(255, 90, 160, 190);
	private readonly Color SunColor = Color.FromArgb(255, 255, 250, 100);

	private Button[][] buttons;
	private int CurrentHour = 0;
	private Weather CurrentWeather = Weather.Clear;
	private int[] hovered = [-1, -1];

	private int[][] IconIds = [[]];

	private bool isMouseDown;
	private int[] selected = [-1, -1];
	private int[][] StatusIds = [[]];
	private Bitmap TileMap;

	public MapPanel(int[][]? Map = null, int[][]? statusIds = null)
	{
		IconIds = Map ?? [[]];
		StatusIds = statusIds ?? [[]];

		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
		UpdateStyles();
		Margin = new Padding(0);
		BackgroundImageLayout = ImageLayout.Stretch;

		Resize += (_, _) => { RefreshImage(); };

		MouseDown += (s, e) =>
		{
			if (e.Button == MouseButtons.Left) isMouseDown = true;
			OnClicked(s, e);
		};
		MouseUp += (_, e) =>
		{
			if (e.Button == MouseButtons.Left) isMouseDown = false;
		};
		MouseMove += MouseMoveEvent;
	}

	public void SetEvents(ref Action<int, int, int> setTileImage, ref Action<int[][], int[][]?> refreshAll,
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather, ref Action<int[]> timeRefresh)
	{
		setTileStatus += SetTileStatus;
		setTileImage += SetTileImage;
		refreshAll += RefreshAll;
		timeRefresh += TimeRefresh;
	}

	private void RefreshImage()
	{
		if (TileMap is null) return;
		BackgroundImage = GetTimeFilteredMap(HelperStuff.ResizeImage(TileMap, Width, Height, false));
	}

	private void TimeRefresh(int[] time)
	{
		CurrentHour = time[1];
		RefreshImage();
	}

	private Bitmap GetTimeFilteredMap(Bitmap normalMap)
	{
		using (var g = Graphics.FromImage(normalMap))
		{
			var darkColor = (CurrentHour) switch
			{
				0 => Color.FromArgb((150), DarkColor),
				1 => Color.FromArgb((150), DarkColor),
				2 => Color.FromArgb((140), DarkColor),
				3 => Color.FromArgb((140), DarkColor),
				4 => Color.FromArgb((130), DarkColor),
				5 => Color.FromArgb((120), DarkColor),
				6 => Color.FromArgb((90), DarkColor),
				7 => Color.FromArgb((50), DarkColor),
				8 => Color.FromArgb((20), DarkColor),
				9 => Color.FromArgb((10), DarkColor),
				10 => Color.FromArgb((8), DarkColor),
				11 => Color.FromArgb((6), DarkColor),
				12 => Color.FromArgb((4), DarkColor),
				13 => Color.FromArgb((2), DarkColor),
				14 => Color.FromArgb((2), DarkColor),
				15 => Color.FromArgb((4), DarkColor),
				16 => Color.FromArgb((6), DarkColor),
				17 => Color.FromArgb((8), DarkColor),
				18 => Color.FromArgb((10), DarkColor),
				19 => Color.FromArgb((20), DarkColor),
				20 => Color.FromArgb((50), DarkColor),
				21 => Color.FromArgb((90), DarkColor),
				22 => Color.FromArgb((120), DarkColor),
				23 => Color.FromArgb((130), DarkColor),
				_ => Color.FromArgb((140), DarkColor)
			};

			var sunColor = (CurrentHour) switch
			{
				0 => Color.FromArgb((0), SunColor),
				1 => Color.FromArgb((0), SunColor),
				2 => Color.FromArgb((0), SunColor),
				3 => Color.FromArgb((0), SunColor),
				4 => Color.FromArgb((0), SunColor),
				5 => Color.FromArgb((0), SunColor),
				6 => Color.FromArgb((10), SunColor),
				7 => Color.FromArgb((20), SunColor),
				8 => Color.FromArgb((30), SunColor),
				9 => Color.FromArgb((20), SunColor),
				10 => Color.FromArgb((10), SunColor),
				11 => Color.FromArgb((0), SunColor),
				12 => Color.FromArgb((0), SunColor),
				13 => Color.FromArgb((0), SunColor),
				14 => Color.FromArgb((0), SunColor),
				15 => Color.FromArgb((0), SunColor),
				16 => Color.FromArgb((0), SunColor),
				17 => Color.FromArgb((10), SunColor),
				18 => Color.FromArgb((20), SunColor),
				19 => Color.FromArgb((30), SunColor),
				20 => Color.FromArgb((20), SunColor),
				21 => Color.FromArgb((10), SunColor),
				22 => Color.FromArgb((0), SunColor),
				23 => Color.FromArgb((0), SunColor),
				_ => Color.FromArgb((0), SunColor)
			};

			using (Brush brush = new SolidBrush(darkColor))
			{
				g.FillRectangle(brush, new Rectangle(0, 0, normalMap.Width, normalMap.Height));
			}

			using (Brush brush = new SolidBrush(sunColor))
			{
				g.FillRectangle(brush, new Rectangle(0, 0, normalMap.Width, normalMap.Height));
			}
		}

		return normalMap;
	}

	private void SetTileImage(int x, int y, int id)
	{
		IconIds[y][x] = id;
		UpdateTileAt(x, y);
	}

	private void SetTileStatus(int x, int y, int status)
	{
		StatusIds[y][x] = status;
		UpdateTileAt(x, y);
	}

	private void RefreshAll(int[][] iconIds, int[][]? statusIds = null)
	{
		var bitmap = new Bitmap(iconIds[0].Length * TileSize, iconIds.Length * TileSize);

		using (var g = Graphics.FromImage(bitmap))
		{
			// Clear the canvas with a background color
			g.Clear(Color.Silver);

			for (var y = 0; y < iconIds.Length; y++)
			{
				for (var x = 0; x < iconIds[y].Length; x++)
				{
					var borderSize = 0;
					if (GlobalVariableManager.settings.Grid)
					{
						borderSize = 1;
					}

					Image tileImage = HelperStuff.ResizeImage(
						BasicGuiManager.TileIcons?[IconIds[y][x]] ?? BasicGuiManager.NO_IMAGE_ICON,
						TileSize - borderSize * 2, TileSize - borderSize * 2, false);

					g.DrawImage(tileImage, x * TileSize + borderSize, y * TileSize + borderSize,
						TileSize - borderSize * 2,
						TileSize - borderSize * 2);
				}
			}
		}

		TileMap = bitmap;
		RefreshImage();
	}

	private void UpdateTileAt(int x, int y)
	{
		if (x == -1 || y == -1) return;

		var bitmap = TileMap;

		using (var g = Graphics.FromImage(bitmap))
		{
			g.Clip = new Region(new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize));

			var borderSize = 0;
			var borderColor = Color.Silver;
			if (GlobalVariableManager.settings.Grid)
			{
				borderSize = 2;
			}

			if (selected[0] == y && selected[1] == x)
			{
				borderColor = Color.Red;
				borderSize = 4;
			}
			else if (hovered[0] == y && hovered[1] == x)
			{
				borderColor = Color.Yellow;
				borderSize = 2;
			}

			g.Clear(borderColor);

			Image tileImage = HelperStuff.ResizeImage(
				BasicGuiManager.TileIcons?[IconIds[y][x]] ?? BasicGuiManager.NO_IMAGE_ICON, TileSize - borderSize * 2,
				TileSize - borderSize * 2, false);

			g.DrawImage(tileImage, (x * TileSize) + borderSize, (y * TileSize) + borderSize, TileSize - borderSize * 2,
				TileSize - borderSize * 2);
		}

		TileMap = bitmap;
		RefreshImage();
	}

	private static string GetStatusText(int? id)
	{
		return (id) switch
		{
			1 => "🚧", // 1 = construction
			2 => "Ⓧ", //  2 = disabled
			_ => "", // 0, null, or other = normal
		};
	}

	private static Color GetStatusColor(int? id)
	{
		return (id) switch
		{
			1 => Color.Orange, // 1 = construction
			2 => Color.Red, //  2 = disabled
			_ => Color.Black, // 0, null, or other = normal
		};
	}

	internal event Action<int, int> MapButtonClicked;

	private void OnClicked(object sender, MouseEventArgs e)
	{
		var oldSelected = selected;
		selected = GetTileFromPoint(e.Location);
		if (selected[0] == -1 && selected[1] == -1) return;
		MapButtonClicked?.Invoke(selected[0], selected[1]);
		UpdateTileAt(selected[1], selected[0]);
		UpdateTileAt(oldSelected[1], oldSelected[0]);
	}

	private void MouseMoveEvent(object sender, MouseEventArgs e)
	{
		var oldHover = hovered;

		if (!ClientRectangle.Contains(e.Location))
		{
			hovered = [-1, -1];
			UpdateTileAt(oldHover[1], oldHover[0]);
		}

		var cur = GetTileFromPoint(e.Location);
		if (hovered[0] == cur[0] && hovered[1] == cur[1]) return;
		hovered = cur;
		if (isMouseDown)
		{
			var oldSelected = selected;
			selected = cur;
			if (selected[0] == -1 && selected[1] == -1) return;
			MapButtonClicked?.Invoke(selected[0], selected[1]);
			UpdateTileAt(oldSelected[1], oldSelected[0]);
		}

		UpdateTileAt(oldHover[1], oldHover[0]);
		UpdateTileAt(hovered[1], hovered[0]);
	}

	private int[] GetTileFromPoint(Point p)
	{
		if ((Width / IconIds[0].Length) == 0 || (Height / IconIds.Length) == 0) return [-1, -1];

		//get the button that is hovered over:
		var hoveredOver = new Point(p.X / (Width / IconIds[0].Length), p.Y / (Height / IconIds.Length));

		//check if the new location is valid:
		if (hoveredOver.X < 0 || hoveredOver.Y < 0 || hoveredOver.Y >= IconIds.Length ||
		    hoveredOver.X >= IconIds[0].Length) return [-1, -1];

		return [hoveredOver.Y, hoveredOver.X];
	}

	internal enum Weather
	{
		Clear,
		Rainy,
	}
}