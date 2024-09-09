namespace Tiles;

public partial class Loader : Form
{
	private bool middleChangeing = false;
	private MenuStatePrimary PrimaryState = MenuStatePrimary.None;
	private MenuStateSecondary SecondaryState = MenuStateSecondary.None;

	private UcWorldHeaderViewer WorldViewer = new UcWorldHeaderViewer();

	public Loader(bool fullscreen)
	{
		InitializeComponent();
		SetFullscreen(fullscreen);

		MiddlePanel.Visible = false;
		MainTable.Controls.Add(MiddlePanel, 1, 1);
		WorldViewer.Visible = false;
		MainTable.Controls.Add(WorldViewer, 1, 1);
		MainTable.SetColumnSpan(WorldViewer, 2);
		MainTable.SetRowSpan(WorldViewer, 2);
		MiddlePanel.Controls.Clear();

		Cursor = HelperStuff.cursors[0];
		GlobalVariableManager.frame = this;
		GlobalVariableManager.Loader = MainTable;

		Main.SetAllControlImages();
		InfoTable.SetAllControlImages();
		SettingsTable.SetAllControlImages();
	}

	private void SetFullscreen(bool fullscreen)
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
		MiddlePanel.Visible = false;
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

		MiddlePanel.Size = new Size(1, 1);
		MiddlePanel.Dock = DockStyle.None;
		MiddlePanel.Visible = true;
		MainTable.SuspendLayout();
		await HelperStuff.AnimatePanelBounds(MiddlePanel, endRec, 500);
		MainTable.ResumeLayout();
		MiddlePanel.Dock = DockStyle.Fill;
	}

	private async Task CloseWorlds()
	{
		WorldViewer.Dock = DockStyle.None;
		var startRec = WorldViewer.Bounds;
		var endRec = new Rectangle(
			startRec.X,
			startRec.Y,
			WorldViewer.Width,
			1
		);

		MainTable.SuspendLayout();
		WorldViewer.Resizing = true;
		await HelperStuff.AnimatePanelBounds(WorldViewer, endRec, 500);
		WorldViewer.Resizing = false;
		MainTable.ResumeLayout();
		WorldViewer.Visible = false;
	}

	private async Task OpenWorlds()
	{
		var pos = MainTable.GetCellPosition(WorldViewer);

		// Get the widths of all columns and heights of all rows
		var columnWidths = MainTable.GetColumnWidths();
		var rowHeights = MainTable.GetRowHeights();

		// Calculate the bounds of the cell
		var endRec = new Rectangle(
			columnWidths.Take(pos.Column).Sum() + WorldViewer.Margin.Left,
			rowHeights.Take(pos.Row).Sum() + WorldViewer.Margin.Top,
			columnWidths[pos.Column] + columnWidths[pos.Column + 1] - WorldViewer.Margin.Vertical,
			rowHeights[pos.Row] + rowHeights[pos.Row + 1] - WorldViewer.Margin.Horizontal
		);

		WorldViewer.Size = new Size(endRec.Width, 1);
		WorldViewer.Visible = true;
		WorldViewer.Size = new Size(endRec.Width, 1);

		MainTable.SuspendLayout();
		WorldViewer.Resizing = true;
		await HelperStuff.AnimatePanelBounds(WorldViewer, endRec, 500);
		WorldViewer.Resizing = false;
		MainTable.ResumeLayout();
		WorldViewer.Dock = DockStyle.Fill;
	}

	private async void BWorlds_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		switch (PrimaryState)
		{
			case MenuStatePrimary.None:
				break;
			case MenuStatePrimary.Worlds:
				await CloseWorlds();
				break;
			default:
				await CloseMid();
				MiddlePanel.Controls.Clear();
				break;
		}

		switch (SecondaryState)
		{
			case MenuStateSecondary.None:
				break;
		}

		if (PrimaryState == MenuStatePrimary.Worlds)
		{
			PrimaryState = MenuStatePrimary.None;
			middleChangeing = false;
			return;
		}

		await OpenWorlds();

		PrimaryState = MenuStatePrimary.Worlds;
		middleChangeing = false;
	}

	private async void BNewWorld_Click(object sender, EventArgs e)
	{
		//TODO
	}

	private async void BNewMapEdit_Click(object sender, EventArgs e)
	{
		//TODO
	}

	private async void BSettings_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		switch (PrimaryState)
		{
			case MenuStatePrimary.None:
				break;
			case MenuStatePrimary.Worlds:
				await CloseWorlds();
				break;
			default:
				await CloseMid();
				MiddlePanel.Controls.Clear();
				break;
		}

		switch (SecondaryState)
		{
			case MenuStateSecondary.None:
				break;
		}

		if (PrimaryState == MenuStatePrimary.Settings)
		{
			PrimaryState = MenuStatePrimary.None;
			middleChangeing = false;
			return;
		}

		MiddlePanel.Controls.Add(SettingsTable);
		await OpenMid();
		foreach (Control c in SettingsTable.Controls)
		{
			HelperStuff.UpdateFont(c);
		}

		PrimaryState = MenuStatePrimary.Settings;
		middleChangeing = false;
	}

	private async void BInfo_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		switch (PrimaryState)
		{
			case MenuStatePrimary.None:
				break;
			case MenuStatePrimary.Worlds:
				await CloseWorlds();
				break;
			default:
				await CloseMid();
				MiddlePanel.Controls.Clear();
				break;
		}

		switch (SecondaryState)
		{
			case MenuStateSecondary.None:
				break;
		}

		if (PrimaryState == MenuStatePrimary.Info)
		{
			PrimaryState = MenuStatePrimary.None;
			middleChangeing = false;
			return;
		}

		MiddlePanel.Controls.Add(InfoTable);
		await OpenMid();
		foreach (Control c in InfoTable.Controls)
		{
			HelperStuff.UpdateFont(c);
		}

		PrimaryState = MenuStatePrimary.Info;
		middleChangeing = false;
	}

	private enum MenuStatePrimary
	{
		None,
		Info,
		Settings,
		Worlds,
	}

	private enum MenuStateSecondary
	{
		None
	}
}