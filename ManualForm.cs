using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoPixelArt
{
	public partial class ManualForm : Form
	{
		public ManualForm(string lang)
		{
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang == "ru" ? "ru" : "en-US");
			InitializeComponent();
		}

		private void ManualForm_Resize(object sender, EventArgs e)
		{
			richTextBox.Size = ClientSize;
		}
	}
}
