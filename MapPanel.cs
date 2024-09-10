using Timer = System.Threading.Timer;

namespace Tiles;

public class MapPanel : PictureBox
{
	private const int TileSize = 64;

	private readonly Color DarkFilterColor = Color.FromArgb(255, 0, 0, 50);
	private readonly Color RainFilterColor = Color.FromArgb(255, 90, 140, 180);
	private readonly Color RainDropColor = Color.FromArgb(200, 90, 160, 210);
	private readonly Color SunFilterColor = Color.FromArgb(255, 255, 250, 100);
	
	private Timer WeatherTimer;
	private int GameSpeed;

	private Button[][] buttons;
	private int CurrentHour = 0;
	public Weather CurrentWeather = Weather.Clear;
	private int[] hovered = [-1, -1];

	private int[][] IconIds = [[]];

	private bool isMouseDown;
	private int[] selected = [-1, -1];
	private int[][] StatusIds = [[]];

	private Bitmap TileMap
	{
		get 
		{
			//TODO: find a better way:
			while (refeshing)
			{
				continue;
			}
			refeshing = true;
			var returnImg = (Bitmap)_tileMap.Clone();
			refeshing = false;
			return returnImg;
		} 
		set => _tileMap = value;
	}

	private Bitmap _tileMap = new Bitmap(64, 64);

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
		
