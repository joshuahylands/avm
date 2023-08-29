using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace AVM.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    
    // Set the backdrop. Mica is only supported on Windows 11
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
}
