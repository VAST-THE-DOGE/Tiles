namespace Tiles;

public partial class UcEditPanel : StandardBackgroundControl
{
	public UcEditPanel()
	{
		InitializeComponent();
	}

	public event Action<bool> SaveRequested;

	public void Initialize(ref Action<long[], int[]> resourceRefreshFire, ref Action<int[]> timeFire,
		ref Action<bool> savedFire)
	{
		var i = 0;
		foreach (var control in tableLayoutPanel1.Controls)
		{
			if (control is not UcResourcePanel rp) continue;

			rp.Initialize(i, GlobalVariableManager.ResourceNames[i], GlobalVariableManager.ResourceColors[i],
				ref resourceRefreshFire);

			i++;
		}

		timeFire += RefreshTime;
		savedFire += RefreshSaved;

		ButtonSaveEdit.Click += (_, _) =>
		{
			LabelSaved.ForeColor = Color.Yellow;
			SaveRequested?.Invoke(false);
		};
		
	}

	private void RefreshTime(int[] dayHour)
	{
		var timeOfDay = dayHour[1] == 0
			? "12:00 am"
			: dayHour[1] == 12
				? "12:00 pm"
				: dayHour[1] < 12
					? $"{dayHour[1]}:00 am"
					: $"{dayHour[1] - 12}:00 pm";

		LabelDays.Text = $"Day {dayHour[0]}";
	}

	private void RefreshSaved(bool saved)
	{
		LabelSaved.ForeColor = saved ? Color.Green : Color.Red;
	}
}