using AutoPixelArt;
using PInvoke;
//using AForge.Imaging.ColorReduction;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PInvoke.User32.VirtualKey;
using static PInvoke.User32.WindowMessage;
using static PInvoke.User32.WindowShowStyle;

namespace MainLibrary
{
	using strings = AutoPixelArt.Resources.Strings;

	public static class MainLib
	{
		public static string Lang { get; set; }

		private static readonly ushort t = 772;
		private static byte paletteCount;
		private static int scale, i, estimated;
		private static bool dir;
		private static Bitmap img, imgOrig, imgU, imgH, imgUC; //Upscaled, Highlighted, UnCropped
		private static volatile Bitmap imgP; //Progress
		private static int[,] imgArray;
		private static volatile Graphics gP; //Progress
		private static readonly Brush blackBrush = new SolidBrush(Color.Black);
		private static readonly Brush[] psBrushes = new SolidBrush[2] { new SolidBrush(Color.White), new SolidBrush(Color.FromArgb(204, 204, 204)) };
		private static HashSet<Color> colors, colorsOrig;
		private static Dictionary<int, int[]> colorsAddr;
		private static Dictionary<string, byte[]> paletteForVer;
		private static MainForm mainForm => (MainForm)Application.OpenForms["MainForm"];
		public static PreviewForm previewForm { get; set; }
		private static PaletteForm paletteForm;
		private static ProgressForm progressForm;
		private static readonly int[] hCount = new int[2];
		private static int[] prevItem = new int[2];
		private static readonly int[] prevXY = new int[2];
		private static readonly int[] imgWH = new int[2];
		private static IntPtr h;
		private static readonly List<IntPtr> hList = new();
		private static readonly Assembly assem = Assembly.GetExecutingAssembly();
		private static readonly AutoPixelArt.Properties.Settings settings = AutoPixelArt.Properties.Settings.Default;
		private static DateTime prevTime, endTime;
		private static readonly int sizeOfInput = Marshal.SizeOf(typeof(User32.INPUT));
		private static readonly User32.INPUT[][] inputsAlt = new User32.INPUT[][]
		{
			new User32.INPUT[]
			{
				new User32.INPUT
				{
					type = User32.InputType.INPUT_KEYBOARD,
					Inputs = new User32.INPUT.InputUnion
					{
						ki = new User32.KEYBDINPUT
						{
							wScan = User32.ScanCode.LMENU,
							wVk = VK_LMENU
						}
					}
				}
			},
			new User32.INPUT[]
			{
				new User32.INPUT
				{
					type = User32.InputType.INPUT_KEYBOARD,
					Inputs = new User32.INPUT.InputUnion
					{
						ki = new User32.KEYBDINPUT
						{
							wScan = User32.ScanCode.LMENU,
							wVk = VK_LMENU,
							dwFlags = User32.KEYEVENTF.KEYEVENTF_KEYUP
						}
					}
				}
			}
		};

		public static string FileName { get; set; }
		public static IntPtr HMain { get; set; }
		public static bool Debugging { get; set; }
		public static int[] OrigImgWH { get; set; }
		public static Dictionary<int, int> ColorsReplace { get; set; }
		public static bool RobotRunning { get; set; }
		public static bool RobotFinished { get; set; }
		public static bool ContinueEnabled { get; set; }
		public static bool DoNotCheckWindow { get; set; }
		public static Dictionary<string, List<byte>> PaletteTitles { get; set; }

		[DllImport("user32.dll")]
		private static extern bool BlockInput(bool fBlockIt);

