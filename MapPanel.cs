namespace Tiles;

public class MapPanel : PictureBox
{
	private const int TileSize = 64;

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

		Resize += (_, _) => { BackgroundImage = HelperStuff.ResizeImage(TileMap, Width, Height, false); };

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
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather)
	{
		setTileStatus += SetTileStatus;
		setTileImage += SetTileImage;
		refreshAll += RefreshAll;
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
		BackgroundImage = HelperStuff.ResizeImage(TileMap, Width, Height, false);
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
		BackgroundImage = HelperStuff.ResizeImage(TileMap, Width, Height, false);
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