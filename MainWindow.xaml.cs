using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using WinRT;

namespace AVM;

partial class MainWindow : Window
{
  SystemBackdropConfiguration? _backdropConfig;
  MicaController? _micaController;
  DesktopAcrylicController? _acrylicController;

  public MainWindow()
  {
    InitializeComponent();
    SetBackdrop();
  }

  void SetBackdrop()
  {
    WindowsSystemDispatcherQueueHelper.EnsureWindowsSystemDispatcherQueueController();

    this.Activated += Window_Activated;
    this.Closed += Window_Closed;
    ((FrameworkElement) this.Content).ActualThemeChanged += Window_ThemeChanged;

    _backdropConfig = new SystemBackdropConfiguration();
    _backdropConfig.IsInputActive = true;

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

    switch (((FrameworkElement) this.Content).ActualTheme)
    {
      case ElementTheme.Dark:     _backdropConfig.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
      case ElementTheme.Light:    _backdropConfig.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
      case ElementTheme.Default:  _backdropConfig.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
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

    this.Activated -= Window_Activated;
    _backdropConfig = null;
  }

  private void Window_ThemeChanged(FrameworkElement sender, object args)
  {
    if (_backdropConfig != null)
      SetConfigurationSourceTheme();
  }
}
