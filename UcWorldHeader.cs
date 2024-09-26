namespace Tiles;

public partial class UcWorldHeader : UserControl
{
	public enum ButtonType
	{
		Play,
		Edit,
		Copy,
		Delete,
	}

	private WorldHeader Header;

	public UcWorldHeader(WorldHeader header)
	{
		Header = header;
		InitializeComponent();

		LoadImg();

		LabelName.Text = Header.Name;
		LabelTime.Text = $"Day {Header.Time[0]} - Hour {Header.Time[1]}";

		ButtonPlay.Text = Header.EditedMap ? "📝" : "▶️";

		ButtonPlay.Click += (_, _) =>
		{
			ButtonClicked.Invoke(Header.EditedMap ? ButtonType.Edit : ButtonType.Play, Header);
		};
		ButtonCopy.Click += (_, _) => { ButtonClicked.Invoke(ButtonType.Copy, Header); };
		ButtonDelete.Click += (_, _) => { ButtonClicked.Invoke(ButtonType.Delete, Header); };

		myTableLayoutPanel1.BackColor = GlobalVariableManager.DifficultyColors[Header.Difficulty];
	}

	public event Action<ButtonType, WorldHeader> ButtonClicked;

	private async void LoadImg()
	{
		var img = HelperStuff.ResizeImage((await WorldManager.LoadWorldImage(Header.Name)), WorldImg.Width,
			WorldImg.Height, false);
		WorldImg.BackgroundImage = img;
	}
}