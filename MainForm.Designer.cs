namespace AutoPixelArt
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.showPicAndPal = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSettingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSettingsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianLangItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishLangItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openManualItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.authorItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verBox = new System.Windows.Forms.ComboBox();
            this.verText = new System.Windows.Forms.RichTextBox();
            this.hideControlsPanel = new System.Windows.Forms.Panel();
            this.processingLabel = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.blockCheckBox = new System.Windows.Forms.CheckBox();
            this.autoMinCheckBox = new System.Windows.Forms.CheckBox();
            this.startStopButton = new System.Windows.Forms.Button();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.colorsLabel = new System.Windows.Forms.Label();
            this.countdownTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.estimatedLabel = new System.Windows.Forms.Label();
            this.showProgressCheckBox = new System.Windows.Forms.CheckBox();
            this.progressStatLabel = new System.Windows.Forms.Label();
            this.progressTimeLabel = new System.Windows.Forms.Label();
            this.continueTextBox = new System.Windows.Forms.TextBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.continueCheckBox = new System.Windows.Forms.CheckBox();
            this.progressPercentsLabel = new System.Windows.Forms.Label();
            this.currentLayerLabel = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            this.hideControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // showPicAndPal
            // 
            resources.ApplyResources(this.showPicAndPal, "showPicAndPal");
            this.showPicAndPal.Name = "showPicAndPal";
            this.showPicAndPal.UseVisualStyleBackColor = true;
            this.showPicAndPal.Click += new System.EventHandler(this.showPicAndPal_Click);
            // 
            // mainMenu
            // 
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileItem,
            this.editItem,
            this.langItem,
            this.helpItem});
            this.mainMenu.Name = "mainMenu";
            // 
            // fileItem
            // 
            resources.ApplyResources(this.fileItem, "fileItem");
            this.fileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openItem,
            this.toolStripSeparator,
            this.exitItem});
            this.fileItem.Name = "fileItem";
            // 
            // openItem
            // 
            resources.ApplyResources(this.openItem, "openItem");
            this.openItem.Name = "openItem";
            this.openItem.Click += new System.EventHandler(this.openItem_Click);
            // 
            // toolStripSeparator
            // 
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            this.toolStripSeparator.Name = "toolStripSeparator";
            // 
            // exitItem
            // 
            resources.ApplyResources(this.exitItem, "exitItem");
            this.exitItem.Name = "exitItem";
            this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // editItem
            // 
            resources.ApplyResources(this.editItem, "editItem");
            this.editItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSettingsItem,
            this.resetSettingsItem});
            this.editItem.Name = "editItem";
            // 
            // openSettingsItem
            // 
            resources.ApplyResources(this.openSettingsItem, "openSettingsItem");
            this.openSettingsItem.Name = "openSettingsItem";
            this.openSettingsItem.Click += new System.EventHandler(this.openSettingsItem_Click);
            // 
            // resetSettingsItem
            // 
            resources.ApplyResources(this.resetSettingsItem, "resetSettingsItem");
            this.resetSettingsItem.Name = "resetSettingsItem";
            this.resetSettingsItem.Click += new System.EventHandler(this.resetSettingsItem_Click);
            // 
            // langItem
            // 
            resources.ApplyResources(this.langItem, "langItem");
            this.langItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.russianLangItem,
            this.englishLangItem});
            this.langItem.Name = "langItem";
            // 
            // russianLangItem
            // 
            resources.ApplyResources(this.russianLangItem, "russianLangItem");
            this.russianLangItem.Checked = true;
            this.russianLangItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.russianLangItem.Name = "russianLangItem";
            this.russianLangItem.Click += new System.EventHandler(this.russianLangItem_Click);
            // 
            // englishLangItem
            // 
            resources.ApplyResources(this.englishLangItem, "englishLangItem");
            this.englishLangItem.Name = "englishLangItem";
            this.englishLangItem.Click += new System.EventHandler(this.englishLangItem_Click);
            // 
            // helpItem
            // 
            resources.ApplyResources(this.helpItem, "helpItem");
            this.helpItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openManualItem,
            this.toolStripSeparator1,
            this.aboutItem,
            this.authorItem});
            this.helpItem.Name = "helpItem";
            // 
            // openManualItem
            // 
            resources.ApplyResources(this.openManualItem, "openManualItem");
            this.openManualItem.Name = "openManualItem";
            this.openManualItem.Click += new System.EventHandler(this.openManualItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // aboutItem
            // 
            resources.ApplyResources(this.aboutItem, "aboutItem");
            this.aboutItem.Name = "aboutItem";
            this.aboutItem.Click += new System.EventHandler(this.aboutItem_Click);
            // 
            // authorItem
            // 
            resources.ApplyResources(this.authorItem, "authorItem");
            this.authorItem.Name = "authorItem";
            this.authorItem.Click += new System.EventHandler(this.authorItem_Click);
            // 
            // verBox
            // 
            resources.ApplyResources(this.verBox, "verBox");
            this.verBox.FormattingEnabled = true;
            this.verBox.Items.AddRange(new object[] {
            resources.GetString("verBox.Items"),
            resources.GetString("verBox.Items1"),
            resources.GetString("verBox.Items2"),
            resources.GetString("verBox.Items3"),
            resources.GetString("verBox.Items4"),
            resources.GetString("verBox.Items5"),
            resources.GetString("verBox.Items6"),
            resources.GetString("verBox.Items7"),
            resources.GetString("verBox.Items8")});
            this.verBox.Name = "verBox";
            this.verBox.SelectedIndexChanged += new System.EventHandler(this.verBox_SelectedIndexChanged);
            this.verBox.TextUpdate += new System.EventHandler(this.verBox_TextUpdate);
            // 
            // verText
            // 
            resources.ApplyResources(this.verText, "verText");
            this.verText.BackColor = System.Drawing.SystemColors.Control;
            this.verText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.verText.Cursor = System.Windows.Forms.Cursors.Default;
            this.verText.Name = "verText";
            this.verText.ReadOnly = true;
            // 
            // hideControlsPanel
            // 
            resources.ApplyResources(this.hideControlsPanel, "hideControlsPanel");
            this.hideControlsPanel.Controls.Add(this.processingLabel);
            this.hideControlsPanel.Name = "hideControlsPanel";
            // 
            // processingLabel
            // 
            resources.ApplyResources(this.processingLabel, "processingLabel");
            this.processingLabel.Name = "processingLabel";
            // 
            // progressLabel
            // 
            resources.ApplyResources(this.progressLabel, "progressLabel");
            this.progressLabel.Name = "progressLabel";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // blockCheckBox
            // 
            resources.ApplyResources(this.blockCheckBox, "blockCheckBox");
            this.blockCheckBox.Name = "blockCheckBox";
            this.blockCheckBox.UseVisualStyleBackColor = true;
            this.blockCheckBox.CheckedChanged += new System.EventHandler(this.blockCheckBox_CheckedChanged);
            // 
            // autoMinCheckBox
            // 
            resources.ApplyResources(this.autoMinCheckBox, "autoMinCheckBox");
            this.autoMinCheckBox.Name = "autoMinCheckBox";
            this.autoMinCheckBox.UseVisualStyleBackColor = true;
            this.autoMinCheckBox.CheckedChanged += new System.EventHandler(this.autoMinCheckBox_CheckedChanged);
            // 
            // startStopButton
            // 
            resources.ApplyResources(this.startStopButton, "startStopButton");
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // widthLabel
            // 
            resources.ApplyResources(this.widthLabel, "widthLabel");
            this.widthLabel.Name = "widthLabel";
            // 
            // heightLabel
            // 
            resources.ApplyResources(this.heightLabel, "heightLabel");
            this.heightLabel.Name = "heightLabel";
            // 
            // colorsLabel
            // 
            resources.ApplyResources(this.colorsLabel, "colorsLabel");
            this.colorsLabel.Name = "colorsLabel";
            // 
            // countdownTimer
            // 
            this.countdownTimer.Interval = 1000;
            this.countdownTimer.Tick += new System.EventHandler(this.countdownTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            // 
            // estimatedLabel
            // 
            resources.ApplyResources(this.estimatedLabel, "estimatedLabel");
            this.estimatedLabel.Name = "estimatedLabel";
            // 
            // showProgressCheckBox
            // 
            resources.ApplyResources(this.showProgressCheckBox, "showProgressCheckBox");
            this.showProgressCheckBox.Name = "showProgressCheckBox";
            this.showProgressCheckBox.UseVisualStyleBackColor = true;
            this.showProgressCheckBox.CheckedChanged += new System.EventHandler(this.showProgressCheckBox_CheckedChanged);
            // 
            // progressStatLabel
            // 
            resources.ApplyResources(this.progressStatLabel, "progressStatLabel");
            this.progressStatLabel.Name = "progressStatLabel";
            // 
            // progressTimeLabel
            // 
            resources.ApplyResources(this.progressTimeLabel, "progressTimeLabel");
            this.progressTimeLabel.Name = "progressTimeLabel";
            // 
            // continueTextBox
            // 
            resources.ApplyResources(this.continueTextBox, "continueTextBox");
            this.continueTextBox.Name = "continueTextBox";
            this.continueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.continueTextBox_KeyDown);
            // 
            // continueButton
            // 
            resources.ApplyResources(this.continueButton, "continueButton");
            this.continueButton.Name = "continueButton";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // continueCheckBox
            // 
            resources.ApplyResources(this.continueCheckBox, "continueCheckBox");
            this.continueCheckBox.Name = "continueCheckBox";
            this.continueCheckBox.UseVisualStyleBackColor = true;
            this.continueCheckBox.CheckedChanged += new System.EventHandler(this.continueCheckBox_CheckedChanged);
            // 
            // progressPercentsLabel
            // 
            resources.ApplyResources(this.progressPercentsLabel, "progressPercentsLabel");
            this.progressPercentsLabel.Name = "progressPercentsLabel";
            // 
            // currentLayerLabel
            // 
            resources.ApplyResources(this.currentLayerLabel, "currentLayerLabel");
            this.currentLayerLabel.Name = "currentLayerLabel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.currentLayerLabel);
            this.Controls.Add(this.progressPercentsLabel);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.continueTextBox);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.progressTimeLabel);
            this.Controls.Add(this.progressStatLabel);
            this.Controls.Add(this.showProgressCheckBox);
            this.Controls.Add(this.estimatedLabel);
            this.Controls.Add(this.colorsLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.verBox);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.showPicAndPal);
            this.Controls.Add(this.blockCheckBox);
            this.Controls.Add(this.autoMinCheckBox);
            this.Controls.Add(this.verText);
            this.Controls.Add(this.hideControlsPanel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.continueCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.hideControlsPanel.ResumeLayout(false);
            this.hideControlsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem exitItem;
		private System.Windows.Forms.ToolStripMenuItem helpItem;
		private System.Windows.Forms.ToolStripMenuItem openItem;
		private System.Windows.Forms.ToolStripMenuItem aboutItem;
		private System.Windows.Forms.RichTextBox verText;
		private System.Windows.Forms.ComboBox verBox;
		private System.Windows.Forms.Panel hideControlsPanel;
		private System.Windows.Forms.Label widthLabel;
		private System.Windows.Forms.Label heightLabel;
		private System.Windows.Forms.Timer countdownTimer;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.CheckBox blockCheckBox;
		private System.Windows.Forms.Label estimatedLabel;
		private System.Windows.Forms.CheckBox showProgressCheckBox;
		public System.Windows.Forms.ProgressBar progressBar;
		public System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.ToolStripMenuItem editItem;
		private System.Windows.Forms.ToolStripMenuItem resetSettingsItem;
		private System.Windows.Forms.Label processingLabel;
		private System.Windows.Forms.ToolStripMenuItem authorItem;
		private System.Windows.Forms.ToolStripMenuItem openManualItem;
		public System.Windows.Forms.Label progressStatLabel;
		public System.Windows.Forms.Label progressTimeLabel;
		private System.Windows.Forms.ToolStripMenuItem openSettingsItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Button continueButton;
		public System.Windows.Forms.Label colorsLabel;
		public System.Windows.Forms.Button showPicAndPal;
		private System.Windows.Forms.CheckBox continueCheckBox;
		public System.Windows.Forms.Label progressPercentsLabel;
		public System.Windows.Forms.CheckBox autoMinCheckBox;
		public System.Windows.Forms.Button startStopButton;
		public System.Windows.Forms.Label currentLayerLabel;
		public System.Windows.Forms.TextBox continueTextBox;
        private System.Windows.Forms.ToolStripMenuItem langItem;
        private System.Windows.Forms.ToolStripMenuItem russianLangItem;
        private System.Windows.Forms.ToolStripMenuItem englishLangItem;
    }
}

