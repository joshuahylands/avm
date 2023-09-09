using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace AVM.Views;

public partial class SettingsWindow : Window
{
  public SettingsWindow()
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

    ExtendsContentIntoTitleBar = true;
    SetTitleBar(TitleBar);
  }
}
