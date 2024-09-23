namespace LandscapeSprinklerDesigner;

internal static class Program
{
  /// <summary>
  ///  The main entry point for the application.
  /// </summary>
  [STAThread]
  static void Main(string[] args)
  {
    // To customize application configuration such as set high DPI settings or default font,
    // see https://aka.ms/applicationconfiguration.
    ApplicationConfiguration.Initialize();

    // This is added to make sure the current path is the same as the application's 
    // executable path. The case where it might be different is when the application
    // is executed by double clicking on an lsd file.
    Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));

    Main frmMain = new Main();

    Global.CheckForUpdate(frmMain);

#if !DISABLE_SPLASH

    frmMain.SplashForm = new Splash();
    frmMain.SplashForm.ShowAsSplash();
    frmMain.SplashForm.TopMost = true;

#endif

    if (args.Length > 0)
    {
      // Load layout.
      frmMain.LoadLayout(args[0]);
    }

    Application.Run(frmMain);
  }
}