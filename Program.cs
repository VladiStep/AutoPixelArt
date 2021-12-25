using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static MainLibrary.MainLib;

namespace AutoPixelArt
{
	
	static class Program
	{
		private static readonly Properties.Settings settings = Properties.Settings.Default;

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			if (ProcessSettings())
			{
				Lang = settings.lang;
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Lang == "ru" ? "ru" : "en-US");

				Application.SetHighDpiMode(HighDpiMode.SystemAware);
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}

		static bool ProcessSettings()
		{
			try
			{
				if (string.IsNullOrEmpty(settings.appVersion))
				{
					string settingsPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
					if (File.Exists($@"{Path.GetDirectoryName(settingsPath)}\restoreSettings"))
					{
						settings.Reset();
						File.Delete($@"{Path.GetDirectoryName(settingsPath)}\restoreSettings");

						Warning("Восстановлены настройки по умолчанию.\n" +
								"Restored default settings.", false);
					}
					else
						settings.Upgrade();

					settings.appVersion = Application.ProductVersion;
					settings.Save();
				}
			}
			catch (ConfigurationErrorsException exception)
			{
				string settingsPath = ((ConfigurationErrorsException)exception.InnerException).Filename;
				File.Delete(settingsPath);
				_ = File.Create($@"{Path.GetDirectoryName(settingsPath)}\restoreSettings");

				Error("Файл настроек повреждён. Откройте программу ещё раз, чтобы восстановить настройки по умолчанию.\n" +
					  "Settings file is damaged. Open the program again to restore default settings.", true);

				return false;
			}

			return true;
		}
	}
}
