namespace Tiles;

public partial class MainGamePanel : UserControl
{
	private MapPanel _mapPanel;


	public MainGamePanel()
	{
		InitializeComponent();
	}

	internal event Action<bool> RequestFreezeUpdate;
	internal event Action<int> GameSpeedUpdate;
	internal event Action<int> UpgradeTileRequest;
	internal event Action SaveRequest;
	internal event Action MenuRequest;
	internal event Action DisabelTileRequest;
	internal event Action<int, int> TileClicked;

	public void Initialize(ref Action<int[], int[]> resourceFire, ref Action<int[]> timeFire,
		ref Action<bool> savedFire, ref Action<int, int, int> setTile, ref Action<int[][], int[][]?> refreshAll,
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather, ref Action<bool> FreezeTime,
		ref Action<int> SetTime)
	{
		//set incoming events:
		ucBottomPanel1.Initialize(GlobalVariableManager.ResourceNames, GlobalVariableManager.ResourceColors,
			ref resourceFire, ref timeFire, ref savedFire);

		_mapPanel = new MapPanel();
		_mapPanel.SetEvents(ref setTile, ref refreshAll, ref setTileStatus, ref setWeather);

		//TODO add setup right panel (so many event, so ignore for now).

		//set outgoing events:
		_mapPanel.MapButtonClicked += (x, y) => { TileClicked.Invoke(x, y); };
		ucBottomPanel1.SaveRequested += () => { SaveRequest?.Invoke(); };

		splitContainer2.Panel1.Controls.Add(_mapPanel);
		_mapPanel.Dock = DockStyle.Fill;
	}
}