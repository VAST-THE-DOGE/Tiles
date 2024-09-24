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
		
		private StandardBackground TabMenuHolder = new() {Dock = DockStyle.Fill, Margin = new Padding(0)};

		public UcNewWorldPanel()
		{
			InitializeComponent();
			TabButtons = [ButtonBasic, ButtonMapSettings, ButtonGovernment, ButtonMisc, ButtonOverview];
			
			myTableLayoutPanel3.Controls.Add(TabMenuHolder, 1, 3);
			
			this.SetAllControlImages();

			for (var i = 0; i < TabButtons.Length; i++)
			{
				var i1 = i;
				TabButtons[i].Click += (_, _) => { ChangeTab(i1); };
				TabButtons[i].Margin = new Padding(10,30,10,0);
			}
			
			//set the starting tab
			TabMenuHolder.Controls.Add(TabMenus[CurrentTab]);
			TabButtons[CurrentTab].ForeColor = Color.Yellow;
			TabButtons[CurrentTab].Margin = new Padding(0,10,0,0);
			HelperStuff.UpdateFont(TabButtons[CurrentTab]);
		}

		private void ChangeTab(int newTab)
		{
			TabButtons[CurrentTab].ForeColor = Color.Black;
			TabButtons[CurrentTab].Margin = new Padding(10,30,10,0);
			HelperStuff.UpdateFont(TabButtons[CurrentTab]);
			TabMenuHolder.Controls.Clear();
			
			CurrentTab = newTab;
			
			TabMenuHolder.Controls.Add(TabMenus[CurrentTab]);
			TabButtons[CurrentTab].ForeColor = Color.Yellow;
			TabButtons[CurrentTab].Margin = new Padding(0,10,0,0);
			HelperStuff.UpdateFont(TabButtons[CurrentTab]);

		}
	}
}