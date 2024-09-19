using System.Drawing.Drawing2D;
using Point = System.Drawing.Point;
using Timer = System.Threading.Timer;

namespace Tiles;

//TODO: find out why a lot of memory was being used. (unknown, fixed with new rendering logic)
//TODO: small memory leak in rendering! ~ 10MB per second when hovering.
public sealed class MapPanel : Panel
{
	public enum Weather
	{
		Clear,
		Sprinkle,
		Rainy,
		Stormy,
	}

	private const int TileSize = 32;

	private readonly Color DarkFilterColor = Color.FromArgb(255, 0, 0, 50);
	private readonly Color RainDropColor = Color.FromArgb(200, 90, 160, 210);
	private readonly Color RainFilterColor = Color.FromArgb(255, 90, 140, 180);
	private readonly Color SunFilterColor = Color.FromArgb(255, 255, 250, 100);
	private bool _imageChanged;

	private bool _inPaint;

	private bool _inTick;
	private bool _isOver;

	private Button[][] buttons;
	private Bitmap CombinedMap;
	private Graphics CombinedMapGraphics;
	private int CurrentHour = 0;
	private Weather CurrentWeather = Weather.Clear;
	private Bitmap FilterMap;
	private Brush FilterMapBrush;
	private Graphics FilterMapGraphics;
	private int GameSpeed;

	private Graphics graphics;
	private int[] hovered = [-1, -1];
	private Bitmap[] HoveredBorderTileIcons;

	private int[][] IconIds = [[]];

	private bool isMouseDown;
	private Bitmap[] MapSizedTileIcons;
	private Pen RainDropPen;
	private HashSet<RainDrop> RainDrops = [];

	private Brush RainFilt = new SolidBrush(Color.Transparent);

	private bool refeshing;
	private int[] selected = [-1, -1];
	private Bitmap[] SelectedBorderTileIcons;
	private int[][] StatusIds = [[]];
	private Brush SunFilt = new SolidBrush(Color.Transparent);

	private int tick;
	private Bitmap TileMap;

	private Graphics TileMapGraphics;
	private Brush TimeFilt = new SolidBrush(Color.Transparent);
	private Bitmap WeatherMap;
	private Graphics WeatherMapGraphics;

	private Timer WeatherTimer;

