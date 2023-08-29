using AVM.ViewModels;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace AVM.Views;

partial class MainWindow : Window
{
  // The ViewModel for the Window. WinUI3's Window doesn't inherit from FrameworkElement therefore doesn't have a DataContext to store this
  public MainWindowViewModel VM = new();

  public MainWindow()
  {
    InitializeComponent();
    
    if (MicaController.IsSupported())
    {
      SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
    }
    else
    {
      SystemBackdrop = new DesktopAcrylicBackdrop();
    }

    // Configure AppWindow Settings
    AppWindow.IsShownInSwitchers = false;
    AppWindow.Move(new(50, 60));

    var presenter = OverlappedPresenter.Create();
    presenter.IsAlwaysOnTop = true;
    presenter.IsResizable = false;
    presenter.SetBorderAndTitleBar(false, false);
    AppWindow.SetPresenter(presenter);
  }

  private void StackPanel_Loaded(object sender, RoutedEventArgs e)
  {
    var panel = (StackPanel) sender;

    AppWindow.Resize(new((int) panel.ActualWidth, (int) panel.ActualHeight));
  }
}
