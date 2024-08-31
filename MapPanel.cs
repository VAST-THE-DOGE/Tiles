namespace Tiles;

public class MapPanel : MyTableLayoutPanel
{
	private Button[][] buttons;

	//TODO: hover while mouse left is down = click.
	private bool isMouseDown;
	private int[] selected = [0, 0];

	public MapPanel(int[][]? Map = null, int[][]? statusIds = null)
	{
		BackColor = Color.DodgerBlue;
		Margin = new Padding(0);

		if (Map != null)
			RefreshAll(Map, statusIds);
	}

	public void SetEvents(ref Action<int, int, int> setTileImage, ref Action<int[][], int[][]?> refreshAll,
		ref Action<int, int, int> setTileStatus, ref Action<int> setWeather)
	{
		setTileStatus += SetTileStatus;
		setTileImage += SetTileImage;
		refreshAll += RefreshAll;
	}

	private void SetTileImage(int x, int y, int id)
	{
		buttons[y][x].BackgroundImage = BasicGuiManager.TileIcons?[id] ?? BasicGuiManager.NO_IMAGE_ICON;
	}

	private void SetTileStatus(int x, int y, int status)
	{
		buttons[y][x].Text = GetStatusText(status);
	}

	private void RefreshAll(int[][] iconIds, int[][]? statusIds = null)
	{
		Controls.Clear();

		buttons = new Button[iconIds.Length][];
		for (var i = 0; i < buttons.Length; i++)
		{
			buttons[i] = new Button[iconIds[0].Length];
		}

		ColumnCount = iconIds[0].Length;
		RowCount = iconIds.Length;

		// NEW - Setup row and column styles //TODO weird sizing of last row & column
		RowStyles.Clear();
		var rowPercentage = RowCount / 100f;
		for (var i = 0; i < RowCount; i++)
		{
			RowStyles.Add(new RowStyle(SizeType.Percent, rowPercentage));
		}

		ColumnStyles.Clear();
		var columnPercentage = ColumnCount / 100f;
		for (var i = 0; i < ColumnCount; i++)
		{
			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentage));
		}

		SuspendLayout();

		for (var c = 0; c < ColumnCount; c++)
		{
			for (var r = 0; r < RowCount; r++)
			{
				var column = c;
				var row = r;

				buttons[r][c] = new Button();
				buttons[r][c].Text = GetStatusText(statusIds?[r][c]);
				buttons[r][c].ForeColor = Color.Orange;
				buttons[r][c].Dock = DockStyle.Fill;

				buttons[r][c].FlatStyle = FlatStyle.Flat;
				buttons[r][c].FlatAppearance.BorderSize = GlobalVariableManager.settings.Grid ? 1 : 0;

				//testing thing
				if (GlobalVariableManager.settings.ExtraEffects)
				{
					buttons[r][c].MouseEnter += (sender, e) =>
					{
						if (selected[0] != row || selected[1] != column)
						{
							buttons[row][column].FlatAppearance.BorderColor
								= Color.Yellow;
							if (!GlobalVariableManager.settings.Grid)
							{
								buttons[row][column].FlatAppearance.BorderSize = 1;
							}
						}
					};

					buttons[r][c].MouseLeave += (sender, e) =>
					{
						if (selected[0] != row || selected[1] != column)
						{
							buttons[row][column].FlatAppearance.BorderColor
								= BackColor;
							if (!GlobalVariableManager.settings.Grid)
							{
								buttons[row][column].FlatAppearance.BorderSize = 0;
							}
						}
					};
					HelperStuff.SetupMouseEffects(buttons[r][c], true, true, false);
				}

				buttons[r][c].FlatAppearance.BorderColor = BackColor;
				buttons[r][c].BackgroundImage =
					BasicGuiManager.TileIcons?[iconIds[r][c]] ?? BasicGuiManager.NO_IMAGE_ICON;
				buttons[r][c].Margin = new Padding(0);
				buttons[r][c].BackgroundImageLayout = ImageLayout.Stretch;

				buttons[r][c].Tag = new Point(c, r);

				buttons[r][c].MouseDown += (s, e) =>
				{
					if (e.Button == MouseButtons.Left) isMouseDown = true;
					OnButtonClicked(s, e);
					Focus();
				};
				buttons[r][c].MouseUp += (_, e) =>
				{
					if (e.Button == MouseButtons.Left) isMouseDown = false;
				};
				buttons[r][c].MouseMove += MainForm_MouseMove;

				Controls.Add(buttons[r][c], column, row);
			}
		}

		ResumeLayout();
		PerformLayout();
	}

	private static string GetStatusText(int? id)
	{
		return (id) switch
		{
			1 => "🚧", // 1 = construction
			2 => "Ⓧ", //  2 = disabled
			_ => "", // 0, null, or other = normal
		};
	}

	internal event Action<int, int> MapButtonClicked;

	private void OnButtonClicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var location = (Point)((Button)sender).Tag;

		buttons[selected[0]][selected[1]].FlatAppearance.BorderColor = BackColor;
		buttons[selected[0]][selected[1]].FlatAppearance.BorderSize = GlobalVariableManager.settings.Grid ? 1 : 0;

		selected[0] = location.Y;
		selected[1] = location.X;

		button.FlatAppearance.BorderColor = Color.Red;
		button.FlatAppearance.BorderSize = 2;

		MapButtonClicked?.Invoke(location.X, location.Y);
	}

	private void MainForm_MouseMove(object sender, MouseEventArgs e)
	{
		if (!isMouseDown || sender is not Button b) return;

		var mousePos = b.PointToClient(Cursor.Position);

		//if (b.ClientRectangle.Contains(mousePos)) return;
		if (b.Tag is not Point p) return;

		//get the button that is hovered over:
		var hoveredOver = new Point(p.X + (int)Math.Floor(mousePos.X / (double)b.Width),
			p.Y + (int)Math.Floor(mousePos.Y / (double)b.Height));

		//check if the new location is valid:
		if (hoveredOver.X == selected[1] && hoveredOver.Y == selected[0]) return;
		if (hoveredOver.X < 0 || hoveredOver.Y < 0 || hoveredOver.Y >= buttons.Length ||
		    hoveredOver.X >= buttons[0].Length) return;
		OnButtonClicked(buttons[hoveredOver.Y][hoveredOver.X], EventArgs.Empty);
	}
}