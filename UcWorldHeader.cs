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

		myTableLayoutPanel1.BackColor = (Header.Difficulty) switch
		{
			0 => Color.LimeGreen,
			1 => Color.Yellow,
			2 => Color.Orange,
			3 => Color.Red,
			4 => Color.Purple,
			5 => Color.Black,
			_ => Color.White,
		};
	}

	public event Action<ButtonType, WorldHeader> ButtonClicked;

	private async void LoadImg()
	{
		var img = HelperStuff.ResizeImage((await WorldManager.LoadWorldImage(Header.Name)), WorldImg.Width,
			WorldImg.Height, false);
		WorldImg.BackgroundImage = img;
	}
}