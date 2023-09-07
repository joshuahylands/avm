using AVM.Views;
using H.NotifyIcon;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace AVM;

partial class App : Application
{
  public readonly static MainWindow MainWindow = new();

  private TaskbarIcon? _trayIcon;

  public App()
  {
    InitializeComponent();
  }

  // Setup the tray icon when the app launches
  protected override void OnLaunched(LaunchActivatedEventArgs e)
  {
    ((XamlUICommand) Resources["ShowMainWindow"]).ExecuteRequested += ShowMainWindow;
    ((XamlUICommand) Resources["ExitApplication"]).ExecuteRequested += ExitApplication;

    _trayIcon = (TaskbarIcon) Resources["TrayIcon"];
    _trayIcon.ForceCreate();
  }

  private void ShowMainWindow(object sender, ExecuteRequestedEventArgs args) => MainWindow.Activate();

  private void ExitApplication(object sender, ExecuteRequestedEventArgs args)
  {
    MainWindow.Close();

    _trayIcon?.Dispose();

    Exit();
  }
}
