namespace Tiles;

public partial class UcMapCreateInfo : UserControl
{
	private int OldWidth;
	private TheCoolSlider Slider;
	private TheCoolSlider Slider2;

	private string[] SizeNames = ["Microscopic", "Tiny", "Small", "Medium", "Large", "Massive", "Gigantic", "Break Your PC"];
	private (int,int)[] SizeTypes = [(5,3), (10,5), (20,10), (30,15), (50,25), (100, 50), (250,125), (1000,500)];
	private int SizePos;
	private int DiffPos;

	public event Action<string, int, (int, int)> CreateRequested;

	public UcMapCreateInfo()
	{
		InitializeComponent();

		Slider = new TheCoolSlider(7);
		Slider.PositionChanged += Slider_ValueChangedDiff;
		myTableLayoutPanel3.Controls.Add(Slider, 0, 1);
		myTableLayoutPanel3.SetColumnSpan(Slider, 2);
		Slider.Anchor = AnchorStyles.Left;
		Slider.Margin = new Padding(5, 0, 5, 5);

		Slider2 = new TheCoolSlider(8);
		Slider2.PositionChanged += Slider_ValueChangedSize;
		myTableLayoutPanel4.Controls.Add(Slider2, 0, 1);
		myTableLayoutPanel4.SetColumnSpan(Slider2, 3);
		Slider2.Anchor = AnchorStyles.Left;
		Slider2.Margin = new Padding(0, 0, 0, 0);

		standardButton2.Click += (_, _) => CreateRequested?.Invoke(maskedTextBox1.Text, DiffPos, SizeTypes[SizePos]);
			
		Slider_ValueChangedDiff(0);
		Slider_ValueChangedSize(0);

		
		Resize += HandleResize;

		Dock = DockStyle.Fill;
		this.SetAllControlImages();
	}

	private async void Slider_ValueChangedDiff(int pos)
	{
		LabelDifficulty.Text = GlobalVariableManager.DifficultyNames[pos];
		LabelDifficulty.ForeColor = GlobalVariableManager.DifficultyColors[pos];
		Slider.SetBackColor(GlobalVariableManager.DifficultyColors[pos]);
		DiffPos = pos;
	}
		
	private async void Slider_ValueChangedSize(int pos)
	{
		LabelSizeName.Text = SizeNames[pos];
		LabelSizeNumbers.Text = @$"{SizeTypes[pos].Item1}x{SizeTypes[pos].Item2}";
		SizePos = pos;
	}

	private void HandleResize(object sender, EventArgs e)
	{
		if (OldWidth == Width) return;

		var width = (myTableLayoutPanel3.Width / myTableLayoutPanel3.ColumnCount) * 2 - Slider.Margin.Horizontal;
		Slider.Size = new Size(width, 60);
			
		// very weird resizing math, but it works for now.
		var width2 = (myTableLayoutPanel4.Width / myTableLayoutPanel3.ColumnCount) * 3 - Slider.Margin.Horizontal;
		Slider2.Size = new Size(width2, 60);
	}
}