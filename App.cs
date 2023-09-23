using System;
using System.Runtime.InteropServices;
using AVM.Views;
using H.NotifyIcon;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using WinRT.Interop;

namespace AVM;

partial class App : Application
{
  public readonly static MainWindow MainWindow = new();

  private TaskbarIcon? _trayIcon;
  private SettingsWindow? _settingsWindow;

  [LibraryImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static partial bool SetForegroundWindow(IntPtr hWnd);

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

    // Start the window hidden
    MainWindow.Activate();
    MainWindow.Hide();
  }

  private void ShowMainWindow(object sender, ExecuteRequestedEventArgs args)
  {
    MainWindow.Show();

    // Set focus to the window. WinUI 3 doesn't focus when calling Show on a Window
    SetForegroundWindow(WindowNative.GetWindowHandle(MainWindow));
  }

  private void ShowSettingsWindow(object sender, ExecuteRequestedEventArgs args)
  {
    _settingsWindow = new SettingsWindow();
    _settingsWindow.Activate();
  }

  private void ExitApplication(object sender, ExecuteRequestedEventArgs args)
  {
    MainWindow.Close();
    _settingsWindow?.Close();

    _trayIcon?.Dispose();

    Exit();
  }
}
