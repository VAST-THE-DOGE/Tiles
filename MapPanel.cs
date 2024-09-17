using System.Drawing.Drawing2D;
using Point = System.Drawing.Point;
using Timer = System.Threading.Timer;

namespace Tiles;

//TODO: find out why a lot of memory was being used. (unknown, fixed with new rendering logic)
//TODO: small memory leak in rendering! ~ 10MB per second when hovering.
public sealed class MapPanel : Panel 
{
	private const int TileSize = 32;

	private readonly Color DarkFilterColor = Color.FromArgb(255, 0, 0, 50);
	private readonly Color RainFilterColor = Color.FromArgb(255, 90, 140, 180);
	private readonly Color RainDropColor = Color.FromArgb(200, 90, 160, 210);
	private readonly Color SunFilterColor = Color.FromArgb(255, 255, 250, 100);
	
	private Timer WeatherTimer;
	private int GameSpeed;
	private Bitmap[] MapSizedTileIcons;
	private Bitmap[] SelectedBorderTileIcons;
	private Bitmap[] HoveredBorderTileIcons;

	private Button[][] buttons;
	private int CurrentHour = 0;
	private Weather CurrentWeather = Weather.Clear;
	private int[] hovered = [-1, -1];

	private int[][] IconIds = [[]];

	private bool isMouseDown;
	private int[] selected = [-1, -1];
	private int[][] StatusIds = [[]];

	private bool refeshing;
	private Bitmap TileMap;
	//{
	//	get 
	//	{
	//		//TODO: find a better way:
	//		while (refeshing)
	//		{
	//			continue;
	//		}
	//		refeshing = true;
	//		var returnImg = (Bitmap)_tileMap.Clone();
	//		refeshing = false;
	//		return returnImg;
	//	}
	//	set
	//	{
	//		refeshing = true;
	//		var oldRef = _tileMap;
	//		_tileMap = value;
	//		oldRef?.Dispose();
	//		refeshing = false;
//
	//	}
	//}

	private Graphics graphics;

