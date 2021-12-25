using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static MainLibrary.MainLib;

namespace AutoPixelArt
{
	public partial class PreviewForm : Form
	{
		private bool keepRatio;

		public PreviewForm()
		{
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Lang == "ru" ? "ru" : "en-US");
			InitializeComponent();
		}

		private void PreviewForm_Load(object sender, System.EventArgs e)
		{
			savePicItem.Image = saveUpPicItem.Image = GetIcon("saveIcon");
			previewPic.Size = ClientSize;

			chromeWidth = Width - ClientSize.Width;
			chromeHeight = Height - ClientSize.Height;
			constantWidth = previewPic.Width;
			constantHeight = previewPic.Height;
			keepRatio = true;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.S))
			{
				SavePicture(false);
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		//https://stackoverflow.com/a/10231213/12136394
		#region Resizer
		private float constantWidth;
		private float constantHeight;

		private int chromeWidth;
		private int chromeHeight;

		// From Windows SDK
		private const int WM_SIZING = 0x214;

		private const int WMSZ_LEFT = 1;
		private const int WMSZ_RIGHT = 2;
		private const int WMSZ_TOP = 3;
		private const int WMSZ_BOTTOM = 6;

		struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		protected override void WndProc(ref Message m)
		{
			if (keepRatio && m.Msg == WM_SIZING)
			{
				RECT rc = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));

				int w = rc.Right - rc.Left - chromeWidth;
				int h = rc.Bottom - rc.Top - chromeHeight;

				switch (m.WParam.ToInt32()) // Resize handle
				{
					case WMSZ_LEFT:
					case WMSZ_RIGHT:
						// Left or right handles, adjust height
						rc.Bottom = rc.Top + chromeHeight + (int)(constantHeight * w / constantWidth);
						break;

					case WMSZ_TOP:
					case WMSZ_BOTTOM:
						// Top or bottom handles, adjust width
						rc.Right = rc.Left + chromeWidth + (int)(constantWidth * h / constantHeight);
						break;

					case WMSZ_LEFT + WMSZ_TOP:
					case WMSZ_LEFT + WMSZ_BOTTOM:
						// Top-left or bottom-left handles, adjust width
						rc.Left = rc.Right - chromeWidth - (int)(constantWidth * h / constantHeight);
						break;

					case WMSZ_RIGHT + WMSZ_TOP:
						// Top-right handle, adjust height
						rc.Top = rc.Bottom - chromeHeight - (int)(constantHeight * w / constantWidth);
						break;

					case WMSZ_RIGHT + WMSZ_BOTTOM:
						// Bottom-right handle, adjust height
						rc.Bottom = rc.Top + chromeHeight + (int)(constantHeight * w / constantWidth);
						break;
				}

				Marshal.StructureToPtr(rc, m.LParam, true);
			}

			base.WndProc(ref m);
		}
		#endregion

		public void UpdatePicture(Bitmap pic)
		{
			previewPic.Image = pic;

			if (WindowState == FormWindowState.Minimized)
				WindowState = FormWindowState.Normal;
		}

		private void PreviewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.OpenForms["PaletteForm"]?.Dispose();
		}

		private void savePicItem_Click(object sender, System.EventArgs e)
		{
			SavePicture(false);
		}

		private void saveUpPicItem_Click(object sender, System.EventArgs e)
		{
			SavePicture(true);
		}

		private void keepRatioItem_Click(object sender, System.EventArgs e)
		{
			keepRatio = !keepRatio;
			keepRatioItem.Checked = keepRatio;
		}
	}

	public class PictureBoxWithInterpolationMode : PictureBox
	{
		public InterpolationMode InterpolationMode { get; set; }

		protected override void OnPaint(PaintEventArgs paintEventArgs)
		{
			paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			paintEventArgs.Graphics.InterpolationMode = InterpolationMode;

			base.OnPaint(paintEventArgs);
		}
	}
}
