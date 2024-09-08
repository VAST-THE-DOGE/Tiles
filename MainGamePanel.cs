namespace Tiles;

public partial class MainGamePanel : StandardBackgroundControl
{
	private MapPanel _mapPanel;


	public MainGamePanel()
	{
		DoubleBuffered = true;

		InitializeComponent();
	}

	internal event Action<bool> RequestFreezeUpdate;
	internal event Action<int> GameSpeedUpdate;
	internal event Action<int> UpgradeTileRequest;
	internal event Action SaveRequest;
	internal event Action MenuRequest;
	internal event Action ToggleTileRequest;
	internal event Action<int, int> TileClicked;

	public void Initialize(ref Action<long[], int[]> resourceFire, ref Action<int[]> timeFire,
		ref Action<bool> savedFire, ref Action<int, int, int> setTile, ref Action<int[][], int[][]?> refreshAll,
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather, ref Action<int> setSpeed,
		ref Action<int, int?, int?> updateSelected, ref Action<bool> freezeTime, World world)
	{
		//set outgoing events:
		ucBottomPanel1.Initialize(ref resourceFire, ref timeFire, ref savedFire);

		ucRightPanel1.Initialize(ref setSpeed, ref updateSelected, ref freezeTime, ref resourceFire);

		_mapPanel = new MapPanel(world.Map, world.TileStatus);
		_mapPanel.SetEvents(ref setTile, ref refreshAll, ref setTileStatus, ref setWeather, ref timeFire);

		//TODO add setup right panel (so many event, so ignore for now).

		//set incoming events:
		_mapPanel.MapButtonClicked += (x, y) => { TileClicked.Invoke(x, y); };
		ucBottomPanel1.SaveRequested += () => { SaveRequest?.Invoke(); };

		MapAreaPanel.Controls.Add(_mapPanel);
		_mapPanel.Dock = DockStyle.Fill;

		ucRightPanel1.ButtonClicked += HandleRightPanelClicks;
		ucRightPanel1.SpeedChangeRequest += GameSpeedUpdate;

		this.SetAllControlImages();
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