using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MainLibrary.MainLib;

namespace AutoPixelArt
{
	using strings = Resources.Strings;

	public partial class MainForm : Form
	{
		private static readonly Properties.Settings settings = Properties.Settings.Default;
		
		private static ManualForm manualForm;
		private static SettingsForm settingsForm;
		public static byte Cd { get; set; }

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
		#if DEBUG
			Debugging = true;
		#endif
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1 && args[1] == "/debug")
				Debugging = true;
			if (Debugging)
				Text = $"AutoPixelArt ({strings.debugMode})";

			HMain = Handle;

			hideControlsPanel.Location = new Point(0, 24);
			hideControlsPanel.BringToFront();

			RestoreCheckboxes();

			if (settings.vanishMinecraft)
			{
				autoMinCheckBox.Enabled = false;
				MouseClick += MainForm_MouseClick;
			}

			bool settingsIsReset = false;
			try
			{
				verBox.SelectedIndex = settings.paletteVersion;
			}
			catch
			{
				verBox.SelectedIndex = settings.paletteVersion = -1;
				settingsIsReset = true;
			}
			try
			{
				using (SettingsForm settForm = new())
				{
					settForm.delayBar.Value = settings.delay;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				settings.delay = 10;
				settingsIsReset = true;
			}
			if (settingsIsReset)
			{
				settings.Save();
				Warning(strings.someSettingsDamaged, false);
			}

			PrintPaletteLabel();

			russianLangItem.Checked = Lang == "ru";
			englishLangItem.Checked = Lang == "en";

			SetMenuItemsIcons();

			CountHiddenWindows();
		}

		public void DisplayProcessingLabel(bool displayPanel, bool displayLabel)
		{
			hideControlsPanel.Enabled = displayPanel;
			hideControlsPanel.Visible = displayPanel;
			processingLabel.Visible = displayLabel;
		}
		public void IncProgressBar()
		{
			progressBar.Value++;
		}
		public async void UpdateStatLabel(int estimated)
		{
			await Task.Delay(400);
			double percents = (double)progressBar.Value / progressBar.Maximum;

			if (percents < 1.0 && Math.Ceiling(percents * 100) == 100)
				percents = 0.99;

			progressStatLabel.Text = $"{progressBar.Value}/{progressBar.Maximum}";
			progressPercentsLabel.Text = $"({Math.Round(percents * 100)} %)";
			currentLayerLabel.Text = $"{strings.currentLayer}{Math.Ceiling(percents * OrigImgWH[1])}";
			if (progressTimeLabel.Visible)
				progressTimeLabel.Text = $"{strings.minutesRemaining}{Math.Ceiling(estimated - (estimated * percents))}";
		}
		public void OnRobotFinish(string mode = "")
		{
			if (mode == "closing")
			{
				MinimizeMinecraft(false);

				return;
			}

			if (settings.showProgress)
			{
				HideProgress();
				ClearProgress();
			}

			if (mode == "cancelled")
				OnRobotCancel();
			else
				Activate();

			notifyIcon.Visible = false;

			progressBar.Visible = false;
			progressLabel.Visible = false;
			progressStatLabel.Visible = false;
			progressPercentsLabel.Visible = false;

			currentLayerLabel.Visible = false;
			progressBar.Value = OrigImgWH[0] * (OrigImgWH[1] - GetImageWH(1));
			progressStatLabel.Text = "0/0";
			progressPercentsLabel.Text = "(0 %)";
			progressTimeLabel.Text = $"{strings.minutesRemaining}0";
			currentLayerLabel.Text = $"{strings.currentLayer}1";

			if (progressTimeLabel.Visible)
				progressTimeLabel.Visible = false;
			else
				currentLayerLabel.Location = new Point(progressTimeLabel.Location.X, progressTimeLabel.Location.Y + progressTimeLabel.Size.Height);

			verBox.Enabled = true;
			showPicAndPal.Enabled = true;

			blockCheckBox.Enabled = true;
			autoMinCheckBox.Enabled = !settings.vanishMinecraft;
			showProgressCheckBox.Enabled = true;

			continueCheckBox.Enabled = true;
			continueButton.Enabled = continueCheckBox.Checked;
			continueTextBox.Enabled = continueCheckBox.Checked;

			startStopButton.Text = strings.start;
			startStopButton.Enabled = true;

			if (settings.vanishMinecraft)
				MouseClick += MainForm_MouseClick;

			if (mode != "cancelled")
			{
				double spentMinutes = Math.Round(GetSpentTime());
				Info($"{strings.doneBuilding}{(spentMinutes == 0 ? strings.lessThanOne : spentMinutes)}.");
			}
		}

