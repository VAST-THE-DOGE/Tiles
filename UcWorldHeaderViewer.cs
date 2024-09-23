namespace Tiles;

public partial class UcWorldHeaderViewer : UserControl
{
	private bool _resizing;
	private TheCoolScrollBar ScrollBar;

	public UcWorldHeaderViewer()
	{
		InitializeComponent();
		FlowPanel.AutoScroll = true;
		FlowPanel.FlowDirection = FlowDirection.LeftToRight;
		Initialize();

		ScrollBar = new TheCoolScrollBar();
		ScrollBar.Dock = DockStyle.Fill;
		myTableLayoutPanel1.Controls.Add(ScrollBar, 1, 1);
		
		ScrollBar.Scroll += CustomScrollBarScroll;
		FlowPanel.MouseWheel += CustomScrollBarScrollInverse;
		FlowPanel.BackColor = Color.Transparent;
	}

	internal bool Resizing
	{
		get => _resizing;
		set
		{
			_resizing = value;
			if (_resizing)
			{
				Resize -= ResizeControls;
				FlowPanel.SuspendLayout();
				//myTableLayoutPanel1.SuspendLayout();
			}
			else
			{
				FlowPanel.ResumeLayout();
				//myTableLayoutPanel1.ResumeLayout();
				Resize += ResizeControls;
			}
		}
	}

	private void CustomScrollBarScrollInverse(object sender, EventArgs e)
	{
		var maxScroll = FlowPanel.VerticalScroll.Maximum - FlowPanel.ClientSize.Height;
		var newThumb = (int)(((double)FlowPanel.VerticalScroll.Value / maxScroll) * (ScrollBar.Height - ScrollBar.thumbHeight));
		ScrollBar.RefreshThumb(newThumb);
	}
	private void CustomScrollBarScroll(object sender, EventArgs e)
	{
		var maxScroll = FlowPanel.VerticalScroll.Maximum - FlowPanel.ClientSize.Height;
		var scrollValue =
			(int)((double)ScrollBar.ThumbPosition / (ScrollBar.Height - ScrollBar.thumbHeight) * maxScroll);
		FlowPanel.VerticalScroll.Value = Math.Max(0, Math.Min(scrollValue, maxScroll));
		FlowPanel.PerformLayout();
	}

	private void Initialize()
	{
		RefreshWorlds();

		Resize += ResizeControls;
		ButtonRefresh.Click += (_, _) => RefreshWorlds();
	}

	private async void RefreshWorlds()
	{
		var headers = (await WorldManager.GetWorldHeaders())
			.OrderByDescending(w => w.Time[0]);

		FlowPanel.Controls.Clear();

		foreach (var header in headers)
		{
			var panel = new UcWorldHeader(header);
			panel.Width = FlowPanel.Width - panel.Margin.Horizontal;

			panel.ButtonClicked += HandleHeaderAction;
			panel.Margin = new Padding(0, 5, 0, 5);

			FlowPanel.Controls.Add(panel);
		}

		this.SetAllControlImages();
	}

	private void ResizeControls(object obj, EventArgs args)
	{
		if (Resizing) return;

		foreach (Control control in FlowPanel.Controls)
		{
			control.Width = FlowPanel.Width - control.Margin.Horizontal;
		}
	}

	private async void HandleHeaderAction(UcWorldHeader.ButtonType buttonType, WorldHeader header)
	{
		switch (buttonType)
		{
			case UcWorldHeader.ButtonType.Play:
				GlobalVariableManager.GameOrEditRef = new Game(await WorldManager.LoadWorld(header));
				break;
			case UcWorldHeader.ButtonType.Edit:
				GlobalVariableManager.GameOrEditRef = new Editor(await WorldManager.LoadWorld(header));
				break;
			case UcWorldHeader.ButtonType.Copy:
				await WorldManager.CopyWorld(await WorldManager.LoadWorld(header), $"{header.Name} - Copy");
				RefreshWorlds();
				break;
			case UcWorldHeader.ButtonType.Delete:
				if (MessageBox.Show($"Are you sure that you want to delete {header.Name}", "Delete World",
					    MessageBoxButtons.YesNo)
				    != DialogResult.Yes)
					break;
				await WorldManager.DeleteWorld(header);
				RefreshWorlds();
				break;
		}
	}
}