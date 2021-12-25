namespace AutoPixelArt
{
	partial class PreviewForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.previewPic = new AutoPixelArt.PictureBoxWithInterpolationMode();
            this.previewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.savePicItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveUpPicItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepRatioItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.previewPic)).BeginInit();
            this.previewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewPic
            // 
            resources.ApplyResources(this.previewPic, "previewPic");
            this.previewPic.ContextMenuStrip = this.previewMenu;
            this.previewPic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.previewPic.Name = "previewPic";
            this.previewPic.TabStop = false;
            // 
            // previewMenu
            // 
            resources.ApplyResources(this.previewMenu, "previewMenu");
            this.previewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePicItem,
            this.saveUpPicItem,
            this.keepRatioItem});
            this.previewMenu.Name = "previewMenu";
            // 
            // savePicItem
            // 
            resources.ApplyResources(this.savePicItem, "savePicItem");
            this.savePicItem.Name = "savePicItem";
            this.savePicItem.Click += new System.EventHandler(this.savePicItem_Click);
            // 
            // saveUpPicItem
            // 
            resources.ApplyResources(this.saveUpPicItem, "saveUpPicItem");
            this.saveUpPicItem.Name = "saveUpPicItem";
            this.saveUpPicItem.Click += new System.EventHandler(this.saveUpPicItem_Click);
            // 
            // keepRatioItem
            // 
            resources.ApplyResources(this.keepRatioItem, "keepRatioItem");
            this.keepRatioItem.Checked = true;
            this.keepRatioItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepRatioItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.keepRatioItem.Name = "keepRatioItem";
            this.keepRatioItem.Click += new System.EventHandler(this.keepRatioItem_Click);
            // 
            // PreviewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewPic);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "PreviewForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewForm_FormClosed);
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewPic)).EndInit();
            this.previewMenu.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip previewMenu;
		private System.Windows.Forms.ToolStripMenuItem savePicItem;
		private System.Windows.Forms.ToolStripMenuItem saveUpPicItem;
        private System.Windows.Forms.ToolStripMenuItem keepRatioItem;
        public PictureBoxWithInterpolationMode previewPic;
    }
}