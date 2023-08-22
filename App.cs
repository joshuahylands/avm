using AVM.Views;
using Microsoft.UI.Xaml;

namespace AVM;

partial class App : Application
{
  private MainWindow? _window;

  public App()
  {
    InitializeComponent();
  }

  protected override void OnLaunched(LaunchActivatedEventArgs e)
  {
    _window = new MainWindow();
    _window.Activate();
  }
}
