using System;
using System.Collections.Generic;
using System.Drawing;
using AVM.Interfaces;
using CoreAudio;

namespace AVM.Models;

class DeviceModel : IMixerDescriptor
{
  private readonly MMDevice _device;

  public DeviceModel(MMDevice device)
  {
    _device = device;
  }

  public string Name => _device.DeviceFriendlyName;

  public int Volume
  {
    get
    {
      if (_device.AudioEndpointVolume == null)
      {
        return 0;
      }
      else
      {
        return (int) Math.Round(_device.AudioEndpointVolume.MasterVolumeLevelScalar * 100.0f);
      }
    }

    set
    {
      if (_device.AudioEndpointVolume != null)
      {
        _device.AudioEndpointVolume.MasterVolumeLevelScalar = value / 100.0f;
      }
    }
  }

  public bool Muted
  {
    get
    {
      if (_device.AudioEndpointVolume == null)
      {
        return true;
      }
      else
      {
        return _device.AudioEndpointVolume.Mute;
      }
    }
    set
    {
      if (_device.AudioEndpointVolume != null)
      {
        _device.AudioEndpointVolume.Mute = value;
      }
    }
  }

  public Icon MixerIcon {
    get => SystemIcons.Application;
  }

  public IMixerDescriptor[] Children
  {
    get
    {
      var applications = new List<ApplicationModel>();

      if (_device.AudioSessionManager2 != null && _device.AudioSessionManager2.Sessions != null)
      {
        foreach (var session in _device.AudioSessionManager2.Sessions)
        {
          applications.Add(new ApplicationModel(session));
        }
      }

      return applications.ToArray();
    }
  }
}
