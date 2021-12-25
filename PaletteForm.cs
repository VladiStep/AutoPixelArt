using MainLibrary;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoPixelArt
{
	public partial class PaletteForm : Form
	{
		public PaletteForm(string lang)
		{
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang == "ru" ? "ru" : "en-US");
			InitializeComponent();
		}

		public void HighlightColorCells(string blockName, bool highlight)
		{
			if (string.IsNullOrEmpty(blockName))
				return;

			List<byte> paletteTitleCells = MainLib.PaletteTitles[blockName];
			if (highlight)
			{
				for (byte i = 0; i < paletteTitleCells.Count; i++)
				{
					Control colorCell = Controls[paletteTitleCells[i]];
					Controls[paletteTitleCells[i]].BackgroundImage = colorCell.BackColor.A != 0 ? Properties.Resources.itemBorder_hl : Properties.Resources.itemBorder_bg_hl;
				}		
			}
			else
			{
				for (byte i = 0; i < paletteTitleCells.Count; i++)
				{
					Control colorCell = Controls[paletteTitleCells[i]];
					colorCell.BackgroundImage = colorCell.BackColor.A != 0 ? Properties.Resources.itemBorder : Properties.Resources.itemBorder_bg;
				}
			}
		}
	}

	public class ColorCell : Panel
	{
		public ColorCell(Color cellColor, int cellIndex)
		{
			DoubleBuffered = true;
			BackColor = cellColor;
			BackgroundImage = (cellColor.A != 0) ? Properties.Resources.itemBorder : Properties.Resources.itemBorder_bg;
			Cursor = Cursors.Cross;
			Location = new Point(cellIndex % 9 * 100, cellIndex / 9 * 100);
			Margin = new Padding(0);
			Name = $"colorCell{cellIndex}";
			Size = new Size(100, 100);
			MouseDown += delegate (object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
					MainLib.HighlightColor(cellColor, cellIndex, true);
			};
			MouseUp += delegate (object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
					MainLib.HighlightColor(cellColor, cellIndex, false);
			};

			labelPic = new LabelPic(cellIndex, cellColor);
			Controls.Add(labelPic);
		}

		public string ColorName { get; set; }
		public LabelPic labelPic;
	}
	public class LabelPic : PictureBox
	{
		public LabelPic(int labelIndex, Color color)
		{
			BackColor = Color.Transparent;
			Location = new Point(0, 47);
			Margin = new Padding(0);
			Name = $"labelPic{labelIndex}";
			Size = new Size(100, 44);
			SizeMode = PictureBoxSizeMode.CenterImage;
			MouseDown += delegate (object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
					MainLib.HighlightColor(color, labelIndex, true);
			};
			MouseUp += delegate (object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
					MainLib.HighlightColor(color, labelIndex, false);
			};
		}
	}
}
