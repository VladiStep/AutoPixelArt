
namespace AutoPixelArt
{
	partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.delayLabel = new System.Windows.Forms.Label();
            this.delayBar = new System.Windows.Forms.TrackBar();
            this.delayPanel = new System.Windows.Forms.Panel();
            this.currentDelayLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.escPanel = new System.Windows.Forms.Panel();
            this.escCheckBox = new System.Windows.Forms.CheckBox();
            this.escLabel = new System.Windows.Forms.Label();
            this.origImgPanel = new System.Windows.Forms.Panel();
            this.origImgCheckBox = new System.Windows.Forms.CheckBox();
            this.origImgLabel = new System.Windows.Forms.Label();
            this.vanishMinecraftPanel = new System.Windows.Forms.Panel();
            this.vanishMinecraftCheckBox = new System.Windows.Forms.CheckBox();
            this.vanishMinecraftLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.delayBar)).BeginInit();
            this.delayPanel.SuspendLayout();
            this.escPanel.SuspendLayout();
            this.origImgPanel.SuspendLayout();
            this.vanishMinecraftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // delayLabel
            // 
            resources.ApplyResources(this.delayLabel, "delayLabel");
            this.delayLabel.Name = "delayLabel";
            // 
            // delayBar
            // 
            resources.ApplyResources(this.delayBar, "delayBar");
            this.delayBar.Maximum = 15;
            this.delayBar.Name = "delayBar";
            this.delayBar.Value = 10;
            this.delayBar.Scroll += new System.EventHandler(this.delayBar_Scroll);
            // 
            // delayPanel
            // 
            resources.ApplyResources(this.delayPanel, "delayPanel");
            this.delayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.delayPanel.Controls.Add(this.currentDelayLabel);
            this.delayPanel.Controls.Add(this.label2);
            this.delayPanel.Controls.Add(this.label1);
            this.delayPanel.Controls.Add(this.delayLabel);
            this.delayPanel.Controls.Add(this.delayBar);
            this.delayPanel.Name = "delayPanel";
            // 
            // currentDelayLabel
            // 
            resources.ApplyResources(this.currentDelayLabel, "currentDelayLabel");
            this.currentDelayLabel.BackColor = System.Drawing.Color.Transparent;
            this.currentDelayLabel.Name = "currentDelayLabel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // saveSettingsButton
            // 
            resources.ApplyResources(this.saveSettingsButton, "saveSettingsButton");
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // escPanel
            // 
            resources.ApplyResources(this.escPanel, "escPanel");
            this.escPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.escPanel.Controls.Add(this.escCheckBox);
            this.escPanel.Controls.Add(this.escLabel);
            this.escPanel.Name = "escPanel";
            // 
            // escCheckBox
            // 
            resources.ApplyResources(this.escCheckBox, "escCheckBox");
            this.escCheckBox.Checked = true;
            this.escCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.escCheckBox.Name = "escCheckBox";
            this.escCheckBox.UseVisualStyleBackColor = true;
            this.escCheckBox.CheckedChanged += new System.EventHandler(this.escCheckBox_CheckedChanged);
            // 
            // escLabel
            // 
            resources.ApplyResources(this.escLabel, "escLabel");
            this.escLabel.Name = "escLabel";
            // 
            // origImgPanel
            // 
            resources.ApplyResources(this.origImgPanel, "origImgPanel");
            this.origImgPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.origImgPanel.Controls.Add(this.origImgCheckBox);
            this.origImgPanel.Controls.Add(this.origImgLabel);
            this.origImgPanel.Name = "origImgPanel";
            // 
            // origImgCheckBox
            // 
            resources.ApplyResources(this.origImgCheckBox, "origImgCheckBox");
            this.origImgCheckBox.Name = "origImgCheckBox";
            this.origImgCheckBox.UseVisualStyleBackColor = true;
            this.origImgCheckBox.CheckedChanged += new System.EventHandler(this.origImgCheckBox_CheckedChanged);
            // 
            // origImgLabel
            // 
            resources.ApplyResources(this.origImgLabel, "origImgLabel");
            this.origImgLabel.Name = "origImgLabel";
            // 
            // vanishMinecraftPanel
            // 
            resources.ApplyResources(this.vanishMinecraftPanel, "vanishMinecraftPanel");
            this.vanishMinecraftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vanishMinecraftPanel.Controls.Add(this.vanishMinecraftCheckBox);
            this.vanishMinecraftPanel.Controls.Add(this.vanishMinecraftLabel);
            this.vanishMinecraftPanel.Name = "vanishMinecraftPanel";
            // 
            // vanishMinecraftCheckBox
            // 
            resources.ApplyResources(this.vanishMinecraftCheckBox, "vanishMinecraftCheckBox");
            this.vanishMinecraftCheckBox.Name = "vanishMinecraftCheckBox";
            this.vanishMinecraftCheckBox.UseVisualStyleBackColor = true;
            this.vanishMinecraftCheckBox.CheckedChanged += new System.EventHandler(this.vanishMinecraftCheckBox_CheckedChanged);
            // 
            // vanishMinecraftLabel
            // 
            resources.ApplyResources(this.vanishMinecraftLabel, "vanishMinecraftLabel");
            this.vanishMinecraftLabel.Name = "vanishMinecraftLabel";
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vanishMinecraftPanel);
            this.Controls.Add(this.origImgPanel);
            this.Controls.Add(this.escPanel);
            this.Controls.Add(this.saveSettingsButton);
            this.Controls.Add(this.delayPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.delayBar)).EndInit();
            this.delayPanel.ResumeLayout(false);
            this.delayPanel.PerformLayout();
            this.escPanel.ResumeLayout(false);
            this.escPanel.PerformLayout();
            this.origImgPanel.ResumeLayout(false);
            this.origImgPanel.PerformLayout();
            this.vanishMinecraftPanel.ResumeLayout(false);
            this.vanishMinecraftPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label delayLabel;
		private System.Windows.Forms.Panel delayPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label currentDelayLabel;
		private System.Windows.Forms.Button saveSettingsButton;
		private System.Windows.Forms.Panel escPanel;
		private System.Windows.Forms.CheckBox escCheckBox;
		private System.Windows.Forms.Label escLabel;
		public System.Windows.Forms.TrackBar delayBar;
		private System.Windows.Forms.Panel origImgPanel;
		private System.Windows.Forms.CheckBox origImgCheckBox;
		private System.Windows.Forms.Label origImgLabel;
		private System.Windows.Forms.Panel vanishMinecraftPanel;
		private System.Windows.Forms.CheckBox vanishMinecraftCheckBox;
		private System.Windows.Forms.Label vanishMinecraftLabel;
	}
}