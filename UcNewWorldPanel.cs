namespace Tiles
{
	public partial class UcNewWorldPanel : UserControl
	{
		private int CurrentTab;
		private StandardButton[] TabButtons;
		private Control[] TabMenus = 
		[
			new UcMapCreateInfo(),
			new UcMapCreateMap(),
			new UcMapCreateGoverment(),
			new UcMapCreatorOther(),
			new UcMapCreatorOverview()
		];

		public UcNewWorldPanel()
		{
			InitializeComponent();
			TabButtons = [ButtonBasic, ButtonMapSettings, ButtonGovernment, ButtonMisc, ButtonOverview];
			this.SetAllControlImages();

			for (var i = 0; i < TabButtons.Length; i++)
			{
				var i1 = i;
				TabButtons[i].Click += (_, _) => { ChangeTab(i1); };
				TabButtons[i].Margin = new Padding(10,30,10,0);
			}
			
			//set the starting tab
			myTableLayoutPanel3.Controls.Remove(TabMenus[CurrentTab]);
			TabButtons[CurrentTab].ForeColor = Color.Yellow;
			TabButtons[CurrentTab].Margin = new Padding(0,10,0,0);
			HelperStuff.UpdateFont(TabButtons[CurrentTab]);
		}

		private void ChangeTab(int newTab)
		{
			TabButtons[CurrentTab].ForeColor = Color.Black;
			TabButtons[CurrentTab].Margin = new Padding(10,30,10,0);
			HelperStuff.UpdateFont(TabButtons[CurrentTab]);
			myTableLayoutPanel3.Controls.Remove(TabMenus[CurrentTab]);
			
			CurrentTab = newTab;
			
			myTableLayoutPanel3.Controls.Remove(TabMenus[CurrentTab]);
			TabButtons[CurrentTab].ForeColor = Color.Yellow;
			TabButtons[CurrentTab].Margin = new Padding(0,10,0,0);
			HelperStuff.UpdateFont(TabButtons[CurrentTab]);

		}
	}
}