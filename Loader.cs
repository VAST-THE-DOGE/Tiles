namespace Tiles;

public partial class Loader : Form
{
	private bool middleChangeing = false;
	private int middleId = 5;

	private int state = 4;

	public Loader(bool fullscreen)
	{
		InitializeComponent();
		SetFullscreen(fullscreen);

		Cursor = HelperStuff.cursors[1];
		GlobalVariableManager.frame = this;

		Main.BackgroundImage = Game.menuIcons[8];
		foreach (Control control in Main.Controls)
		{
			if (control is Button)
			{
				HelperStuff.SetupMouseEffects(control, true, true, true);
				control.BackgroundImage = Game.menuIcons[28];
			}
		}

		SettingsTable.BackgroundImage = Game.menuIcons[8];
		foreach (Control control in SettingsTable.Controls)
		{
			if (control is Button)
			{
				HelperStuff.SetupMouseEffects(control, true, true, true);
				control.BackgroundImage = Game.menuIcons[28];
			}
		}

		InfoTable.BackgroundImage = Game.menuIcons[8];
		foreach (Control control in InfoTable.Controls)
		{
			if (control is Button)
			{
				HelperStuff.SetupMouseEffects(control, true, true, true);
				control.BackgroundImage = Game.menuIcons[28];
			}
		}
	}

	public void SetFullscreen(bool fullscreen)
	{
		if (fullscreen)
		{
			WindowState = FormWindowState.Normal;
			FormBorderStyle = FormBorderStyle.None;
			Bounds = Screen.PrimaryScreen.Bounds;
			TopMost = true;
		}
		else
		{
			WindowState = FormWindowState.Maximized;
			FormBorderStyle = FormBorderStyle.Sizable;
			TopMost = false;
		}
	}

	private async void Loader_SizeChanged(object sender, EventArgs e)
	{
		foreach (Control control in Main.Controls)
		{
			if (control is Button)
			{
				HelperStuff.UpdateFontNew(control);
			}
		}
	}

	private async Task CloseMid()
	{
		MiddlePanel.Dock = DockStyle.None;
		var startRec = MiddlePanel.Bounds;
		var endRec = new Rectangle(
			startRec.Left,
			startRec.Top,
			0,
			0
		);

		MainTable.SuspendLayout();
		await HelperStuff.AnimatePanelBounds(MiddlePanel, endRec, 500);
		MainTable.ResumeLayout();
	}

	private async Task OpenMid()
	{
		var pos = MainTable.GetCellPosition(MiddlePanel);

		// Get the widths of all columns and heights of all rows
		var columnWidths = MainTable.GetColumnWidths();
		var rowHeights = MainTable.GetRowHeights();

		// Calculate the bounds of the cell
		var endRec = new Rectangle(
			columnWidths.Take(pos.Column).Sum() + MiddlePanel.Margin.Left,
			rowHeights.Take(pos.Row).Sum() + MiddlePanel.Margin.Top,
			columnWidths[pos.Column] - MiddlePanel.Margin.Vertical,
			rowHeights[pos.Row] - MiddlePanel.Margin.Horizontal
		);

		MainTable.SuspendLayout();
		await HelperStuff.AnimatePanelBounds(MiddlePanel, endRec, 500);
		MainTable.ResumeLayout();
		MiddlePanel.Dock = DockStyle.Fill;
	}

	private async void BWorlds_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		if (middleId != 0)
		{
			await CloseMid();
			MiddlePanel.Controls.Clear();
		}

		//if (middleId == 1)
		//{
		//	middleId = 0;
		//	middleChangeing = false;
		//	return;
		//}
		//
		//await OpenMid();

		middleId = 1;
		middleChangeing = false;
	}

	private async void BNewWorld_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		if (middleId != 0)
		{
			await CloseMid();
			MiddlePanel.Controls.Clear();
		}

		//if (middleId == 2)
		//{
		//	middleId = 0;
		//	middleChangeing = false;
		//	return;
		//}
		//
		//await OpenMid();

		middleId = 2;
		middleChangeing = false;
	}

	private async void BNewMapEdit_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		if (middleId != 0)
		{
			await CloseMid();
			MiddlePanel.Controls.Clear();
		}

		//if (middleId == 3)
		//{
		//	middleId = 0;
		//	middleChangeing = false;
		//	return;
		//}
		//
		//await OpenMid();

		middleId = 3;
		middleChangeing = false;
	}

	private async void BSettings_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		if (middleId != 0)
		{
			await CloseMid();
			MiddlePanel.Controls.Clear();
		}

		if (middleId == 4)
		{
			middleId = 0;
			middleChangeing = false;
			return;
		}

		MiddlePanel.Controls.Add(SettingsTable);
		await OpenMid();
		foreach (Control c in SettingsTable.Controls)
		{
			HelperStuff.UpdateFont(c);
		}

		middleId = 4;
		middleChangeing = false;
	}

	private async void BInfo_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		if (middleId != 0)
		{
			await CloseMid();
			MiddlePanel.Controls.Clear();
		}

		if (middleId == 5)
		{
			middleId = 0;
			middleChangeing = false;
			return;
		}

		MiddlePanel.Controls.Add(InfoTable);
		await OpenMid();
		foreach (Control c in InfoTable.Controls)
		{
			HelperStuff.UpdateFont(c);
		}

		middleId = 5;
		middleChangeing = false;
	}
}