		WeatherTimer = new Timer(WeatherTimerTick, null, 0, 5);
	}

	private int tick;
	private HashSet<RainDrop> RainDrops = [];

	private class RainDrop(Point topPoint, Point bottomPoint)
	{
		public Point TopPoint = topPoint;
		public Point BottomPoint = bottomPoint;

		public void Move(Weather currentWeather)
		{
			switch (currentWeather)
			{
				case Weather.Sprinkle:
					BottomPoint.X -= 2;
					BottomPoint.Y += 24;
					TopPoint.X -= 2;
					TopPoint.Y += 23;
					break;
				case Weather.Rainy:
					BottomPoint.X -= 16;
					BottomPoint.Y += 32;
					TopPoint.X -= 15;
					TopPoint.Y += 30;
					break;
				case Weather.Stormy:
					BottomPoint.X -= 108;
					BottomPoint.Y += 48;
					TopPoint.X -= 100;
					TopPoint.Y += 40;
					break;
				default:
					BottomPoint.X -= 0;
					BottomPoint.Y += 16;
					TopPoint.X -= 0;
					TopPoint.Y += 16;
					break;
			}
		}
	}
	private void WeatherTimerTick(object state)
	{
		if (GameSpeed == 0 || CurrentWeather == Weather.Clear)
		{
			return;
		}
		//if the tick is reached.
		else if (tick >= 5 / GameSpeed)
		{
			//reset tick
			tick = 0;

			if (CurrentWeather is Weather.Rainy or Weather.Stormy or Weather.Sprinkle)
			{
				var maxDrops = (CurrentWeather) switch
				{
					Weather.Sprinkle => 10,
					Weather.Rainy => 25,
					Weather.Stormy => 75,
					_ => 0,
				};
				var dropsPerTick = (CurrentWeather) switch
				{
					Weather.Sprinkle => 1,
					Weather.Rainy => 2,
					Weather.Stormy => 5,
					_ => 0,
				};
				if (RainDrops.Count < maxDrops)
				{
					for (var i = 0; i <= dropsPerTick; i++)
					{
						var topY = 0; 
						var topX = Random.Shared.Next(0, Width + Height);
						if (topX >= Width)
						{
							topY= topX - Width;
							topX =  Width;
						}
						var Length = Random.Shared.Next(0, 5);
						RainDrops.Add(new RainDrop(new Point(topX, topY), new Point(topX - Length, topY + Length * 2)));
					}
				}

				RefreshImage();
			}
			
			//do stuff here
			foreach (var drop in RainDrops)
			{
				drop.Move(CurrentWeather);
				if (drop.TopPoint.Y > Height || drop.TopPoint.X > Width)
				{
					RainDrops.Remove(drop);
				}
			}
		}
		//tick is not reached
		else
		{
			tick++;
		}
	}

	public void SetEvents(ref Action<int, int, int> setTileImage, ref Action<int[][], int[][]?> refreshAll,
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather, ref Action<int[]> timeRefresh,
		ref Action<int> speedChange)
	{
		setTileStatus += SetTileStatus;
		setTileImage += SetTileImage;
		refreshAll += RefreshAll;
		timeRefresh += TimeRefresh;
		speedChange += (num) => { GameSpeed = num; };
	}
	
	private bool refeshing;
	private void RefreshImage()
	{
		if (TileMap is null) return;
		BackgroundImage = GetWeatherFilteredMap(
			GetTimeFilteredMap(
				HelperStuff.ResizeImage(TileMap, Width, Height, false)
				)
			);
	}

	private void TimeRefresh(int[] time)
	{
		CurrentHour = time[1];
		RefreshImage();
	}
	
	private Bitmap GetWeatherFilteredMap(Bitmap normalMap)
	{
		using (var g = Graphics.FromImage(normalMap))
		{
			var alpha = (CurrentWeather) switch
			{
				Weather.Sprinkle => 50,
				Weather.Rainy => 100,
				Weather.Stormy => 150,
				_ => 0
			};
			var filter = Color.FromArgb(alpha, RainFilterColor);
			using (Brush brush = new SolidBrush(filter))
			{
				g.FillRectangle(brush, new Rectangle(0, 0, normalMap.Width, normalMap.Height));
			}
			
			var pen = new Pen(RainDropColor);
			//pen.Width = 5;
			//save to prevent any modifications
			var rainDrops = RainDrops.ToArray();
			foreach (var drop in rainDrops)
			{
				g.DrawLine(pen, drop.TopPoint, drop.BottomPoint);
			}
		}

		return normalMap;
	}

	private Bitmap GetTimeFilteredMap(Bitmap normalMap)
	{
		using (var g = Graphics.FromImage(normalMap))
		{
			var darkColor = (CurrentHour) switch
			{
				0 => Color.FromArgb((150), DarkFilterColor),
				1 => Color.FromArgb((150), DarkFilterColor),
				2 => Color.FromArgb((140), DarkFilterColor),
				3 => Color.FromArgb((140), DarkFilterColor),
				4 => Color.FromArgb((130), DarkFilterColor),
				5 => Color.FromArgb((120), DarkFilterColor),
				6 => Color.FromArgb((90), DarkFilterColor),
				7 => Color.FromArgb((50), DarkFilterColor),
				8 => Color.FromArgb((20), DarkFilterColor),
				9 => Color.FromArgb((10), DarkFilterColor),
				10 => Color.FromArgb((8), DarkFilterColor),
				11 => Color.FromArgb((6), DarkFilterColor),
				12 => Color.FromArgb((4), DarkFilterColor),
				13 => Color.FromArgb((2), DarkFilterColor),
				14 => Color.FromArgb((2), DarkFilterColor),
				15 => Color.FromArgb((4), DarkFilterColor),
				16 => Color.FromArgb((6), DarkFilterColor),
				17 => Color.FromArgb((8), DarkFilterColor),
				18 => Color.FromArgb((10), DarkFilterColor),
				19 => Color.FromArgb((20), DarkFilterColor),
				20 => Color.FromArgb((50), DarkFilterColor),
				21 => Color.FromArgb((90), DarkFilterColor),
				22 => Color.FromArgb((120), DarkFilterColor),
				23 => Color.FromArgb((130), DarkFilterColor),
				_ => Color.FromArgb((140), DarkFilterColor)
			};

			var sunColor = (CurrentHour) switch
			{
				0 => Color.FromArgb((0), SunFilterColor),
				1 => Color.FromArgb((0), SunFilterColor),
				2 => Color.FromArgb((0), SunFilterColor),
				3 => Color.FromArgb((0), SunFilterColor),
				4 => Color.FromArgb((0), SunFilterColor),
				5 => Color.FromArgb((0), SunFilterColor),
				6 => Color.FromArgb((10), SunFilterColor),
				7 => Color.FromArgb((20), SunFilterColor),
				8 => Color.FromArgb((30), SunFilterColor),
				9 => Color.FromArgb((20), SunFilterColor),
				10 => Color.FromArgb((10), SunFilterColor),
				11 => Color.FromArgb((0), SunFilterColor),
				12 => Color.FromArgb((0), SunFilterColor),
				13 => Color.FromArgb((0), SunFilterColor),
				14 => Color.FromArgb((0), SunFilterColor),
				15 => Color.FromArgb((0), SunFilterColor),
				16 => Color.FromArgb((0), SunFilterColor),
				17 => Color.FromArgb((10), SunFilterColor),
				18 => Color.FromArgb((20), SunFilterColor),
				19 => Color.FromArgb((30), SunFilterColor),
				20 => Color.FromArgb((20), SunFilterColor),
				21 => Color.FromArgb((10), SunFilterColor),
				22 => Color.FromArgb((0), SunFilterColor),
				23 => Color.FromArgb((0), SunFilterColor),
				_ => Color.FromArgb((0), SunFilterColor)
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
				borderSize = 1;
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

	//TODO: make the status text display:
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

	public enum Weather
	{
		Clear,
		Sprinkle,
		Rainy,
		Stormy,
	}
}