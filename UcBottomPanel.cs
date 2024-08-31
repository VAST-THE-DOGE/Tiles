namespace Tiles;

public partial class UcBottomPanel : StandardBackgroundControl
{
	public UcBottomPanel()
	{
		InitializeComponent();
	}

	public event Action SaveRequested;

	public void Initialize(ref Action<long[], int[]> resourceRefreshFire, ref Action<int[]> timeFire,
		ref Action<bool> savedFire)
	{
		var i = 0;
		foreach (var control in tableLayoutPanel1.Controls)
		{
			if (control is UcResourcePanel rp)
			{
				rp.Initialize(i, GlobalVariableManager.ResourceNames[i], GlobalVariableManager.ResourceColors[i],
					ref resourceRefreshFire);
				i++;
			}
		}

		timeFire += RefreshTime;
		savedFire += RefreshSaved;

		ButtonSave.Click += (_, _) =>
		{
			LabelSaved.ForeColor = Color.Yellow;
			SaveRequested?.Invoke();
		};
	}

	private void RefreshTime(int[] dayHour)
	{
		LabelDays.Text = $"Day {dayHour[0]}";
		LabelHour.Text = $"Hour: {dayHour[1]}";
	}

	private void RefreshSaved(bool saved)
	{
		LabelSaved.Text = @"💾: " + (saved ? "✔" : "✘");
		LabelSaved.ForeColor = saved ? Color.Green : Color.Red;
	}
}