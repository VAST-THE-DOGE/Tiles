namespace Tiles;

public partial class UcBottomPanel : UserControl
{
	public UcBottomPanel()
	{
		InitializeComponent();
	}

	public void Initialize(string[] ResourceNames, Color[] ResourceColors, Action<int[], int[]> resourceRefreshFire)
	{
		var i = 0;
		foreach (var control in Controls)
		{
			if (control is UcResourcePanel rp)
			{
				rp.Initialize(i, Game.ResourceNames[i], Game.ResourceColors[i], resourceRefreshFire);
			}
		}
	}
}