using AVM.Helpers;
using AVM.ViewModels;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using WinRT;

namespace AVM.Views;

partial class MainWindow : Window
{
  private SystemBackdropConfiguration? _backdropConfig;
  private MicaController? _micaController;
  private DesktopAcrylicController? _acrylicController;

  // The ViewModel for the Window. WinUI3's Window doesn't inherit from FrameworkElement therefore doesn't have a DataContext to store this
  public MainWindowViewModel VM = new();

  public MainWindow()
  {
    InitializeComponent();
    SetBackdrop();
  }

  void SetBackdrop()
  {
    if (!WindowsSystemDispatcherQueueHelper.EnsureWindowsSystemDispatcherQueueController())
    {
      return;
    }

    Activated += Window_Activated;
    Closed += Window_Closed;
    ((FrameworkElement) Content).ActualThemeChanged += Window_ThemeChanged;

    _backdropConfig = new SystemBackdropConfiguration
    {
      IsInputActive = true
    };

    SetConfigurationSourceTheme();

    if (MicaController.IsSupported())
    {
      _micaController = new MicaController();
      _micaController.AddSystemBackdropTarget(this.As<ICompositionSupportsSystemBackdrop>());
      _micaController.SetSystemBackdropConfiguration(_backdropConfig);
    }
    else if (DesktopAcrylicController.IsSupported())
    {
      _acrylicController = new DesktopAcrylicController();
      _acrylicController.AddSystemBackdropTarget(this.As<ICompositionSupportsSystemBackdrop>());
      _acrylicController.SetSystemBackdropConfiguration(_backdropConfig);
    }
  }

  private void SetConfigurationSourceTheme()
  {
    if (_backdropConfig == null) return;

    switch (((FrameworkElement) Content).ActualTheme)
    {
      case ElementTheme.Dark:     _backdropConfig.Theme = SystemBackdropTheme.Dark; break;
      case ElementTheme.Light:    _backdropConfig.Theme = SystemBackdropTheme.Light; break;
      case ElementTheme.Default:  _backdropConfig.Theme = SystemBackdropTheme.Default; break;
    }
  }

  private void Window_Activated(object sender, WindowActivatedEventArgs args)
  {
    if (_backdropConfig != null)
      _backdropConfig.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
  }

  private void Window_Closed(object sender, WindowEventArgs args)
  {
    if (_micaController != null)
    {
      _micaController.Dispose();
      _micaController = null;
    }
    else if (_acrylicController != null)
    {
      _acrylicController.Dispose();
      _acrylicController = null;
    }

    Activated -= Window_Activated;
    _backdropConfig = null;
  }

  private void Window_ThemeChanged(FrameworkElement sender, object args)
  {
    if (_backdropConfig != null)
      SetConfigurationSourceTheme();
  }
}
