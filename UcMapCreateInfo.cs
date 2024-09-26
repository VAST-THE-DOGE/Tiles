namespace Tiles
{
	public partial class UcMapCreateInfo : UserControl
	{
		private int OldWidth;
		private TheCoolSlider Slider;

		public UcMapCreateInfo()
		{
			InitializeComponent();

			Slider = new TheCoolSlider(7);
			Slider.PositionChanged += Slider_ValueChanged;
			myTableLayoutPanel3.Controls.Add(Slider, 0, 1);
			myTableLayoutPanel3.SetColumnSpan(Slider, 2);
			Slider.Anchor = AnchorStyles.Left;
			Slider.Margin = new Padding(5, 0, 5, 5);

			Resize += HandleResize;

			Dock = DockStyle.Fill;
			this.SetAllControlImages();
		}

		private async void Slider_ValueChanged(int pos)
		{
			LabelDifficulty.Text = GlobalVariableManager.DifficultyNames[pos];
			LabelDifficulty.ForeColor = GlobalVariableManager.DifficultyColors[pos];
			Slider.SetBackColor(GlobalVariableManager.DifficultyColors[pos]);
		}

		private void HandleResize(object sender, EventArgs e)
		{
			if (OldWidth == Width) return;

			var width = (myTableLayoutPanel3.Width / myTableLayoutPanel3.ColumnCount) * 2 - Slider.Margin.Horizontal;
			Slider.Size = new Size(width, 50);
		}
	}
}