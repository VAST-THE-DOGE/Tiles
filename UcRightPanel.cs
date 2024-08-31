namespace Tiles;

public partial class UcRightPanel : StandardBackgroundControl
{
	public enum MenuButtons
	{
		Info,
		ToggleTile,
		Upgrade1,
		Upgrade2,
		Upgrade3,
		Settings,
		BackToMenu,
		Diplomacy,
		Leader,
		News,
		Laws,
		Trading,
		Research,
	}

	private int CurrentTileStatus;
	private long[] Resources;
	private Tile SelecteedTile;

	//private bool EditMode = false;

	public UcRightPanel()
	{
		InitializeComponent();

		Resize += HandleResize;
	}

	public event Action<MenuButtons> ButtonClicked;
	public event Action<int> SpeedChangeRequest;

	public void Initialize(ref Action<int> setSpeed, ref Action<int, int?, int?> updateSelected,
		ref Action<bool> freezeTime, ref Action<long[], int[]> resourceFire)
	{
		UpdateSelectedTile(0);

		freezeTime += FreezeTime;
		updateSelected += UpdateSelectedTile;
		setSpeed += SetSpeed;
		resourceFire += (resources, _) =>
		{
			Resources = resources;
			RefreshUpgradeButtons();
		};

		Speed0Button.Click += (_, _) => SpeedChangeRequest.Invoke(0);
		Speed1Button.Click += (_, _) => SpeedChangeRequest.Invoke(1);
		Speed2Button.Click += (_, _) => SpeedChangeRequest.Invoke(2);
		Speed3Button.Click += (_, _) => SpeedChangeRequest.Invoke(3);
		Speed4Button.Click += (_, _) => SpeedChangeRequest.Invoke(4);

		InfoButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Info);
		DisableButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.ToggleTile);
		SettingsButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Settings);
		MenuButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.BackToMenu);
		DiplomacyButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Diplomacy);
		LeaderButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Leader);
		NewsButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.News);
		LawsButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Laws);
		TradingButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Trading);
		ResearchButton.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Research);

		Upgrade1Button.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Upgrade1);
		Upgrade2Button.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Upgrade2);
		Upgrade3Button.Click += (_, _) => ButtonClicked.Invoke(MenuButtons.Upgrade3);
	}

	private void HandleResize(object sender, EventArgs e)
	{
		//TODO: size entire panel to have the tile image as a square!

		ImgSelected.Image =
			HelperStuff.ResizeImage((Bitmap)ImgSelected.Image, ImgSelected.Width, ImgSelected.Height, false);
		ImgUpgrade1.Image =
			HelperStuff.ResizeImage((Bitmap)ImgUpgrade1.Image, ImgUpgrade1.Width, ImgUpgrade1.Height, false);
		ImgUpgrade2.Image =
			HelperStuff.ResizeImage((Bitmap)ImgUpgrade2.Image, ImgUpgrade2.Width, ImgUpgrade2.Height, false);
		ImgUpgrade3.Image =
			HelperStuff.ResizeImage((Bitmap)ImgUpgrade3.Image, ImgUpgrade3.Width, ImgUpgrade3.Height, false);

		tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Absolute, Speed0Button.Width);
	}

	private void RefreshUnlockedButtons(bool[] unlockedButtons)
	{
		//TODO: make this system when needed
	}

	private void SetSpeed(int speed)
	{
		Speed0Button.ForeColor = Color.Black;
		Speed1Button.ForeColor = Color.Black;
		Speed2Button.ForeColor = Color.Black;
		Speed3Button.ForeColor = Color.Black;
		Speed4Button.ForeColor = Color.Black;

		switch (speed)
		{
			case 0:
				Speed0Button.ForeColor = Color.Yellow;
				break;
			case 1:
				Speed1Button.ForeColor = Color.Yellow;
				break;
			case 2:
				Speed2Button.ForeColor = Color.Yellow;
				break;
			case 3:
				Speed3Button.ForeColor = Color.Yellow;
				break;
			case 4:
				Speed4Button.ForeColor = Color.Yellow;
				break;
		}
	}

	private void FreezeTime(bool freeze)
	{
		//TODO
	}

	private void UpdateSelectedTile(int tileId, int? status = null, int? timer = null)
	{
		SelecteedTile = GlobalVariableManager.tileInfo[tileId];

		ImgSelected.Image = HelperStuff.ResizeImage(BasicGuiManager.TileIcons?[tileId] ?? BasicGuiManager.NO_IMAGE_ICON,
			ImgSelected.Width, ImgSelected.Height, false);

		SetUpgradeImage(ImgUpgrade1, 0);
		SetUpgradeImage(ImgUpgrade2, 1);
		SetUpgradeImage(ImgUpgrade3, 2);

		RefreshUpgradeButtons();

		return;

		void SetUpgradeImage(PictureBox box, int num)
		{
			if (SelecteedTile.Upgrades[num] is not -1)
				box.Image = HelperStuff.ResizeImage(BasicGuiManager.TileIcons?[SelecteedTile.Upgrades[num]] ??
				                                    BasicGuiManager.NO_IMAGE_ICON, box.Width, box.Height, false);
			else
				box.Image = HelperStuff.ResizeImage(BasicGuiManager.NO_IMAGE_ICON, box.Width, box.Height, false);
		}
	}

	private void RefreshUpgradeButtons()
	{
		Upgrade1Button.ForeColor = SelecteedTile.Upgrades[0] != -1
			? (CheckResources(SelecteedTile.Cost1) ? Color.Yellow : Color.Black)
			: Color.Gray;
		Upgrade2Button.ForeColor = SelecteedTile.Upgrades[1] != -1
			? (CheckResources(SelecteedTile.Cost2) ? Color.Yellow : Color.Black)
			: Color.Gray;
		Upgrade3Button.ForeColor = SelecteedTile.Upgrades[2] != -1
			? (CheckResources(SelecteedTile.Cost3) ? Color.Yellow : Color.Black)
			: Color.Gray;

		return;

		bool CheckResources(int[] other)
		{
			return !other.Where((t, i) => Resources[i] - t < 0).Any();
		}
	}
}