		public void RestoreCheckboxes()
		{
			blockCheckBox.CheckedChanged -= blockCheckBox_CheckedChanged;
			autoMinCheckBox.CheckedChanged -= autoMinCheckBox_CheckedChanged;
			showProgressCheckBox.CheckedChanged -= showProgressCheckBox_CheckedChanged;
			blockCheckBox.Checked = settings.blockInput;
			autoMinCheckBox.Checked = settings.vanishMinecraft || settings.hideMinecraft;
			showProgressCheckBox.Checked = settings.showProgress;
			blockCheckBox.CheckedChanged += blockCheckBox_CheckedChanged;
			autoMinCheckBox.CheckedChanged += autoMinCheckBox_CheckedChanged;
			showProgressCheckBox.CheckedChanged += showProgressCheckBox_CheckedChanged;
		}
		public void PrintPaletteLabel()
		{
			Color[] labelColors = new Color[] { Color.Red, Color.Orange, Color.Gold, Color.Green, Color.LightBlue, Color.Blue, Color.Magenta };

			verText.ResetText();
			verText.SelectionStart = 0;
			verText.SelectionLength = 0;

			if (Lang == "ru")
			{
				verText.SelectionColor = Color.Black;
				verText.AppendText(strings.paletteVer0);
			}

			for (int i = 0; i < 7; i++)
			{
				verText.SelectionColor = labelColors[i];
				verText.AppendText(strings.paletteVer1[i].ToString());
			}

			if (Lang == "en")
			{
				verText.SelectionColor = Color.Black;
				verText.AppendText(strings.paletteVer0);
			}
		}
		public void SetMenuItemsIcons()
		{
			exitItem.Image = GetIcon("exitIcon");
			openSettingsItem.Image = GetIcon("settingsIcon");
			resetSettingsItem.Image = GetIcon("resetSettingsIcon");
			openManualItem.Image = GetIcon("manualIcon");
			aboutItem.Image = GetIcon("aboutIcon");
			authorItem.Image = GetIcon("emailIcon");
		}

		private void showPicAndPal_Click(object sender, EventArgs e)
		{
			ShowPreview();
			ShowPalette(verBox.SelectedIndex != 0);
		}

		private void OnFileOpened(string fileName)
		{
			PictureBox pictureBox = new();
			try
			{
				pictureBox.Load(fileName);
			}
			catch
			{
				Error(strings.imgDamaged, false);
				return;
			}
			finally
			{
				pictureBox.Dispose();
			}

			DisplayProcessingLabel(true, true);

			Task.Run(() =>
			{
				int[] imgCWHE = ProcessImage(fileName);

				if (imgCWHE[0] != 0)
				{
					BeginInvoke((Action)delegate
					{
						DisplayProcessingLabel(false, false);

						widthLabel.Text = $"{strings.width}{imgCWHE[1]}";
						heightLabel.Text = $"{strings.height}{imgCWHE[2]}";
						colorsLabel.Text = $"{strings.colorCount}{imgCWHE[0]}";
						estimatedLabel.Text = $"{strings.takeMinutes}{((imgCWHE[3] == 0) ? strings.lessThanOne : imgCWHE[3])}";
					});
				}
				else
					BeginInvoke((Action)delegate
					{
						DisplayProcessingLabel(true, false);
					});
			});

			previewForm?.Close();
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			OnFileOpened(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
		}
		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void verBox_TextUpdate(object sender, EventArgs e)
		{
			verBox.Text = strings.chooseVer;
		}
		private void verBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!showPicAndPal.Enabled)
				showPicAndPal.Enabled = true;

			if (verBox.SelectedIndex == -1)
			{
				verBox.Text = strings.chooseVer;
				showPicAndPal.Enabled = false;
			}

			if (verBox.SelectedIndex == 0)
			{
				if (verBox.SelectedIndex != settings.paletteVersion)
					Warning(strings.noPalette, false);
			}
			else
				GeneratePalette(verBox.SelectedIndex);

