namespace Tiles
{
	public partial class UcMapCreatorOther : UserControl
	{
		public UcMapCreatorOther()
		{
			InitializeComponent();
			var slider = new TheCoolSlider(5)
			{
				Size = new Size(500, 50),
			};
			Controls.Add(slider);
		}
	}
}