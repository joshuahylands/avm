using AVM.Views;
using Microsoft.UI.Xaml;

namespace AVM;

partial class App : Application
{
  public readonly static MainWindow MainWindow = new();

  public App()
  {
    InitializeComponent();
  }

  protected override void OnLaunched(LaunchActivatedEventArgs e)
  {
    MainWindow.Activate();
  }
}
