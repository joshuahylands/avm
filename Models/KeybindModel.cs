using Windows.System;

namespace AVM.Models;

// Defines the keybind settings of the application
partial class KeybindModel
{
  public VirtualKey SelectedDeviceUp = VirtualKey.PageUp;
  public VirtualKey SelectedDeviceDown = VirtualKey.PageDown;

  public VirtualKey SelectedMixerUp = VirtualKey.Left;
  public VirtualKey SelectedMixerDown = VirtualKey.Right;

  public VirtualKey VolumeUp = VirtualKey.Up;
  public VirtualKey VolumeDown = VirtualKey.Down;

  public VirtualKey Mute = VirtualKey.M;
}