	public MapPanel(int[][]? map = null, int[][]? statusIds = null)
	{
		//resize the images once before use:
		MapSizedTileIcons = new Bitmap[BasicGuiManager.TileIcons.Length];
		SelectedBorderTileIcons = new Bitmap[BasicGuiManager.TileIcons.Length];
		HoveredBorderTileIcons = new Bitmap[BasicGuiManager.TileIcons.Length];


		for (var i = 0; i < BasicGuiManager.TileIcons.Length; i++)
		{
			var icon = new Bitmap(TileSize, TileSize);
			using (var tileImgCreator = Graphics.FromImage(icon))
			{
				tileImgCreator.InterpolationMode = InterpolationMode.HighQualityBicubic;
				tileImgCreator.PixelOffsetMode = PixelOffsetMode.HighQuality;
				tileImgCreator.CompositingQuality = CompositingQuality.HighQuality;

				if (GlobalVariableManager.settings.Grid)
				{
					tileImgCreator.Clear(Color.Silver);
					tileImgCreator.DrawImage(
						BasicGuiManager.TileIcons?[i] ?? BasicGuiManager.NO_IMAGE_ICON,
						1, 1, TileSize - 2, TileSize - 2);
					SelectedBorderTileIcons[i] = (Bitmap)icon.Clone();
				}
				else
				{
					MapSizedTileIcons[i] = HelperStuff.ResizeImage(
						BasicGuiManager.TileIcons?[i] ?? BasicGuiManager.NO_IMAGE_ICON,
						TileSize, TileSize, false);
				}

				tileImgCreator.Clear(Color.Red);
				tileImgCreator.DrawImage(
					BasicGuiManager.TileIcons?[i] ?? BasicGuiManager.NO_IMAGE_ICON,
					2, 2, TileSize - 4, TileSize - 4);
				SelectedBorderTileIcons[i] = (Bitmap)icon.Clone();

				tileImgCreator.Clear(Color.Yellow);
				tileImgCreator.DrawImage(
					BasicGuiManager.TileIcons?[i] ?? BasicGuiManager.NO_IMAGE_ICON,
					1, 1, TileSize - 2, TileSize - 2);
				HoveredBorderTileIcons[i] = icon;
			}
		}

		//GC.Collect();

		IconIds = map ?? [[]];
		StatusIds = statusIds ?? [[]];

		DoubleBuffered = true;
		SetStyle(
			ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

		UpdateStyles();
		Margin = new Padding(0);
		BackgroundImageLayout = ImageLayout.Stretch;

		//Resize += async (_, _) => { await RefreshImage(); };

		MouseDown += (s, e) =>
		{
			if (e.Button == MouseButtons.Left) isMouseDown = true;
			OnClicked(s, e);
		};
		MouseUp += (_, e) =>
		{
			if (e.Button == MouseButtons.Left) isMouseDown = false;
		};

		MouseLeave += async (_, _) =>
		{
			MouseMove -= MouseMoveEvent;
			var oldHover = hovered;
			hovered = [-1, -1];
			await UpdateTileAt(oldHover[1], oldHover[0]);
			OnPaint();
			_isOver = false;
		};
		MouseEnter += (_, _) =>
		{
			MouseMove += MouseMoveEvent;
			_isOver = true;
		};

		WeatherTimer = new Timer(WeatherTimerTick, null, 0, 50);
	}

	private async void WeatherTimerTick(object state)
	{
		if (_inTick)
		{
			return;
		}
		//if the tick is reached.
		else if (GameSpeed != 0 && tick >= 5 / GameSpeed)
		{
			_inTick = true;
			//reset tick
			tick = 0;

			if (CurrentWeather is Weather.Rainy or Weather.Stormy or Weather.Sprinkle || RainDrops.Count > 0)
			{
				var maxDrops = (CurrentWeather) switch
				{
					Weather.Sprinkle => 600,
					Weather.Rainy => 400,
					Weather.Stormy => 200,
					_ => 0,
				};
				var dropsPerTick = (CurrentWeather) switch
				{
					Weather.Sprinkle => 1,
					Weather.Rainy => 2,
					Weather.Stormy => 10,
					_ => 0,
				};
				var dropSize = (CurrentWeather) switch
				{
					Weather.Sprinkle => 5,
					Weather.Rainy => 10,
					Weather.Stormy => 15,
					_ => 1,
				};

				if (RainDrops.Count < maxDrops)
				{
					for (var i = 0; i <= dropsPerTick; i++)
					{
						var topY = 0;
						var topX = Random.Shared.Next(0, Width + Height);
						if (topX >= Width)
						{
							topY = topX - Width;
							topX = Width;
						}

						var Length = Random.Shared.Next(0, dropSize);
						RainDrops.Add(new RainDrop(new Point(topX, topY), new Point(topX - Length, topY + Length * 2),
							CurrentWeather));
					}
				}

				await RefreshWeather();
				OnPaint();
			}

			RainDrops
				.AsParallel()
				.ForAll(drop => drop.Move());

			RainDrops = RainDrops
				.AsParallel()
				.Where(drop => !(drop.TopPoint.Y > Height || drop.TopPoint.X > Width))
				.ToHashSet();

			_inTick = false;
		}
		//tick is not reached
		else if (_isOver && _imageChanged)
		{
			_inTick = true;
			OnPaint();
			tick++;
			_imageChanged = false;
			_inTick = false;
		}
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
		speedChange += (num) =>
		{
			if (GameSpeed == 0)
			{
				GameSpeed = num;
				OnPaint();
			}
			else if (num == 0)
			{
				GameSpeed = num;
				OnPaint();
			}
			else
			{
				GameSpeed = num;
			}
		};
		setWeather += async (id) =>
		{
			CurrentWeather = (Weather)id;

			var alpha = (CurrentWeather) switch
			{
				Weather.Sprinkle => 50,
				Weather.Rainy => 100,
				Weather.Stormy => 150,
				_ => 0
			};

			RainFilt = new SolidBrush(Color.FromArgb(alpha, RainDropColor));

			await RefreshFilter();
			await RefreshWeather();
		};
	}

	internal async Task<Bitmap> Screenshot()
	{
		Bitmap clonedImg;

		lock (WeatherMap)
		{
			clonedImg = (Bitmap)WeatherMap.Clone();
		}

		return clonedImg;
	}

	private async void TimeRefresh(int[] time)
	{
		CurrentHour = time[1];

		//TODO: move to a not hard coded method
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

		SunFilt = new SolidBrush(sunColor);
		TimeFilt = new SolidBrush(darkColor);

		await RefreshFilter();
	}

	private async void SetTileImage(int x, int y, int id)
	{
		IconIds[y][x] = id;
		await UpdateTileAt(x, y);
		OnPaint();
	}

	private async void SetTileStatus(int x, int y, int status)
	{
		StatusIds[y][x] = status;
		await UpdateTileAt(x, y);
		OnPaint();
	}

	private async void RefreshAll(int[][] iconIds, int[][]? statusIds = null)
	{
		TileMap = new Bitmap(TileSize * iconIds[0].Length, TileSize * iconIds.Length);
		FilterMap = new Bitmap(TileMap.Width, TileMap.Height);
		WeatherMap = new Bitmap(TileMap.Width, TileMap.Height);
		CombinedMap = new Bitmap(TileMap.Width, TileMap.Height);

		TileMapGraphics = Graphics.FromImage(TileMap);
		WeatherMapGraphics = Graphics.FromImage(WeatherMap);
		FilterMapGraphics = Graphics.FromImage(FilterMap);
		CombinedMapGraphics = Graphics.FromImage(CombinedMap);

		RainDropPen ??= new Pen(RainDropColor);

		for (var y = 0; y < IconIds.Length; y++)
		{
			for (var x = 0; x < IconIds[y].Length; x++)
			{
				await UpdateTileAt(x, y);
			}
		}

		Parent.BackgroundImage = TileMap;
		Parent.BackgroundImageLayout = ImageLayout.Stretch;

		OnPaint();
	}

	private async Task RefreshFilter()
	{
		lock (FilterMapGraphics)
		{
			FilterMapGraphics.Clear(Color.Transparent);

			lock (FilterMap)
			{
				lock (TimeFilt)
				{
					FilterMapGraphics.FillRectangle(TimeFilt, new Rectangle(0, 0, FilterMap.Width, FilterMap.Height));
				}

				lock (SunFilt)
				{
					FilterMapGraphics.FillRectangle(SunFilt, new Rectangle(0, 0, FilterMap.Width, FilterMap.Height));
				}

				lock (RainFilt)
				{
					FilterMapGraphics.FillRectangle(RainFilt, new Rectangle(0, 0, FilterMap.Width, FilterMap.Height));
				}
			}
		}
	}

	private async Task RefreshWeather()
	{
		//save to prevent any modifications
		var rainDrops = RainDrops.ToArray();
		lock (WeatherMapGraphics)
		{
			WeatherMapGraphics.Clear(Color.Transparent);

			foreach (var drop in rainDrops)
			{
				WeatherMapGraphics.DrawLine(RainDropPen, drop.TopPoint, drop.BottomPoint);
			}
		}
	}

	private async Task UpdateTileAt(int x, int y)
	{
		if (x == -1 || y == -1) return;

		Bitmap tileImage;

		if (selected[0] == y && selected[1] == x)
		{
			tileImage = SelectedBorderTileIcons[IconIds[y][x]];
		}
		else if (hovered[0] == y && hovered[1] == x)
		{
			tileImage = HoveredBorderTileIcons[IconIds[y][x]];
		}
		else
		{
			tileImage = MapSizedTileIcons[IconIds[y][x]];
		}

		lock (TileMap)
		{
			TileMapGraphics.DrawImage(tileImage, x * TileSize, y * TileSize, TileSize, TileSize);
		}
	}

	protected override void OnPaint(PaintEventArgs? e = null)
	{
		if (_inPaint)
		{
			return;
		}

		if (InvokeRequired)
		{
			Invoke(() => OnPaint(e));
			return;
		}

		_inPaint = true;

		if (e is not null)
		{
			graphics = e.Graphics;
			graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			graphics.CompositingQuality = CompositingQuality.HighSpeed;
		}

		try
		{
			graphics.IsVisible(0, 0);
		}
		catch
		{
			graphics = Graphics.FromHwnd(Handle);
			graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			graphics.CompositingQuality = CompositingQuality.HighSpeed;
		}

		if (GameSpeed == 0)
		{
			graphics.Clear(Color.LightSkyBlue);
		}

		lock (CombinedMapGraphics)
		{
			lock (TileMap)
			{
				CombinedMapGraphics.DrawImage(TileMap, 0, 0, TileMap.Width, TileMap.Height);
			}

			lock (FilterMap)
			{
				CombinedMapGraphics.DrawImage(FilterMap, 0, 0, FilterMap.Width, FilterMap.Height);
			}

			lock (WeatherMap)
			{
				CombinedMapGraphics.DrawImage(WeatherMap, 0, 0, WeatherMap.Width, WeatherMap.Height);
			}

			graphics.DrawImage(
				CombinedMap,
				GameSpeed == 0 ? 5 : 0,
				GameSpeed == 0 ? 5 : 0,
				Width - (GameSpeed == 0 ? 10 : 0),
				Height - (GameSpeed == 0 ? 10 : 0)
			);
		}

		_inPaint = false;
	}

	//TODO: make the status text display: (can be custom icons) Disabled = black border + black X. Construction = orange border + 2 orange lines on top and bottom
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

	private async void OnClicked(object sender, MouseEventArgs e)
	{
		var oldSelected = selected;
		selected = GetTileFromPoint(e.Location);
		if (selected[0] == -1 && selected[1] == -1) return;
		MapButtonClicked?.Invoke(selected[0], selected[1]);
		await UpdateTileAt(selected[1], selected[0]);
		await UpdateTileAt(oldSelected[1], oldSelected[0]);
		_imageChanged = true;
	}

	private async void MouseMoveEvent(object sender, MouseEventArgs e)
	{
		var oldHover = hovered;

		if (!ClientRectangle.Contains(e.Location))
		{
			hovered = [-1, -1];
			await UpdateTileAt(oldHover[1], oldHover[0]);
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
			await UpdateTileAt(oldSelected[1], oldSelected[0]);
		}

		await UpdateTileAt(oldHover[1], oldHover[0]);
		await UpdateTileAt(hovered[1], hovered[0]);
		_imageChanged = true;
	}

	private int[] GetTileFromPoint(Point p)
	{
		if ((Width / IconIds[0].Length) == 0 || (Height / IconIds.Length) == 0) return [-1, -1];

		//get the button that is hovered over:
		var hoveredOver = new Point((int)(p.X / (Width / (double)IconIds[0].Length)),
			(int)(p.Y / (Height / (double)IconIds.Length)));

		//check if the new location is valid:
		if (hoveredOver.X < 0 || hoveredOver.Y < 0 || hoveredOver.Y >= IconIds.Length ||
		    hoveredOver.X >= IconIds[0].Length) return [-1, -1];

		return [hoveredOver.Y, hoveredOver.X];
	}

	private class RainDrop(Point topPoint, Point bottomPoint, Weather StartWeather)
	{
		public Point BottomPoint = bottomPoint;
		public Point TopPoint = topPoint;

		public void Move()
		{
			switch (StartWeather) //TODO: weird rain stuff
			{
				case Weather.Sprinkle:
					BottomPoint.X -= 6;
					BottomPoint.Y += 7;
					TopPoint.X -= 6;
					TopPoint.Y += 7;
					break;
				case Weather.Rainy:
					BottomPoint.X -= 19;
					BottomPoint.Y += 19;
					TopPoint.X -= 18;
					TopPoint.Y += 18;
					break;
				case Weather.Stormy:
					BottomPoint.X -= 38;
					BottomPoint.Y += 34;
					TopPoint.X -= 36;
					TopPoint.Y += 32;
					break;
				default:
					BottomPoint.X -= 6;
					BottomPoint.Y += 5;
					TopPoint.X -= 5;
					TopPoint.Y += 4;
					break;
			}
		}
	}
}