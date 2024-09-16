namespace Tiles;

public partial class MainEditPanel : StandardBackgroundControl
{
	private MapPanel _mapPanel;


	public MainEditPanel()
	{
		DoubleBuffered = true;

		InitializeComponent();
	}

	internal event Action<bool> RequestFreezeUpdate;
	internal event Action<int> GameSpeedUpdate;
	internal event Action<int> UpgradeTileRequest;
	internal event Action<int, int, int> SetSelectedToIdRequest;
	internal event Action<bool> SaveRequest;
	internal event Action MenuRequest;
	internal event Action ToggleTileRequest;
	internal event Action<int, int> TileClicked;

	private event Action<bool> SetAuto;
	private event Action<int[]> SetTime;
	private bool _isAuto;

	public void Initialize(ref Action<long[], int[]> resourceFire, ref Action<int[]> timeFire,
		ref Action<bool, bool> savedFire, ref Action<int, int, int> setTile, ref Action<int[][], int[][]?> refreshAll,
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather, ref Action<int> setSpeed,
		ref Action<int, int?, int?> updateSelected, ref Action<bool> freezeTime, World world)
	{
		//set outgoing events:
		ucBottomPanel1.Initialize(ref timeFire, ref savedFire, ref SetAuto);

		ucRightPanel1.Initialize(ref setSpeed, ref updateSelected, ref freezeTime, ref resourceFire);
		ucRightPanel1.SpeedChangeRequest += _ => { GameSpeedUpdate?.Invoke(0); }; 

		_mapPanel = new MapPanel(world.Map, world.TileStatus);
		timeFire += a => { SetTime.Invoke([a[0], 12]);};
		_mapPanel.SetEvents(ref setTile, ref refreshAll, ref setTileStatus, ref setWeather, ref SetTime, ref setSpeed);
		
		//set incoming events:
		_mapPanel.MapButtonClicked += (x, y) => { 
			TileClicked.Invoke(x, y);
			if (_isAuto) SetSelectedToIdRequest(x, y, ucBottomPanel1.NewTileId);
		};
		ucBottomPanel1.SaveRequested += b => { SaveRequest?.Invoke(b); };
		ucBottomPanel1.AutoSetClicked += () =>
		{
			_isAuto = !_isAuto;
			SetAuto?.Invoke(_isAuto); 
		};

		MapAreaPanel.Controls.Add(_mapPanel);
		_mapPanel.Dock = DockStyle.Fill;

		ucRightPanel1.ButtonClicked += HandleRightPanelClicks;
		ucRightPanel1.SpeedChangeRequest += GameSpeedUpdate;

		this.SetAllControlImages();
	}

	internal async Task<Bitmap> ScreenshotMap()
	{
		return await _mapPanel.Screenshot();
	}

	private void HandleRightPanelClicks(UcRightPanel.MenuButtons button)
	{
		switch (button)
		{
			case UcRightPanel.MenuButtons.Info:
				break;
			case UcRightPanel.MenuButtons.ToggleTile:
				ToggleTileRequest?.Invoke();
				break;
			case UcRightPanel.MenuButtons.Upgrade1:
				UpgradeTileRequest?.Invoke(1);
				break;
			case UcRightPanel.MenuButtons.Upgrade2:
				UpgradeTileRequest?.Invoke(2);
				break;
			case UcRightPanel.MenuButtons.Upgrade3:
				UpgradeTileRequest?.Invoke(3);
				break;
			case UcRightPanel.MenuButtons.Settings:
				break;
			case UcRightPanel.MenuButtons.BackToMenu:
				MenuRequest?.Invoke();
				break;
			case UcRightPanel.MenuButtons.Diplomacy:
				break;
			case UcRightPanel.MenuButtons.Leader:
				break;
			case UcRightPanel.MenuButtons.News:
				break;
			case UcRightPanel.MenuButtons.Laws:
				break;
			case UcRightPanel.MenuButtons.Trading:
				break;
			case UcRightPanel.MenuButtons.Research:
				break;
		}
	}
}