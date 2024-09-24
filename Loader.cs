namespace Tiles;

public partial class Loader : Form
{
	private bool middleChangeing = false;
	private MenuStatePrimary PrimaryState = MenuStatePrimary.None;
	private MenuStateSecondary SecondaryState = MenuStateSecondary.None;

	private MyTableLayoutPanel MidRightHolder = new()
	{
		BackColor = Color.Transparent,
		RowCount = 1,
		ColumnCount = 1,
		Dock = DockStyle.Fill,
	};

	private UcWorldHeaderViewer WorldViewer = new() 
	{
		Dock = DockStyle.Fill,
	};
	private UcNewWorldPanel NewWorldPanel = new() 
	{
		Dock = DockStyle.Fill,
	};

	public Loader(bool fullscreen)
	{
		InitializeComponent();
		SetFullscreen(fullscreen);
		
		MidRightHolder.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		MidRightHolder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

		MiddlePanel.Visible = false;
		MainTable.Controls.Add(MiddlePanel, 1, 1);
		MidRightHolder.Visible = false;
		MainTable.Controls.Add(MidRightHolder, 1, 1);
		MainTable.SetColumnSpan(MidRightHolder, 2);
		MainTable.SetRowSpan(MidRightHolder, 2);
		MiddlePanel.Controls.Clear();
		MidRightHolder.Dock = DockStyle.None;

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
		MiddlePanel.Controls.Clear();

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

	private async Task CloseMidRight()
	{
		MidRightHolder.SuspendLayout();
		MidRightHolder.Dock = DockStyle.None;
		var startRec = MidRightHolder.Bounds;
		var endRec = new Rectangle(
			startRec.X,
			startRec.Y,
			MidRightHolder.Width,
			1
		);

		MainTable.SuspendLayout();
		await HelperStuff.AnimatePanelBounds(MidRightHolder, endRec, 500);
		MainTable.ResumeLayout();
		MidRightHolder.Visible = false;
		MidRightHolder.Controls.Clear();
		MidRightHolder.ResumeLayout();

	}

	private async Task OpenMidRight()
	{
		MidRightHolder.SuspendLayout();
		var pos = MainTable.GetCellPosition(MidRightHolder);

		// Get the widths of all columns and heights of all rows
		var columnWidths = MainTable.GetColumnWidths();
		var rowHeights = MainTable.GetRowHeights();

		// Calculate the bounds of the cell
		var endRec = new Rectangle(
			columnWidths.Take(pos.Column).Sum() + MidRightHolder.Margin.Left,
			rowHeights.Take(pos.Row).Sum() + MidRightHolder.Margin.Top,
			columnWidths[pos.Column] + columnWidths[pos.Column + 1] - MidRightHolder.Margin.Vertical,
			rowHeights[pos.Row] + rowHeights[pos.Row + 1] - MidRightHolder.Margin.Horizontal
		);

		MidRightHolder.Size = new Size(endRec.Width, 1);
		MidRightHolder.Visible = true;
		MidRightHolder.Size = new Size(endRec.Width, 1);

		MainTable.SuspendLayout();
		await HelperStuff.AnimatePanelBounds(MidRightHolder, endRec, 500);
		MainTable.ResumeLayout();
		MidRightHolder.Dock = DockStyle.Fill;
		MidRightHolder.ResumeLayout();
	}

	private async void BWorlds_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		switch (PrimaryState)
		{
			case MenuStatePrimary.None:
				break;
			case MenuStatePrimary.NewWorld:
			case MenuStatePrimary.NewEdit:
			case MenuStatePrimary.Worlds:
				await CloseMidRight();
				break;
			default:
				await CloseMid();
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

		MidRightHolder.Controls.Add(WorldViewer);
		await OpenMidRight();

		PrimaryState = MenuStatePrimary.Worlds;
		middleChangeing = false;
	}

	private async void BNewWorld_Click(object sender, EventArgs e)
	{
		if (middleChangeing) return;

		middleChangeing = true;
		switch (PrimaryState)
		{
			case MenuStatePrimary.None:
				break;
			case MenuStatePrimary.NewEdit:
			case MenuStatePrimary.NewWorld:
			case MenuStatePrimary.Worlds:
				await CloseMidRight();
				break;
			default:
				await CloseMid();
				break;
		}

		switch (SecondaryState)
		{
			case MenuStateSecondary.None:
				break;
		}

		if (PrimaryState == MenuStatePrimary.NewWorld)
		{
			PrimaryState = MenuStatePrimary.None;
			middleChangeing = false;
			return;
		}

		MidRightHolder.Controls.Add(NewWorldPanel);
		await OpenMidRight();

		PrimaryState = MenuStatePrimary.NewWorld;
		middleChangeing = false;
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
				await CloseMidRight();
				break;
			default:
				await CloseMid();
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
				await CloseMidRight();
				break;
			default:
				await CloseMid();
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
		NewWorld,
		NewEdit,
	}

	private enum MenuStateSecondary
	{
		None
	}
}