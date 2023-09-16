using AVM.Views;
using H.NotifyIcon;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace AVM;

partial class App : Application
{
  public static MainWindow? MainWindow { get; private set; }

  private TaskbarIcon? _trayIcon;
  private SettingsWindow? _settingsWindow;

  public App()
  {
    InitializeComponent();
  }

  // Setup the tray icon when the app launches
  protected override void OnLaunched(LaunchActivatedEventArgs e)
  {
    ((XamlUICommand) Resources["ShowMainWindow"]).ExecuteRequested += ShowMainWindow;
    ((XamlUICommand) Resources["ShowSettingsWindow"]).ExecuteRequested += ShowSettingsWindow;
    ((XamlUICommand) Resources["ExitApplication"]).ExecuteRequested += ExitApplication;

    _trayIcon = (TaskbarIcon) Resources["TrayIcon"];
    _trayIcon.ForceCreate();
  }

  private void ShowMainWindow(object sender, ExecuteRequestedEventArgs args)
  {
    MainWindow = new();
    MainWindow?.Activate();
  }

  private void ShowSettingsWindow(object sender, ExecuteRequestedEventArgs args)
  {
    _settingsWindow = new SettingsWindow();
    _settingsWindow.Activate();
  }

  private void ExitApplication(object sender, ExecuteRequestedEventArgs args)
  {
    MainWindow?.Close();
    _settingsWindow?.Close();

    _trayIcon?.Dispose();

    Exit();
  }
}
