using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AVM.Views;

partial class TrayIcon : UserControl
{
  public TrayIcon()
  {
    InitializeComponent();
  }

  public void Exit(object sender, RoutedEventArgs e)
  {
    App.MainWindow.Close();
    App.SettingsWindow.Close();
    TrayIconControl.Dispose();
    Application.Current.Exit();
  }

  public void ShowMainWindow(object sender, RoutedEventArgs e)
  {
    App.MainWindow.Activate();
  }
}
