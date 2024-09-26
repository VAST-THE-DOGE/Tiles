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
			myTableLayoutPanel1.Controls.Add(Slider, 2, 1);
			Slider.Anchor = AnchorStyles.Left;

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

			var width = myTableLayoutPanel1.Width / myTableLayoutPanel1.ColumnCount - Slider.Margin.Horizontal;
			Slider.Size = new Size(width, width / 10);
			var width2 = (myTableLayoutPanel1.Width / myTableLayoutPanel1.ColumnCount * 2) -
			             maskedTextBox1.Margin.Horizontal;
			maskedTextBox1.Width = width2;
		}
	}
}