			if (verBox.SelectedIndex != settings.paletteVersion)
			{
				settings.paletteVersion = (short)verBox.SelectedIndex;
				settings.Save();

				RecalculateImage();
			}
		}

		private async void EnableStartStopButton()
		{
			await Task.Delay(1000);
			startStopButton.Enabled = true;
		}
		private void startStopButton_Click(object sender, EventArgs e)
		{
			if (startStopButton.Text == strings.start)
			{
				if (StartMain())
				{
					HidePreview();
					HidePalette();

					MouseClick -= MainForm_MouseClick;

					verBox.Enabled = false;
					showPicAndPal.Enabled = false;

					blockCheckBox.Enabled = false;
					autoMinCheckBox.Enabled = false;
					showProgressCheckBox.Enabled = false;

					continueCheckBox.Enabled = false;
					continueButton.Enabled = false;
					continueTextBox.Enabled = false;

					startStopButton.Text = strings.cancel;
					startStopButton.Enabled = false;

					countdownTimer.Enabled = true;
					notifyIcon.BalloonTipTitle = strings.countdown;

					EnableStartStopButton();
				}
			}
			else
			{
				if (countdownTimer.Enabled)
				{
					countdownTimer.Enabled = false;
					notifyIcon.Visible = false;

					if (settings.vanishMinecraft)
					{
						autoMinCheckBox.Enabled = false;
						MouseClick += MainForm_MouseClick;
					}
					else
						autoMinCheckBox.Enabled = true;

					verBox.Enabled = true;
					showPicAndPal.Enabled = true;

					blockCheckBox.Enabled = true;
					showProgressCheckBox.Enabled = true;

					continueCheckBox.Enabled = true;
					if (continueCheckBox.Checked)
					{
						continueButton.Enabled = true;
						continueTextBox.Enabled = true;
					}
				}
				else
				{
					RobotRunning = false;
					while (!RobotFinished)
						Thread.Sleep(100);
					OnRobotFinish("cancelled");
					KeyUpAll();
				}

				startStopButton.Text = strings.start;
			}
		}

		private void countdownTimer_Tick(object sender, EventArgs e)
		{
			if (!notifyIcon.Visible)
			{
				Cd = settings.delay;
				notifyIcon.Icon = Icon;
			}

			notifyIcon.Visible = true;

			if (Cd == 0)
			{
				notifyIcon.BalloonTipTitle = "AutoPixelArt";
				notifyIcon.BalloonTipText = strings.startSuccess;
				notifyIcon.ShowBalloonTip(1);

				countdownTimer.Enabled = false;

				if (settings.showProgress)
				{
					FillProgress();
					ShowProgress();
				}

				progressBar.Visible = true;
				progressLabel.Visible = true;
				progressStatLabel.Visible = true;
				progressPercentsLabel.Visible = true;
				currentLayerLabel.Visible = true;
				if (!estimatedLabel.Text.Contains(strings.lessThanOne) && estimatedLabel.Text.Split(" - ")[^1] != "1")
				{
					progressTimeLabel.Text = $"{strings.minutesRemaining}{estimatedLabel.Text.Split(" - ")[^1]}";
					progressTimeLabel.Visible = true;
				}
				else
					currentLayerLabel.Location = new Point(currentLayerLabel.Location.X, progressTimeLabel.Location.Y);

				Task.Run(RobotThread);
			}
			else
			{
				notifyIcon.BalloonTipText = Cd.ToString();
				notifyIcon.ShowBalloonTip(1);
			}

			Cd--;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (RobotRunning)
			{
				e.Cancel = true;

				RobotRunning = false;
				while (!RobotFinished)
					Thread.Sleep(100);
				OnRobotFinish("closing");
				KeyUpAll();

				e.Cancel = false;
			}

			Application.Exit();
		}

		private void blockCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!settings.blockInput)
				Warning(strings.toCancelBlock, false);
			settings.blockInput = blockCheckBox.Checked;
			settings.Save();
		}

		private void autoMinCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			settings.hideMinecraft = autoMinCheckBox.Checked;
			settings.Save();
		}

		private void showProgressCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			settings.showProgress = showProgressCheckBox.Checked;
			settings.Save();
		}

		private void openItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new()
			{
				Filter = $"{strings.pic}|*.png; *.jpg; *.jpeg; *.bmp; *.dip; *.tif; *.tiff; *.gif|{strings.allFiles}|*.*",
				RestoreDirectory = true,
				Title = strings.openPic
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				OnFileOpened(openFileDialog.FileName);
			}

			openFileDialog.Dispose();
		}
		private void exitItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void aboutItem_Click(object sender, EventArgs e)
		{
			Info($"{strings.ver} - alpha {Application.ProductVersion}.\n{strings.author}\nE-mail - vlad2001.21@mail.ru");
		}

		private void authorItem_Click(object sender, EventArgs e)
		{
			Process.Start("explorer", "mailto:vlad2001.21@mail.ru");
		}

		private void resetSettingsItem_Click(object sender, EventArgs e)
		{
			if (Application.OpenForms["previewForm"] is not null)
			{
				HidePreview();
				if (Application.OpenForms["paletteForm"] is not null)
					HidePalette();
			}

			settings.Reset();
			settings.Save();

			blockCheckBox.CheckedChanged -= blockCheckBox_CheckedChanged;
			autoMinCheckBox.CheckedChanged -= autoMinCheckBox_CheckedChanged;
			showProgressCheckBox.CheckedChanged -= showProgressCheckBox_CheckedChanged;
			blockCheckBox.Checked = settings.blockInput;
			autoMinCheckBox.Checked = settings.hideMinecraft;
			showProgressCheckBox.Checked = settings.showProgress;
			verBox.SelectedIndex = settings.paletteVersion;
			blockCheckBox.CheckedChanged += blockCheckBox_CheckedChanged;
			autoMinCheckBox.CheckedChanged += autoMinCheckBox_CheckedChanged;
			showProgressCheckBox.CheckedChanged += showProgressCheckBox_CheckedChanged;

			Msg(strings.settingsReset);
		}
		private void openManualItem_Click(object sender, EventArgs e)
		{
			if (settings.firstOpened)
			{
				Warning(strings.readCarefully, false);

				settings.firstOpened = false;
				settings.Save();
			}

			if (manualForm is null || manualForm.IsDisposed)
			{
				manualForm = new ManualForm(Lang);
				manualForm.richTextBox.SelectAll();
				manualForm.richTextBox.SelectedRtf = Lang == "en" ? Properties.Resources.Manual_EN : Properties.Resources.Manual_RU;
				manualForm.richTextBox.SelectionStart = 0;
			}
			else
			{
				if (manualForm.WindowState == FormWindowState.Minimized)
					manualForm.WindowState = FormWindowState.Normal;
				manualForm.Activate();
			}

			manualForm.Show();
		}
		private void openSettingsItem_Click(object sender, EventArgs e)
		{
			settingsForm = new SettingsForm();
			settingsForm.Show();
		}

		private void continueCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ContinueEnabled = continueButton.Enabled = continueTextBox.Enabled = continueCheckBox.Checked;

			if (!ContinueEnabled && progressBar.Value != 0)
			{
				RecalculateImage();
				progressBar.Value = OrigImgWH[0] * (OrigImgWH[1] - GetImageWH(1));
			}
				
		}
		private void continueButton_Click(object sender, EventArgs e)
		{
			int enteredLayer = 0;
			try
			{
				enteredLayer = Convert.ToInt32(continueTextBox.Text) - 1;
			}
			catch
			{
				Error(strings.invalidNum, false);
				return;
			}

			if (enteredLayer < 1 || enteredLayer >= OrigImgWH[1])
			{
				Error($"{strings.validPicLayer}{OrigImgWH[1]}.", false);
				return;
			}

			Task.Run(() =>
			{
				Invoke((Action)delegate { CropImage(enteredLayer); });
			}
			).ContinueWith(delegate
			{
				if (Application.OpenForms["previewForm"] is not null)
					Invoke((Action)delegate { showPicAndPal.PerformClick(); });
			}
			);
		}

		private void continueTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				continueButton.PerformClick();
				e.Handled = e.SuppressKeyPress = true;
			}
		}

		public void MainForm_MouseClick(object sender, MouseEventArgs e)
		{
			if (!autoMinCheckBox.Enabled && autoMinCheckBox.Bounds.Contains(e.Location))
				Warning(strings.hideEnabled, false);
		}

		private void russianLangItem_Click(object sender, EventArgs e)
		{
			if (Lang != "ru")
			{
				russianLangItem.Checked = true;
				englishLangItem.Checked = false;

				settings.lang = Lang = "ru";
				settings.Save();

				string warnStr = strings.ResourceManager.GetObject("langChangeReset", new CultureInfo("ru")) as string;
				Warning(warnStr, false);
			}
		}
		private void englishLangItem_Click(object sender, EventArgs e)
		{
			if (Lang != "en")
			{
				englishLangItem.Checked = true;
				russianLangItem.Checked = false;

				settings.lang = Lang = "en";
				settings.Save();

				string warnStr = strings.ResourceManager.GetObject("langChangeReset", new CultureInfo("en-US")) as string;
				Warning(warnStr, false);
			}
		}
	}
}
