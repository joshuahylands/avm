using System;
using System.Collections.Generic;
using AVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CoreAudio;

namespace AVM.ViewModels;

partial class MainWindowViewModel : ObservableObject
{
  public List<VolumeSliderViewModel> Devices;

  [ObservableProperty]
  private VolumeSliderViewModel _selectedDevice;

  public MainWindowViewModel()
  {
    var deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());

    Devices = new List<VolumeSliderViewModel>();

    foreach (var device in deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
    {
      Devices.Add(new VolumeSliderViewModel(new DeviceModel(device)));
    }

    SelectedDevice = Devices[0];
  }
}
