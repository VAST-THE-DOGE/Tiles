namespace Tiles;

public partial class UcResourcePanel : StandardBackgroundControl
{
	private Color ResourceColor;
	private int ResourceId;
	private string ResourceName;

	public UcResourcePanel()
	{
		InitializeComponent();
	}

	public void Initialize(int resourceId, string resourceName, Color resourceColor,
		ref Action<long[], int[]> resourceRefreshFire)
	{
		ResourceId = resourceId;
		ResourceName = resourceName;
		ResourceColor = resourceColor;

		label1.Text = $"{ResourceName}: ~";
		label2.Text = $"~";

		BackColor = resourceColor;
		tableLayoutPanel1.BackColor = resourceColor;
		label1.ForeColor = resourceColor;

		resourceRefreshFire += RefreshResources;
	}

	private void RefreshResources(long[] newValues, int[] newIncomes)
	{
		label1.Text = $"{ResourceName}: {newValues[ResourceId]}";
		label2.Text = newIncomes[ResourceId] > 0
			? $"+{newIncomes[ResourceId]}"
			: newIncomes[ResourceId].ToString();
		label2.ForeColor = (newIncomes[ResourceId]) switch
		{
			> 0 => Color.Green,
			0 => Color.Yellow,
			< 0 => Color.Red,
		};
	}
}