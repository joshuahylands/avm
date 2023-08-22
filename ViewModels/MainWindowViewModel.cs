using System;
using System.Collections.ObjectModel;
using AVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CoreAudio;

namespace AVM.ViewModels;

partial class MainWindowViewModel : ObservableObject
{
  public ObservableCollection<DeviceModel> Devices;

  [ObservableProperty]
  private DeviceModel _selectedDevice;

  public MainWindowViewModel()
  {
    var deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());

    Devices = new ObservableCollection<DeviceModel>();

    foreach (var device in deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
    {
      Devices.Add(new DeviceModel(device));
    }

    SelectedDevice = Devices[0];
  }
}
