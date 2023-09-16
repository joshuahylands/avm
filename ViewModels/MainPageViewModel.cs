using System;
using System.Collections.Generic;
using AVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CoreAudio;

namespace AVM.ViewModels;

partial class MainPageViewModel : KeybindViewModel
{
  public List<DeviceModel> Devices;

  private int _selectedDeviceIndex;
  public int SelectedDeviceIndex
  {
    get => _selectedDeviceIndex;
    set
    {
      SetProperty(ref _selectedDeviceIndex, value);

      InitializeMixers();

      SelectedMixerIndex = 0;
    }
  }

  [ObservableProperty]
  private List<VolumeSliderViewModel> _mixers = new();

  [ObservableProperty]
  private int _selectedMixerIndex = 0;

  public MainPageViewModel()
  {
    var deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());

    Devices = new List<DeviceModel>();

    foreach (var device in deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
    {
      Devices.Add(new DeviceModel(device));
    }

    SelectedDeviceIndex = 0;
  }

  private void InitializeMixers()
  {
    var selectedDevice = Devices[SelectedDeviceIndex];
    var volumeSliderViewModels = new List<VolumeSliderViewModel>
    {
      new VolumeSliderViewModel(selectedDevice)
    };

    foreach (var mixerDescriptor in selectedDevice.Children)
    {
      volumeSliderViewModels.Add(new VolumeSliderViewModel(mixerDescriptor));
    }

    Mixers = volumeSliderViewModels;
  }
}
