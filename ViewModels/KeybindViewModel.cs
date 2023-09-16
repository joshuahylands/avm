using AVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Windows.System;

namespace AVM.ViewModels;

// Class to be used as a base class for view models that expose keybind settings
class KeybindViewModel : ObservableObject
{
  private KeybindModel KeybindModel => (KeybindModel) Application.Current.Resources["Settings"];

  public VirtualKey SelectedDeviceUp
  {
    get => KeybindModel.SelectedDeviceUp;
    set => KeybindModel.SelectedDeviceUp = value;
  }

  public VirtualKey SelectedDeviceDown
  {
    get => KeybindModel.SelectedDeviceDown;
    set => KeybindModel.SelectedDeviceDown = value;
  }

  public VirtualKey SelectedMixerUp
  {
    get => KeybindModel.SelectedMixerUp;
    set => KeybindModel.SelectedMixerUp = value;
  }

  public VirtualKey SelectedMixerDown
  {
    get => KeybindModel.SelectedMixerDown;
    set => KeybindModel.SelectedMixerDown = value;
  }

  public VirtualKey VolumeUp
  {
    get => KeybindModel.VolumeUp;
    set => KeybindModel.VolumeUp = value;
  }

  public VirtualKey VolumeDown
  {
    get => KeybindModel.VolumeDown;
    set => KeybindModel.VolumeDown = value;
  }

  public VirtualKey Mute
  {
    get => KeybindModel.Mute;
    set => KeybindModel.Mute = value;
  }
}
