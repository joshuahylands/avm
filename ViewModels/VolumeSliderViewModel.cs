using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using AVM.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;

namespace AVM.ViewModels;

partial class VolumeSliderViewModel : ObservableObject
{
  protected readonly IMixerDescriptor _mixerDescriptor;

  public VolumeSliderViewModel(IMixerDescriptor mixerDescriptor)
  {
    _mixerDescriptor = mixerDescriptor;
    Muted = mixerDescriptor.Muted;
  }

  public string Name
  {
    get => _mixerDescriptor.Name;
  }

  public int Volume
  {
    get => _mixerDescriptor.Volume;
    set => _mixerDescriptor.Volume = value;
  }

  public bool Muted
  {
    get => _mixerDescriptor.Muted;
    set
    {
      _mixerDescriptor.Muted = value;
      IsSliderEnabled = !value;
      ApplicationIconOpacity = value ? 0.25f : 1.0f;
    }
  }

  [ObservableProperty]
  private bool _isSliderEnabled;

  [ObservableProperty]
  private float _applicationIconOpacity;

  public BitmapImage ApplicationIcon
  {
    get
    {
      var icon = _mixerDescriptor.MixerIcon;

      using var stream = new MemoryStream();
      icon
        .ToBitmap()
        .Save(stream, ImageFormat.Png);
      stream.Position = 0;

      var image = new BitmapImage();
      image.SetSource(stream.AsRandomAccessStream());

      return image;
    }
  }

  public VolumeSliderViewModel[] Children
  {
    get
    {
      var childViewModels = new List<VolumeSliderViewModel>();

      foreach (var child in _mixerDescriptor.Children)
      {
        childViewModels.Add(new VolumeSliderViewModel(child));
      }

      return childViewModels.ToArray();
    }
  }

  public void ToggleMute() => Muted = !Muted;
}
