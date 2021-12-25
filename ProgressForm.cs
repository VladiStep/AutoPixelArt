using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoPixelArt
{
	public partial class ProgressForm : Form
	{
		public ProgressForm(string lang)
		{
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang == "ru" ? "ru" : "en-US");
			InitializeComponent();
		}

		private void ProgressForm_Load(object sender, EventArgs e)
		{
			progressPic.Size = ClientSize;
		}

		public void UpdatePicture(Bitmap pic)
		{
			progressPic.BackgroundImage = pic;
			progressPic.Refresh();
		}
		public void ClearPicture()
		{
			progressPic.BackgroundImage.Dispose();
		}
	}
}