		public static DialogResult Prompt(object msg, string category = "question")
		{
			MessageBoxIcon icon = category switch
			{
				"question" => MessageBoxIcon.Question,
				"warning" => MessageBoxIcon.Warning,
				"error" => MessageBoxIcon.Error,
				_ => MessageBoxIcon.None
			};
			return MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.YesNo, icon);
		}
		public static void Error(object msg, bool wait)
		{
			if (!wait)
				Task.Run(() => MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.OK, MessageBoxIcon.Error));
			else
				MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		public static void Warning(object msg, bool wait)
		{
			if (!wait)
				Task.Run(() => MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.OK, MessageBoxIcon.Warning));
			else
				MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		public static void Info(object msg)
		{
			Task.Run(() => MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.OK, MessageBoxIcon.Information));
		}
		public static void Msg(object msg)
		{
			Task.Run(() => MessageBox.Show(msg.ToString(), "AutoPixelArt", MessageBoxButtons.OK));
		}

		public static Stream GetTitleImage(string imageName)
		{
			return assem.GetManifestResourceStream($"AutoPixelArt.Resources.rendered_titles_{Lang}.{imageName}.png");
		}
		public static Bitmap GetIcon(string iconName)
		{
			return (Bitmap)AutoPixelArt.Properties.Resources.ResourceManager.GetObject(iconName);
		}

		public static bool EnumHandler(IntPtr hwnd, IntPtr lParam)
		{
			try {
				if (User32.GetWindowText(hwnd).Contains("Minecraft") && User32.GetClassName(hwnd).Contains("GL"))
				{
					if (!User32.IsIconic(hwnd))
					{
						h = hwnd;
						hCount[0]++;
					}
					else
					{
						hCount[1]++;
					}
				}
			}
			catch (Win32Exception e)
			{
				Msg($"Win32Exception: {e}");
				return false;
			}

			return true;
		}
		public static bool DebugEnumHandler(IntPtr hwnd, IntPtr lParam)
		{
			try {
				if (User32.GetWindowText(hwnd) == "Я - окно Minecraft" || User32.GetWindowText(hwnd).Contains("Minecraft") && User32.GetClassName(hwnd).Contains("GL"))
				{
					if (!User32.IsIconic(hwnd))
					{
						h = hwnd;
						hCount[0]++;
					}
					else
					{
						hCount[1]++;
					}
				}
			}
			catch (Win32Exception e)
			{
				Msg($"Win32Exception: {e}");
				return false;
			}

			return true;
		}
		public static bool HiddenEnumHandler(IntPtr hwnd, IntPtr lParam)
		{
			try
			{
				if (User32.GetWindowText(hwnd).Contains("Minecraft") && User32.GetClassName(hwnd).Contains("GL"))
					if (!User32.IsWindowVisible(hwnd))
						hList.Add(hwnd);
			}
			catch (Win32Exception e)
			{
				Msg($"Win32Exception: {e}");
				return false;
			}

			return true;
		}
		public static bool DebugHiddenEnumHandler(IntPtr hwnd, IntPtr lParam)
		{
			try
			{
				if (User32.GetWindowText(hwnd) == "Я - окно Minecraft" || User32.GetWindowText(hwnd).Contains("Minecraft") && User32.GetClassName(hwnd).Contains("GL"))
					if (!User32.IsWindowVisible(hwnd))
						hList.Add(hwnd);
			}
			catch (Win32Exception e)
			{
				Msg($"Win32Exception: {e}");
				return false;
			}
			
			return true;
		}

		public static void GenerateColorsAddr()
		{
			colorsAddr.Clear();

			i = 0;
			if (colors.Count <= 9)
			{
				foreach (Color c in colors)
				{
					colorsAddr.Add(c.ToArgb(), new int[2] { i + 1, -1 });

					i++;
				}
			}
			else
			{
				foreach (Color c in colors)
				{
					colorsAddr.Add(c.ToArgb(), new int[2] { i % 9 + 1, i / 9 + 1 });

					i++;
				}
			}
		}
		public static void GenerateImageArray()
		{
			BitmapData data = img.LockBits(new Rectangle(0, 0, imgWH[0], imgWH[1]), ImageLockMode.ReadOnly, img.PixelFormat);

			byte[] imgBytes = new byte[imgWH[1] * data.Stride];
			try
			{
				Marshal.Copy(data.Scan0, imgBytes, 0, imgBytes.Length);
			}
			finally
			{
				img.UnlockBits(data);
			}

			imgArray = new int[imgWH[0], imgWH[1]];

			for (int y = 0; y < imgWH[1]; y++)
			{
				for (int x = 0; x < imgWH[0]; x++)
				{
					int offset = y * data.Stride + x * 4;
					imgArray[x, y] = imgBytes[offset + 3] << 24 | imgBytes[offset + 2] << 16 | imgBytes[offset + 1] << 8 | imgBytes[offset];
				}
			}
		}
		public static void ReplaceImageColors(Dictionary<int, int> colorsReplace)
		{
			BitmapData data = img.LockBits(new Rectangle(0, 0, imgWH[0], imgWH[1]), ImageLockMode.ReadWrite, img.PixelFormat);

			byte[] imgBytes = new byte[imgWH[1] * data.Stride];

			Marshal.Copy(data.Scan0, imgBytes, 0, imgBytes.Length);

			imgArray = new int[imgWH[0], imgWH[1]];

			for (int y = 0; y < imgWH[1]; y++)
			{
				for (int x = 0; x < imgWH[0]; x++)
				{
					int offset = y * data.Stride + x * 4;
					int resColor = colorsReplace[imgBytes[offset + 3] << 24 | imgBytes[offset + 2] << 16 | imgBytes[offset + 1] << 8 | imgBytes[offset]];

					imgBytes[offset] = (byte)(resColor & 0xFF);
					imgBytes[offset + 1] = (byte)(resColor >> 8 & 0xFF);
					imgBytes[offset + 2] = (byte)(resColor >> 16 & 0xFF);
					imgBytes[offset + 3] = (byte)(resColor >> 24 & 0xFF);

					imgArray[x, y] = imgBytes[offset + 3] << 24 | imgBytes[offset + 2] << 16 | imgBytes[offset + 1] << 8 | imgBytes[offset];
				}
			}

			Marshal.Copy(imgBytes, 0, data.Scan0, imgBytes.Length);

			img.UnlockBits(data);
		}
		public static void MatchColorsWithPalette()
		{
			ColorsReplace = new Dictionary<int, int>();

			foreach (Color c in colors)
				ColorsReplace.Add(c.ToArgb(), GetClosestBlockColor(c));

			ReplaceImageColors(ColorsReplace);

			colors.Clear();
			int[] values = ColorsReplace.Values.ToArray();
			for (int i = 0; i < values.Length; i++)
				colors.Add(Color.FromArgb(values[i]));

			ColorsReplace.Clear();
		}
		public static void RecalculateImage(bool doAll = true)
		{
			if (imgOrig is null)
				return;

			if (doAll)
			{
				if (!ContinueEnabled)
				{
					img = new Bitmap(imgOrig);
					imgWH[1] = OrigImgWH[1];
					colors = new HashSet<Color>(colorsOrig);
				}

				if (settings.showOrigImg || settings.paletteVersion == 0)
					GenerateImageArray();
				else
					MatchColorsWithPalette();

				GenerateColorsAddr();

				imgUC = new Bitmap(img);
				imgU = new Bitmap(img, imgWH[0] * scale, imgWH[1] * scale);

				using (Graphics g = Graphics.FromImage(imgU))
				{
					g.Clear(Color.White);
					g.InterpolationMode = InterpolationMode.NearestNeighbor;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.DrawImage(img, 0, 0, imgU.Width, imgU.Height);
				}

				if (Application.OpenForms["previewForm"] is not null)
					mainForm.Invoke((Action)delegate { mainForm.showPicAndPal.PerformClick(); });
			}
			else
			{
				GeneratePalette(settings.paletteVersion);
				MatchColorsWithPalette();
			}

			mainForm.Invoke((Action)delegate
			{
				mainForm.colorsLabel.Text = $"{strings.colorCount}{colors.Count}";
			});
		}
		public static int ReduceImageColors()
		{
			if (string.IsNullOrEmpty(FileName))
				return 0;

			Graphics g;
			int wasColors;

			/*MedianCutQuantizer medianCut = new MedianCutQuantizer();
			ColorImageQuantizer colorQuantizer = new ColorImageQuantizer(medianCut) { UseCaching = false };
			JarvisJudiceNinkeColorDithering colorDithering = new JarvisJudiceNinkeColorDithering() { UseCaching = false };
				
			wasColors = colors.Count;

			colors = colorQuantizer.CalculatePalette(img, 81).ToHashSet();
			colorDithering.ColorTable = colors.ToArray();

			img = colorDithering.Apply(img);

			medianCut.Clear();
			medianCut = null;
			colorQuantizer = null;
			colorDithering = null;*/

			SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32> imgIS = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(FileName);

			g = Graphics.FromImage(new Bitmap(1, 1));
			imgIS.Metadata.ResolutionUnits = SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerInch;
			imgIS.Metadata.HorizontalResolution = g.DpiX;
			imgIS.Metadata.VerticalResolution = g.DpiY;
			g.Dispose();

			wasColors = colors.Count;
			colors.Clear();

			/*using (MemoryStream memoryStream = new MemoryStream())
			{
				img.Save(memoryStream, ImageFormat.Png);

				memoryStream.Seek(0, SeekOrigin.Begin);

				imgIS = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(memoryStream, new SixLabors.ImageSharp.Formats.Png.PngDecoder());
			}*/

			SixLabors.ImageSharp.Processing.Processors.Quantization.IQuantizer quantizer = new SixLabors.ImageSharp.Processing.Processors.Quantization.OctreeQuantizer(
				new SixLabors.ImageSharp.Processing.Processors.Quantization.QuantizerOptions { MaxColors = 81 });

			try
			{
				imgIS.Mutate(x => x.Quantize(quantizer));
			}
			catch
			{
				Error(strings.reduceError, false);

				g.Dispose();
				img.Dispose();
				imgIS.Dispose();

				return 0;
			}

			for (int y = 0; y < imgIS.Height; y++)
			{
				Span<SixLabors.ImageSharp.PixelFormats.Rgba32> pixelRowSpan = imgIS.GetPixelRowSpan(y);
				for (int x = 0; x < imgIS.Width; x++)
				{
					colors.Add(Color.FromArgb(pixelRowSpan[x].A, pixelRowSpan[x].R, pixelRowSpan[x].G, pixelRowSpan[x].B));
				}
			}

			using (MemoryStream memoryStream = new())
			{
				imgIS.Save(memoryStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());

				memoryStream.Seek(0, SeekOrigin.Begin);

				using (Bitmap imgT = new(memoryStream))
				{
					img = new Bitmap(imgWH[0], imgWH[1], PixelFormat.Format32bppArgb);
					g = Graphics.FromImage(img);
					g.DrawImage(imgT, 0, 0);
					g.Dispose();
				}
			}

			imgIS.Dispose();

			if (settings.showOrigImg || settings.paletteVersion == 0)
				GenerateImageArray();

			return wasColors;
		}
		public static int[] ProcessImage(string fileName)
		{
			FileName = fileName;
			colors = new HashSet<Color>();
			colorsAddr = new Dictionary<int, int[]>();
			int wasColors = 0;
			Graphics g;

			using (Bitmap imgT = new(fileName))
			{
				g = Graphics.FromImage(new Bitmap(1, 1));
				imgT.SetResolution(g.DpiX, g.DpiY);

				img = new Bitmap(imgT.Width, imgT.Height, PixelFormat.Format32bppArgb);
				imgWH[0] = img.Width;
				imgWH[1] = img.Height;
				OrigImgWH = (int[])imgWH.Clone();

				g = Graphics.FromImage(img);
				g.DrawImage(imgT, 0, 0);
				g.Dispose();
			}

			estimated = (int)Math.Round((imgWH[0] * imgWH[1] * 125 + imgWH[1] * 200 + (imgWH[0] * imgWH[1] - imgWH[1]) * (t - 125) + imgWH[1] * (485 - 200) + 250) / 1000.0 / 60.0);

			GenerateImageArray();
			for (int y = 0; y < imgWH[1]; y++)
				for (int x = 0; x < imgWH[0]; x++)
					colors.Add(Color.FromArgb(imgArray[x, y]));

			if (colors.Count > 81)
				wasColors = ReduceImageColors();

			imgOrig = new Bitmap(img);

			colors = colors.OrderBy(x => x.R).ThenBy(x => x.G).ThenBy(x => x.B).ToHashSet();
			colorsOrig = new HashSet<Color>(colors);

			if (!settings.showOrigImg && settings.paletteVersion > 0)
				RecalculateImage(false);

			prevXY[1] = imgWH[1] - 1;

			GenerateColorsAddr();

			string imageName = FileName.Split('\\')[^1];
			int requiredWidth = 125 + TextRenderer.MeasureText(imageName[..Math.Min(imageName.Length, 32)], SystemFonts.CaptionFont).Width;
			scale = (imgWH[0] <= 400) ? (100 / imgWH[0]) : 1;
			scale = Math.Clamp(scale, 1, 34);
			if (imgWH[0] * scale < requiredWidth)
				scale = requiredWidth / imgWH[0] + 1;


			imgU = new Bitmap(img, imgWH[0] * scale, imgWH[1] * scale);

			g = Graphics.FromImage(imgU);
			g.Clear(Color.White);
			g.InterpolationMode = InterpolationMode.NearestNeighbor;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;
			g.DrawImage(img, 0, 0, imgU.Width, imgU.Height);
			g.Dispose();

			imgUC = new Bitmap(img);
			//img.Dispose();

			if (wasColors != 0)
				Warning($"{strings.colorsReduced0}{colors.Count} ({strings.colorsReduced1}{wasColors}).", false);

			mainForm.progressBar.Invoke((Action)delegate
			{
				mainForm.progressBar.Maximum = OrigImgWH[0] * OrigImgWH[1];
			});

			return new int[4] { colors.Count, imgWH[0], imgWH[1], estimated };
		}
		public static void CropImage(int cropFrom)
		{
			img = imgUC.Clone(new Rectangle(0, 0, OrigImgWH[0], OrigImgWH[1] - cropFrom), PixelFormat.Format32bppArgb);
			imgWH[1] = img.Height;
			prevXY[1] = img.Height - 1;

			estimated = (ushort)Math.Round((imgWH[0] * imgWH[1] * 125 + imgWH[1] * 200 + (imgWH[0] * imgWH[1] - imgWH[1]) * (t - 125) + imgWH[1] * (485 - 200) + 250) / 1000.0 / 60.0);

			mainForm.progressBar.Value = OrigImgWH[0] * cropFrom;

			imgU = new Bitmap(img, imgWH[0] * scale, imgWH[1] * scale);

			using (Graphics g = Graphics.FromImage(imgU))
			{
				g.Clear(Color.White);
				g.InterpolationMode = InterpolationMode.NearestNeighbor;
				g.PixelOffsetMode = PixelOffsetMode.HighQuality;
				g.DrawImage(img, 0, 0, imgU.Width, imgU.Height);
			}

			GenerateImageArray();
		}
		public static void GeneratePalette(int paletteVer)
		{
			if (paletteVer == -1)
			{
				paletteForVer = null;
				return;
			}

			paletteCount = (byte)typeof(Palette).GetFields().Length;
			paletteForVer = new Dictionary<string, byte[]>(Palette.v1_8);

			if (paletteVer != 1)
			{
				for (int i = 1; i < paletteCount - (paletteCount - paletteVer); i++)
				{
					Dictionary<string, byte[]> d1 = (Dictionary<string, byte[]>)typeof(Palette).GetFields()[i].GetValue(null);

					foreach (KeyValuePair<string, byte[]> kvp in d1)
					{
						if (!paletteForVer.TryAdd(kvp.Key, kvp.Value))
						{
							paletteForVer[kvp.Key] = kvp.Value;
						}
					}

					/*using (StreamWriter file = new StreamWriter($"palette_for_ver({i}).txt"))
					{
						foreach (KeyValuePair<string, byte[]> keyValuePair in palette_for_ver)
						{
							file.WriteLine($"{{ \"{keyValuePair.Key}\", {{ {keyValuePair.Value[0]}, {keyValuePair.Value[1]}, {keyValuePair.Value[2]} }},");
						}
					}*/
				}
			}
		}
		public static string GetClosestBlock(Color c)
		{
			int minError = 99999;
			string bestBlock = string.Empty;

			foreach (KeyValuePair<string, byte[]> kvp in paletteForVer)
			{
				int curError = (int)Math.Sqrt(Math.Pow((double)c.R - kvp.Value[0], 2) + Math.Pow((double)c.G - kvp.Value[1], 2) + Math.Pow((double)c.B - kvp.Value[2], 2));

				if (curError < minError)
				{
					minError = curError;
					bestBlock = kvp.Key;
				}
			}

			return bestBlock;
		}
		public static int GetClosestBlockColor(Color c)
		{
			int minError = 99999;
			int bestColor = 0;

			if (c.A == 0)
				return 0;

			foreach (KeyValuePair<string, byte[]> kvp in paletteForVer)
			{
				int curError = (int)Math.Sqrt(Math.Pow((double)c.R - kvp.Value[0], 2) + Math.Pow((double)c.G - kvp.Value[1], 2) + Math.Pow((double)c.B - kvp.Value[2], 2));

				if (curError < minError)
				{
					minError = curError;
					byte[] value = kvp.Value;
					bestColor = 255 << 24 | value[0] << 16 | value[1] << 8 | value[2];
				}
			}

			return bestColor;
		}
		public static int GetImageWH(byte index)
		{
			return imgArray.GetLength(index);
		}

		/*public static bool IsSneakButtonSet()
		{
			File.ReadAllText()
		}*/

		public static void LKey(bool state)
		{
			if (state)
				User32.SendMessage(h, WM_LBUTTONDOWN, (IntPtr)VK_LBUTTON, (IntPtr)0);
			else
				User32.SendMessage(h, WM_LBUTTONUP, (IntPtr)VK_LBUTTON, (IntPtr)0);
		}
		public static void RKey(bool state)
		{
			if (state)
				User32.SendMessage(h, WM_RBUTTONDOWN, (IntPtr)VK_RBUTTON, (IntPtr)0);
			else
				User32.SendMessage(h, WM_RBUTTONUP, (IntPtr)VK_RBUTTON, (IntPtr)0);
		}
		public static void Esc()
		{
			User32.SendMessage(h, WM_KEYDOWN, (IntPtr)VK_ESCAPE, (IntPtr)0x00010001);
			User32.SendMessage(h, WM_KEYUP, (IntPtr)VK_ESCAPE, (IntPtr)0xC0010001);
		}

		public static void CountHiddenWindows()
		{
			bool found = false;
			string msg = string.Empty;

			hList.Clear();
			if (Debugging)
				User32.EnumWindows(DebugHiddenEnumHandler, IntPtr.Zero);
			else
				User32.EnumWindows(HiddenEnumHandler, IntPtr.Zero);

			if (hList.Count == 1)
			{
				msg = strings.hiddenFound;
				found = true;
			}
			else if (hList.Count > 1)
			{
				msg = strings.hiddenFoundSev;
				found = true;
			}

			if (found)
				if (Prompt(msg) == DialogResult.Yes)
				{
					foreach (IntPtr ptr in hList)
					{
						User32.ShowWindow(ptr, SW_MINIMIZE);
						User32.ShowWindow(ptr, SW_SHOWNORMAL);
					}

					Info(hList.Count == 1 ? strings.hiddenRestored : strings.hiddenRestoredSev);
				}
		}
		public static void MinimizeMinecraft(bool state, bool vanish = false)
		{
			DoNotCheckWindow = true;

			if (state)
			{
				User32.ShowWindow(h, SW_MINIMIZE);
				if (vanish)
					User32.ShowWindow(h, SW_HIDE);

				User32.SendInput(1, inputsAlt[0], sizeOfInput);
				User32.SetForegroundWindow(HMain);
				Thread.Sleep(100);
				User32.SendInput(1, inputsAlt[1], sizeOfInput);
			}
			else
			{
				User32.ShowWindow(h, SW_MINIMIZE);
				User32.ShowWindow(h, SW_SHOWNORMAL);
			}

			Task.Run(() =>
			{
				Thread.Sleep(200);
				DoNotCheckWindow = false;
			});
		}

		public static void SelectItem(int[] colorAddr)
		{
			if (!DoNotCheckWindow && !User32.IsWindow(h))
			{
				mainForm.Invoke((Action)delegate
				{
					string layer = mainForm.currentLayerLabel.Text.Split(" - ")[^1];

					if (layer == "1")
						Error(strings.windowLost, false);
					else
					{
						Error($"{strings.windowLost}\n{strings.unfinishedLayerNum0}{layer} ({strings.unfinishedLayerNum1}).", false);
						mainForm.continueTextBox.Text = layer;
					}

					mainForm.startStopButton.PerformClick();
				});

				while (!RobotFinished)
					Thread.Sleep(100);
				return;
			}

			if (colorAddr[0] == prevItem[0] && colorAddr[1] == prevItem[1])
				Thread.Sleep(125);
			else
			{
				uint lParamUp1 = 0;
				uint lParamUp2 = 0;
				uint lParamDown1 = 0;
				uint lParamDown2 = 0;

				if (1 <= colorAddr[0] && colorAddr[0] <= 8)
				{
					lParamUp1 = Convert.ToUInt32($"0xC00{1 + colorAddr[0]}0001", 16);
					lParamDown1 = Convert.ToUInt32($"0x000{1 + colorAddr[0]}0001", 16);
				}
				else if (colorAddr[0] == 9)
				{
					lParamUp1 = 0xC00A0001;
					lParamDown1 = 0x000A0001;
				}

				if (1 <= colorAddr[1] && colorAddr[1] <= 8)
				{
					lParamUp2 = Convert.ToUInt32($"0xC00{1 + colorAddr[1]}0001", 16);
					lParamDown2 = Convert.ToUInt32($"0x000{1 + colorAddr[1]}0001", 16);
				}
				else if (colorAddr[1] == 9)
				{
					lParamUp2 = 0xC00A0001;
					lParamDown2 = 0x000A0001;
				}

				if (colorAddr[1] != -1 && colorAddr[1] != prevItem[1])
				{
					User32.SendMessage(h, WM_KEYDOWN, (IntPtr)'X', (IntPtr)0x002D0001);
					Thread.Sleep(25);
					User32.SendMessage(h, WM_KEYDOWN, (IntPtr)0x00000030 + colorAddr[1], (IntPtr)lParamDown2);
					Thread.Sleep(25);
					User32.SendMessage(h, WM_KEYUP, (IntPtr)0x00000030 + colorAddr[1], (IntPtr)lParamUp2);
					Thread.Sleep(25);
					User32.SendMessage(h, WM_KEYUP, (IntPtr)'X', (IntPtr)0xC02D0001);
					Thread.Sleep(25);
				}
				else
					Thread.Sleep(100);

				User32.SendMessage(h, WM_KEYDOWN, (IntPtr)0x00000030 + colorAddr[0], (IntPtr)lParamDown1);
				Thread.Sleep(25);
				User32.SendMessage(h, WM_KEYUP, (IntPtr)0x00000030 + colorAddr[0], (IntPtr)lParamUp1);

				prevItem = (int[])colorAddr.Clone();
			}
		}
		public static void ChangeDir()
		{
			KeyUp(dir ? 'D' : 'A');
			KeyDown(dir ? 'A' : 'D');
		}
		public static void KeyDown(char key)
		{
			var lParam = key switch
			{
				'W' => 0x00110001,
				'A' => 0x001e0001,
				'S' => 0x001f0001,
				'D' => 0x00200001,
				_ => 0,
			};
			User32.SendMessage(h, WM_KEYDOWN, (IntPtr)key, (IntPtr)lParam);
		}
		public static void KeyUp(char key)
		{
			var lParam = key switch
			{
				'W' => 0xC0110001,
				'A' => 0xC01e0001,
				'S' => 0xC01f0001,
				'D' => 0xC0200001,
				_ => (uint)0,
			};
			User32.SendMessage(h, WM_KEYUP, (IntPtr)key, (IntPtr)lParam);
		}
		public static void Slow(bool isSlow)
		{
			if (isSlow)
			{
				User32.SendMessage(h, WM_KEYDOWN, (IntPtr)'Z', (IntPtr)0x002C0001);
			}
			else
			{
				User32.SendMessage(h, WM_KEYUP, (IntPtr)'Z', (IntPtr)0xC02C0001);
			}
		}
		public static void Jump()
		{
			User32.SendMessage(h, WM_KEYDOWN, (IntPtr)VK_SPACE, (IntPtr)0x00390001);
			Thread.Sleep(200);
			User32.SendMessage(h, WM_KEYUP, (IntPtr)VK_SPACE, (IntPtr)0xC0390001);
		}

		public static void KeyUpAll()
		{
			uint lParamUp;

			Slow(false);
			RKey(false);
			KeyUp('A');
			KeyUp('D');
			KeyUp('X');

			for (int i = 1; i <= 8; i++)
			{
				lParamUp = Convert.ToUInt32($"0xC00{1 + i}0001", 16);
				User32.SendMessage(h, WM_KEYUP, (IntPtr)0x00000030 + i, (IntPtr)lParamUp);
			}
			lParamUp = 0xC00A0001;
			User32.SendMessage(h, WM_KEYUP, (IntPtr)0x00000039, (IntPtr)lParamUp);
		}

		public static bool IsNeededToFocus()
		{
			IntPtr hPreview = previewForm.Handle;
			IntPtr hPalette = paletteForm.Handle;
			IntPtr hCur = hPalette;

			byte visibleCount = 0;

			while (visibleCount <= 2)
			{
				hCur = User32.GetNextWindow(hCur, User32.GetNextWindowCommands.GW_HWNDNEXT);
				if (User32.IsWindowVisible(hCur))
				{
					if (hCur == hPreview)
						break;

					visibleCount++;
				}
			}

			return !(visibleCount == 0);
		}
		public static void HighlightColor(Color c, int cellIndex, bool draw)
		{
			if (draw)
			{
				using (Graphics gH = Graphics.FromImage(imgH))
				{
					Rectangle[] rects;
					int rectCount = 0;
					gH.Clear(Color.White);

					for (int y = 0; y < imgWH[1]; y++)
						for (int x = 0; x < imgWH[0]; x++)
							if (imgArray[x, y] == c.ToArgb())
								rectCount++;

					rects = new Rectangle[rectCount];
					rectCount = 0;

					for (int y = 0; y < imgWH[1]; y++)
						for (int x = 0; x < imgWH[0]; x++)
							if (imgArray[x, y] == c.ToArgb())
							{
								rects[rectCount] = new Rectangle(x * scale, y * scale, scale, scale);
								rectCount++;
							}

					gH.FillRectangles(blackBrush, rects);

					previewForm.UpdatePicture(imgH);
				}
			}
			else
				previewForm.UpdatePicture(imgU);

			paletteForm.HighlightColorCells((paletteForm.Controls[cellIndex] as ColorCell).ColorName, draw);

			if (IsNeededToFocus())
				previewForm.Focus();
		}
		public static void FillProgress()
		{
			imgP = new Bitmap(imgU.Width, imgU.Height);
			gP = Graphics.FromImage(imgP);
			byte yMod;

			for (byte y = 0; y < imgWH[1]; y++)
			{
				yMod = (byte)((y + 1) % 2);
				for (byte x = 0; x < imgWH[0]; x++)
					gP.FillRectangle(psBrushes[(x + yMod) % 2], x * scale, y * scale, scale, scale);
			}
		}
		public static void ClearProgress()
		{
			imgP.Dispose();
			gP.Dispose();
			progressForm.ClearPicture();
		}

		public static void SavePicture(bool upscaled)
		{
			ImageFormat format;
			ImageCodecInfo encoder;
			EncoderParameters parameters = new(2);
			parameters.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionNone);
			parameters.Param[1] = new EncoderParameter(Encoder.Quality, 100L);

			string fileName = FileName.Split('\\')[^1];
			string fileExt = fileName.Split('.')[^1].ToLower();
			if (fileExt == fileName)
				fileExt = string.Empty;

			SaveFileDialog saveFileDialog = new()
			{
				Filter = "BMP|*.bmp; *.rle; *.dip|TIFF|*.tiff; *.tif|PNG|*.png",
				FilterIndex = 4,
				RestoreDirectory = true,
				FileName = fileName,
				DefaultExt = "png",
				Title = strings.savePic
			};

			switch (fileExt)
			{
				case "bmp":
				case "rle":
				case "dip":
					saveFileDialog.FilterIndex = 1;
					break;

				case "tif":
				case "tiff":
					saveFileDialog.FilterIndex = 2;
					break;

				default:
					fileExt = string.Empty;
					saveFileDialog.FileName = $"{string.Concat(fileName.Split('.')[..^1])}.png";
					break;
			}
			if (!string.IsNullOrEmpty(fileExt))
				saveFileDialog.DefaultExt = fileExt;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileName = saveFileDialog.FileName.Split('\\')[^1];
				fileExt = fileName.Split('.')[^1].ToLower();
				if (fileExt == fileName)
					fileExt = string.Empty;

				format = fileExt switch
				{
					"bmp" or "rle" or "dip" => ImageFormat.Bmp,
					"tif" or "tiff" => ImageFormat.Tiff,
					_ => ImageFormat.Png,
				};
				encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == format.Guid);

				if (upscaled)
					imgU.Save(saveFileDialog.FileName, encoder, parameters);
				else
					img.Save(saveFileDialog.FileName, encoder, parameters);

				Info(strings.saved);
			}

			saveFileDialog.Dispose();
		}

		public static void ShowPreview()
		{
			previewForm?.Dispose();

			imgH?.Dispose();
			imgH = new Bitmap(imgU.Width, imgU.Height);

			Point previewXY = new(mainForm.Location.X + mainForm.Width, mainForm.Location.Y);

			previewForm = new PreviewForm
			{
				Text = FileName.Split('\\').Last(),
				ClientSize = new Size(
					imgU.Width,
					(imgWH[1] * scale >= Screen.PrimaryScreen.Bounds.Height - 200)
					? Screen.PrimaryScreen.Bounds.Height - 100 : imgU.Height
				)
			};
			previewForm.Location = previewXY;
			previewForm.UpdatePicture(imgU);

			previewForm.Show();
		}
		public static void ShowPalette(bool matchColors)
		{
			Bitmap imgT;
			PaletteTitles = new Dictionary<string, List<byte>>();
			Screen curScr = Screen.FromControl(mainForm);

			Point paletteXY = new(previewForm.Location.X + previewForm.Width, previewForm.Location.Y);
			if (curScr.Bounds.Width - paletteXY.X <= 300)
			{
				paletteXY.X = mainForm.Location.X;
				paletteXY.Y += mainForm.Height;
			}

			paletteForm?.Dispose();

			paletteForm = new PaletteForm(Lang)
			{
				ClientSize = new Size(
					(colors.Count >= 9) ? 900 : (colors.Count % 9 * 100),
					((colors.Count - 1) / 9 + 1) * 100
			),
				Location = paletteXY
			};

			i = 0;
			foreach (Color c in colors)
			{
				string closestBlock = string.Empty;

				if (matchColors)
				{
					if (c.A == 0)
					{
						closestBlock = "barrier";
						imgT = new Bitmap(GetTitleImage("barrier"));
					}
					else
					{
						closestBlock = GetClosestBlock(c);

						imgT = new Bitmap(GetTitleImage(closestBlock));
						for (int y = 0; y < imgT.Height; y++)
							for (int x = 0; x < imgT.Width; x++)
							{
								if (imgT.GetPixel(x, y) == Color.FromArgb(255, 255, 255))
									imgT.SetPixel(
										x, y,
										Color.FromArgb(
											(c.R >= 123 && c.R <= 132) ? 255 : (255 - c.R),
											(c.G >= 123 && c.G <= 132) ? 255 : (255 - c.G),
											(c.B >= 123 && c.B <= 132) ? 255 : (255 - c.B)
										)
									);
							}
					}

					if (!PaletteTitles.ContainsKey(closestBlock))
						PaletteTitles[closestBlock] = new List<byte>() { (byte)i };
					else
						PaletteTitles[closestBlock].Add((byte)i);
				}
				else
					imgT = null;

				ColorCell colorCell = new(c, i)
				{
					ColorName = closestBlock
				};
				colorCell.labelPic.Image = imgT;
				paletteForm.Controls.Add(colorCell);

				i++;
			}

			paletteForm.Show();
		}
		public static void ShowProgress()
		{
			progressForm?.Dispose();

			progressForm = new ProgressForm(Lang)
			{
				ClientSize = new Size(imgP.Width, imgP.Height),
				Location = new Point(mainForm.Location.X + mainForm.Width, mainForm.Location.Y),
			};
			progressForm.UpdatePicture(imgP);

			progressForm.Show();
		}
		public static void HidePreview()
		{
			previewForm?.Dispose();
		}
		public static void HidePalette()
		{
			paletteForm?.Dispose();
		}
		public static void HideProgress()
		{
			progressForm?.Dispose();
		}

		public static bool StartMain()
		{
			hCount[0] = 0;
			hCount[1] = 0;
			if (Debugging)
				User32.EnumWindows(DebugEnumHandler, IntPtr.Zero);
			else
				User32.EnumWindows(EnumHandler, IntPtr.Zero);

			if ((hCount[0] + hCount[1]) == 0)
			{
				Error(strings.windowNotFound, true);
				return false;
			}

			if ((hCount[0] + hCount[1]) > 1)
			{
				if (hCount[0] == 1)
				{
					Warning($"{strings.openedSevWindows0}\n{strings.openedSevWindows1}", true);
					return true;
				}
				else
					Warning($"{strings.openedSevWindows0}\n{strings.openedSevWindows2}", true);

				hCount[0] = 0;
				hCount[1] = 0;
				if (Debugging)
					User32.EnumWindows(DebugEnumHandler, IntPtr.Zero);
				else
					User32.EnumWindows(EnumHandler, IntPtr.Zero);

				if (hCount[0] > 1)
				{
					Error(strings.stillSevWindows, true);
					return false;
				}
				else if (hCount[0] == 0)
				{
					Error(strings.noWindow, true);
					return false;
				}
			}

			return true;
		}

		public static int Elapsed(Stopwatch time)
		{
			return (int)time.ElapsedMilliseconds;
		}

		public static void SleepAbs(long ms)
		{
			if (ms > 0)
				Thread.Sleep((int)ms);
		}
		public static double GetSpentTime()
		{
			return endTime.Subtract(prevTime).TotalMinutes;
		}
		public static void OnRobotCancel()
		{
			dir = false;
			prevXY[0] = 0;
			prevXY[1] = imgWH[1] - 1;

			if (AutoPixelArt.Properties.Settings.Default.pressEsc)
				Esc();

			if (settings.hideMinecraft || settings.vanishMinecraft)
				MinimizeMinecraft(false);
		}
		public static void RobotThread()
		{
			if (settings.blockInput)
			{
				try
				{
					BlockInput(true);
				}
				catch (Exception e)
				{
					if (Prompt($"{strings.blockError0}{e.Message}.\n{strings.blockError1}\n\n{strings.blockError2}", "error") == DialogResult.No)
						return;
				}
			}
			if (settings.hideMinecraft)
				MinimizeMinecraft(true, settings.vanishMinecraft);

			int x1, y1;
			prevTime = DateTime.Now;
			Stopwatch prevTime1 = Stopwatch.StartNew();
			RobotRunning = true;

			SelectItem(colorsAddr[imgArray[0, imgWH[1] - 1]]);
			Slow(true);
			RKey(true);

			for (int y = 0; y < imgWH[1]; y++)
			{
				if (!RobotRunning)
				{
					RobotFinished = true;
					return;
				}

				x1 = dir ? (imgWH[0] - 1) : 0;
				y1 = imgWH[1] - y - 1;

				if (y != 0)
				{
					if (!RobotRunning)
					{
						RobotFinished = true;
						return;
					}

					SleepAbs(t - Elapsed(prevTime1));
					SelectItem(colorsAddr[imgArray[x1, y1]]);
				}

				prevTime1.Restart();
				UpdateStat(x1, y1);
				Jump();
				ChangeDir();

				if (!dir)
				{
					for (int x = 1; x < imgWH[0]; x++)
					{
						if (!RobotRunning)
						{
							RobotFinished = true;
							return;
						}

						if (x != 1)
						{
							SleepAbs(t - Elapsed(prevTime1));
						}
						else
						{
							SleepAbs(485 - Elapsed(prevTime1));
						}

						prevTime1.Restart();
						SelectItem(colorsAddr[imgArray[x, y1]]);
						UpdateStat(x, y1);
					}
				}
				else
				{
					for (int x = imgWH[0] - 2; x >= 0; x--)
					{
						if (!RobotRunning)
						{
							RobotFinished = true;
							return;
						}

						if (x != imgWH[0] - 2)
						{
							SleepAbs(t - Elapsed(prevTime1));
						}
						else
						{
							SleepAbs(485 - Elapsed(prevTime1));
						}
						prevTime1.Restart();
						SelectItem(colorsAddr[imgArray[x, y1]]);
						UpdateStat(x, y1);
					}
				}

				if (y == imgWH[1] - 1)
				{
					SleepAbs(t - Elapsed(prevTime1));
				}

				dir = !dir;
			}

			prevTime1.Stop();

			dir = false;
			prevXY[0] = 0;
			prevXY[1] = imgWH[1] - 1;
			KeyUp('A');
			KeyUp('D');
			RKey(false);
			Slow(false);

			Thread.Sleep(250);
			if (AutoPixelArt.Properties.Settings.Default.pressEsc)
				Esc();

			endTime = DateTime.Now;

			if (settings.blockInput)
			{
				try
				{
					BlockInput(false);
				}
				catch { }
			}

			if (settings.vanishMinecraft || settings.hideMinecraft)
				MinimizeMinecraft(false);

			RobotFinished = true;

			mainForm.Invoke((Action) delegate { mainForm.OnRobotFinish(); });
		}
		public static async void UpdateStat(int pixelX, int pixelY)
		{
			Color curPixel = Color.FromArgb(imgArray[pixelX, pixelY]);

			mainForm.BeginInvoke((Action)delegate
			{
				mainForm.IncProgressBar();
				mainForm.UpdateStatLabel(estimated);
			});

			await Task.Delay(400);

			if (settings.showProgress && !progressForm.Disposing && !progressForm.IsDisposed)
			{
				try
				{
					progressForm.BeginInvoke((Action)delegate
					{
						if (prevXY[0] != pixelX || prevXY[1] != pixelY)
							gP.FillRectangle(new SolidBrush(Color.FromArgb(imgArray[prevXY[0], prevXY[1]])), prevXY[0] * scale, prevXY[1] * scale, scale, scale);

						gP.FillRectangle(new SolidBrush(
							Color.FromArgb(
								(curPixel.R >= 123 && curPixel.R <= 132) ? 255 : (255 - curPixel.R),
								(curPixel.G >= 123 && curPixel.G <= 132) ? 255 : (255 - curPixel.G),
								(curPixel.B >= 123 && curPixel.B <= 132) ? 255 : (255 - curPixel.B)
							)),
							pixelX * scale, pixelY * scale, scale, scale);

						prevXY[0] = pixelX;
						prevXY[1] = pixelY;

						progressForm.UpdatePicture(imgP);
					});

					await Task.Delay(250);

					progressForm.BeginInvoke((Action)delegate
					{
						gP.FillRectangle(new SolidBrush(curPixel), pixelX * scale, pixelY * scale, scale, scale);
						progressForm.UpdatePicture(imgP);
					});
				}
				catch (InvalidOperationException) { }
			}
		}
	}
}