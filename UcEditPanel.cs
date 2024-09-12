namespace Tiles;

public partial class UcEditPanel : StandardBackgroundControl
{
	internal event Action EditTimeRequest;
	internal event Action EditResourcesRequest;
	internal event Action EditCustomRequest;
	internal event Action AutoSetClicked;
	internal event Action SetSelectedClicked;
	internal event Action<bool> SaveRequested;
	
	public UcEditPanel()
	{
		InitializeComponent();
	}

	internal int NewTileId
	{
		get => _NewTileId;
		private set { 
			_NewTileId = value;
			RefreshUpdateTo(value);
		}
	}

	private int _NewTileId;

	public void Initialize(ref Action<int[]> timeFire,
		ref Action<bool, bool> savedFire, ref Action<bool> setAuto)
	{
		RefreshAuto(false);
		RefreshSaved(true, false);
		
		timeFire += RefreshTime;
		savedFire += RefreshSaved;
		setAuto += RefreshAuto;

		ButtonSaveEdit.Click += (_, _) =>
		{
			LabelSaved.ForeColor = Color.Orange;
			SaveRequested?.Invoke(false);
		};
		ButtonSavePlay.Click += (_, _) =>
		{
			LabelSaved.ForeColor = Color.Yellow;
			SaveRequested?.Invoke(true);
		};
		ButtonEditResources.Click += (_, _) =>
		{
			EditResourcesRequest?.Invoke();
		};
		ButtonCustomSettings.Click += (_, _) =>
		{
			EditCustomRequest?.Invoke();
		};
		ButtonEditTime.Click += (_, _) =>
		{
			EditTimeRequest?.Invoke();
		};
		ButtonSetSelect.Click += (_, _) =>
		{
			SetSelectedClicked?.Invoke();
		};
		ButtonAutoSet.Click += (_, _) =>
		{
			AutoSetClicked?.Invoke();
		};
		
		ButtonLeftLeft.Text = "◀◀";
		ButtonLeftLeft.Click += (sender, e) =>
		{
			var newID = NewTileId - 10;
			if (newID < 0)
			{
				newID = GlobalVariableManager.tileInfo.Length + newID;
			}
			NewTileId = newID;
		};
		ButtonLeft.Text = "◀";
		ButtonLeft.Click += (sender, e) =>
		{
			var newID = NewTileId - 1;
			if (newID < 0)
			{
				newID = GlobalVariableManager.tileInfo.Length + newID;
			}
			NewTileId = newID;
		};
		ButtonRight.Text = "▶";
		ButtonRight.Click += (sender, e) =>
		{
			var newID = NewTileId + 1;
			if (newID >= GlobalVariableManager.tileInfo.Length)
			{
				newID -= GlobalVariableManager.tileInfo.Length;
			}
			NewTileId = newID;
		};
		ButtonRightRight.Text = "▶▶";
		ButtonRightRight.Click += (sender, e) =>
		{
			var newID = NewTileId + 10;
			if (newID >= GlobalVariableManager.tileInfo.Length)
			{
				newID -= GlobalVariableManager.tileInfo.Length;
			}
			NewTileId = newID;
		};
	}

	private void RefreshTime(int[] dayHour)
	{
		LabelDays.Text = $"Day {dayHour[0]}";
	}

	private void RefreshSaved(bool saved, bool playing)
	{
		LabelSaved.ForeColor = saved ? playing ? Color.Green: Color.Blue : Color.Red;
	}
	
	private void RefreshAuto(bool auto)
	{
		ButtonAutoSet.Text = auto ? "Auto Set: True": "Auto Set: False";
		ButtonAutoSet.ForeColor = auto ? Color.Green: Color.Red;
	}

	private void RefreshUpdateTo(int value)
	{
		LabelSetId.Text = value.ToString();
		ImgSetTile.BackgroundImage = 
			HelperStuff.ResizeImage(BasicGuiManager.TileIcons?[value]?? BasicGuiManager.NO_IMAGE_ICON,
			ImgSetTile.Width, ImgSetTile.Height, false);
	}
}