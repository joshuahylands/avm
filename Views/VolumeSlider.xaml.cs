using AVM.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace AVM.Views;

partial class VolumeSlider : UserControl
{
  public VolumeSlider()
  {
    InitializeComponent();
  }

  private void Image_PointerPressed(object sender, PointerRoutedEventArgs e) => ((VolumeSliderViewModel) DataContext).ToggleMute();
}
