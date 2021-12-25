using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static MainLibrary.MainLib;

namespace AutoPixelArt
{
	using strings = Resources.Strings;

	public partial class SettingsForm : Form
	{
		private static readonly Properties.Settings settings = Properties.Settings.Default;
		private bool recalcImg;
		private readonly MainForm mainForm = (MainForm)Application.OpenForms["mainForm"];
		public SettingsForm()
		{
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Lang == "ru" ? "ru" : "en-US");
			InitializeComponent();
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			Bitmap saveIcon = new(20, 16);
			using (Graphics g = Graphics.FromImage(saveIcon))
            {
				g.DrawImage(Properties.Resources.saveIcon, 0, 0, 16, 16);
            }
			saveSettingsButton.Image = saveIcon;

			delayBar.Scroll -= delayBar_Scroll;
			origImgCheckBox.CheckedChanged -= origImgCheckBox_CheckedChanged;
			escCheckBox.CheckedChanged -= escCheckBox_CheckedChanged;
			vanishMinecraftCheckBox.CheckedChanged -= vanishMinecraftCheckBox_CheckedChanged;

			currentDelayLabel.Text = settings.delay.ToString();
			delayBar.Value = settings.delay;
			origImgCheckBox.Checked = settings.showOrigImg;
			escCheckBox.Checked = settings.pressEsc;
			vanishMinecraftCheckBox.Checked = settings.vanishMinecraft;

			delayBar.Scroll += delayBar_Scroll;
			origImgCheckBox.CheckedChanged += origImgCheckBox_CheckedChanged;
			escCheckBox.CheckedChanged += escCheckBox_CheckedChanged;
			vanishMinecraftCheckBox.CheckedChanged += vanishMinecraftCheckBox_CheckedChanged;
		}

		private void delayBar_Scroll(object sender, EventArgs e)
		{
			settings.delay = (byte)delayBar.Value;
			currentDelayLabel.Text = delayBar.Value.ToString();
		}

		private void escCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			settings.pressEsc = escCheckBox.Checked;
		}

		private void origImgCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			settings.showOrigImg = origImgCheckBox.Checked;

			recalcImg = !recalcImg;
		}

		private void vanishMinecraftCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!settings.vanishWarned)
			{
				Warning(strings.vanishWarning, false);
				settings.vanishWarned = true;
			}

			settings.vanishMinecraft = vanishMinecraftCheckBox.Checked;
			
			if (settings.vanishMinecraft)
				mainForm.MouseClick += mainForm.MainForm_MouseClick;
			else
				mainForm.MouseClick -= mainForm.MainForm_MouseClick;
		}

		private void saveSettingsButton_Click(object sender, EventArgs e)
		{
			mainForm.autoMinCheckBox.Enabled = !settings.vanishMinecraft;
			mainForm.autoMinCheckBox.Checked = settings.vanishMinecraft;

			settings.Save();

			if (recalcImg)
				RecalculateImage();

			Msg(strings.settingsSaved);
			
			Dispose();
		}
	}
}