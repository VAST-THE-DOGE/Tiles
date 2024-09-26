namespace Tiles;

public partial class UcNewWorldPanel : UserControl
{
	private StandardBackground TabMenuHolder = new() {Dock = DockStyle.Fill, Margin = new Padding(0)};
	private UcMapCreateInfo NewMapInfo = new();
	private bool Editing;

	public UcNewWorldPanel(bool editing=false)
	{
		InitializeComponent();
		
		Editing = editing;
			
		myTableLayoutPanel3.Controls.Add(TabMenuHolder, 1, 3);
			
		this.SetAllControlImages();
		
		TabMenuHolder.Controls.Add(NewMapInfo);

		NewMapInfo.CreateRequested += async (name, diff, size) =>
		{
			new Game(await WorldManager.CreateWorld(name, diff, size, Editing));
		};
	}
}