namespace Tiles;

public class MapPanel : MyTableLayoutPanel
{
	//TODO: what is this??? change to private and move code.
	public Button[][] buttons;

	public MapPanel(int[][] Map)
	{
		buttons = new Button[Map.Length][];
		for (var i = 0; i < buttons.Length; i++)
		{
			buttons[i] = new Button[Map[0].Length];
		}

		ColumnCount = Map[0].Length;
		RowCount = Map.Length;
		BackColor = Color.SteelBlue;
		Margin = new Padding(0);

		// NEW - Setup row and column styles
		RowStyles.Clear();
		var rowPercentage = RowCount / 100f;
		for (var i = 0; i <= RowCount; i++)
		{
			RowStyles.Add(new RowStyle(SizeType.Percent, rowPercentage));
		}

		ColumnStyles.Clear();
		var columnPercentage = ColumnCount / 100f;
		for (var i = 0; i <= ColumnCount; i++)
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
				buttons[r][c].Text = "🚧";
				buttons[r][c].ForeColor = Color.Orange;

				buttons[r][c].FlatStyle = FlatStyle.Flat;
				if (!GlobalVariableManager.settings.Grid)
				{
					buttons[r][c].FlatAppearance.BorderSize = 0;
				}
				else
				{
					buttons[r][c].FlatAppearance.BorderSize = 1;
				}

				//testing thing
				if (GlobalVariableManager.settings.ExtraEffects)
				{
					buttons[r][c].MouseEnter += (sender, e) =>
					{
						if (Game.selected[0] != row || Game.selected[1] != column)
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
						if (Game.selected[0] != row || Game.selected[1] != column)
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
				buttons[r][c].BackgroundImage = BasicGuiManager.TileIcons?[Map[r][c]] ?? Game.NO_IMAGE_ICON;
				buttons[r][c].Margin = new Padding(0);
				buttons[r][c].BackgroundImageLayout = ImageLayout.Stretch;

				buttons[r][c].Tag = new Point(c, r); //new
				//buttons[r][c].Click += (sender, e) =>
				//{
				//    Clicked(this, row, column);
				//};
				buttons[r][c].Click += OnButtonClicked; //new

				Controls.Add(buttons[r][c], column, row);
			}
		}

		ResumeLayout();
		PerformLayout();
	}

	internal event Action<MapPanel, int, int> MapButtonClicked;

	private void OnButtonClicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var location = (Point)button.Tag;

		MapButtonClicked?.Invoke(this, location.Y, location.X);
	}
}