	private Bitmap _tileMap = new(64, 64);

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
						1, 1,TileSize - 2,TileSize - 2);
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
					2, 2,TileSize - 4,TileSize - 4);
				SelectedBorderTileIcons[i] = (Bitmap)icon.Clone();
					
				tileImgCreator.Clear(Color.Yellow);
				tileImgCreator.DrawImage(
					BasicGuiManager.TileIcons?[i] ?? BasicGuiManager.NO_IMAGE_ICON,
					1, 1,TileSize - 2,TileSize - 2);
				HoveredBorderTileIcons[i] = icon;
			}
		}
		
		//GC.Collect();
		
		IconIds = map ?? [[]];
		StatusIds = statusIds ?? [[]];

		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.UserMouse | ControlStyles.ContainerControl, true);
		
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
			await RefreshImage();
		};
		MouseEnter += (_, _) =>
		{
			MouseMove += MouseMoveEvent;
		};
		
		WeatherTimer = new Timer(WeatherTimerTick, null, 0, 75);
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
					BottomPoint.X -= 2;
					BottomPoint.Y += 24;
					TopPoint.X -= 2;
					TopPoint.Y += 23;
					break;
			}
		}
	}

	private bool _inTick;
	private async void WeatherTimerTick(object state)
	{
		if (GameSpeed == 0 || _inTick)
		{
			return;
		}
		//if the tick is reached.
		else if (tick >= 5 / GameSpeed)
		{
			_inTick = true;
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
				
				await RefreshImage();
			}
			
			//do stuff here
			RainDrops
				.AsParallel()
				.ForAll(drop => drop.Move(CurrentWeather));
			
			RainDrops = RainDrops
				.AsParallel()
				.Where(drop => !(drop.TopPoint.Y > Height || drop.TopPoint.X > Width))
				.ToHashSet();
			
			// foreach (var drop in RainDrops)
			// {
			// 	drop.Move(CurrentWeather);
			// 	if (drop.TopPoint.Y > Height || drop.TopPoint.X > Width)
			// 	{
			// 		RainDrops.Remove(drop);
			// 	}
			// }
			
			_inTick = false;
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
		setWeather += (id) => { CurrentWeather = (Weather)id; };
	}
	
	private async Task RefreshImage() //TODO: LAG LAG LAG
	{
		OnPaint();
		return;
		
		if (TileMap is null) return;
		var tuple = await GetWeatherFilteredMap(
			await GetTimeFilteredMap(
				HelperStuff.ResizeImage(TileMap, Width, Height, true)
			)
		);
		var imageRef = BackgroundImage;
		BackgroundImage = tuple.Item1;
		
		this.Invoke(Refresh); //TODO: fix or create lag?
		
		imageRef?.Dispose();
		tuple.Item2.Dispose();
		//tuple.Item1.Dispose();
		
		GC.Collect(); //TODO: find the memory leak and remove this.
	}

	internal async Task<Bitmap> Screenshot()
	{
		if (TileMap is null) return BasicGuiManager.NO_IMAGE_ICON;
		var tuple = await GetWeatherFilteredMap(
			await GetTimeFilteredMap(
				HelperStuff.ResizeImage(TileMap, Width, Height, true)
			)
		);
		tuple.Item2.Dispose();
		return tuple.Item1;
	}

	private void TimeRefresh(int[] time)
	{
		CurrentHour = time[1];
		RefreshImage();
	}
	
	private async Task<Tuple<Bitmap, Graphics>> GetWeatherFilteredMap(Tuple<Bitmap, Graphics> tuple)
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
			tuple.Item2.FillRectangle(brush, new Rectangle(0, 0, tuple.Item1.Width, tuple.Item1.Height));
		}
		
		var pen = new Pen(RainDropColor);
		//pen.Width = 5;
		//save to prevent any modifications
		var rainDrops = RainDrops.ToArray();
		foreach (var drop in rainDrops)
		{
			tuple.Item2.DrawLine(pen, drop.TopPoint, drop.BottomPoint);
		}

		return tuple;
	}

	private async Task<Tuple<Bitmap, Graphics>> GetTimeFilteredMap(Bitmap normalMap)
	{
		var g = Graphics.FromImage(normalMap);
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

		return new Tuple<Bitmap, Graphics>(normalMap, g);
	}

	private async void SetTileImage(int x, int y, int id)
	{
		IconIds[y][x] = id;
		await UpdateTileAt(x, y);
		await RefreshImage();
	}

	private async void SetTileStatus(int x, int y, int status)
	{
		StatusIds[y][x] = status;
		await UpdateTileAt(x, y);
		await RefreshImage();
	}

	private Graphics TileMapGraphics;
	private async void RefreshAll(int[][] iconIds, int[][]? statusIds = null)
	{
		TileMap = new Bitmap(TileSize * iconIds[0].Length, TileSize * iconIds.Length);
		TileMapGraphics = Graphics.FromImage(TileMap);
		
		for (var y = 0; y < IconIds.Length; y++)
		{
			for (var x = 0; x < IconIds[y].Length; x++)
			{
				await UpdateTileAt(x, y);
			}
		}
		
		OnPaint();
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
				
		TileMapGraphics.DrawImage(tileImage, x * TileSize, y * TileSize, TileSize, TileSize);
	}

	//TODO: brushes: saved and reused
	//TODO: all drawing here:
	protected override void OnPaint(PaintEventArgs? e=null)
	{
		if (e is not null)
		{
			graphics = e.Graphics;
			graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			graphics.CompositingQuality = CompositingQuality.HighSpeed;
		}
		
		try
		{
			graphics.Clear(Color.Silver);
		}
		catch
		{
			graphics = Graphics.FromHwnd(Handle); //TODO: cross thread error (Fixed???)
			graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			graphics.CompositingQuality = CompositingQuality.HighSpeed;
			graphics.Clear(Color.Silver);
		}
		
		graphics.DrawImage(TileMap, 0, 0, Width, Height);
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

	private async void OnClicked(object sender, MouseEventArgs e)
	{
		var oldSelected = selected;
		selected = GetTileFromPoint(e.Location);
		if (selected[0] == -1 && selected[1] == -1) return;
		MapButtonClicked?.Invoke(selected[0], selected[1]);
		await UpdateTileAt(selected[1], selected[0]);
		await UpdateTileAt(oldSelected[1], oldSelected[0]);
		await RefreshImage();
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
		await RefreshImage();
	}

	private int[] GetTileFromPoint(Point p)
	{
		if ((Width / IconIds[0].Length) == 0 || (Height / IconIds.Length) == 0) return [-1, -1];

		//get the button that is hovered over:
		var hoveredOver = new Point((int)(p.X / (Width / (double)IconIds[0].Length)), (int)(p.Y / (Height / (double)IconIds.Length)));

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