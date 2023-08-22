using System;
using System.Diagnostics;
using AVM.Interfaces;
using CoreAudio;

namespace AVM.Models;

class ApplicationModel : IMixerDescriptor
{
  private readonly AudioSessionControl2 _sessionControl;

  public ApplicationModel(AudioSessionControl2 sessionControl)
  {
    _sessionControl = sessionControl;
  }

  public string Name
  {
    get
    {
      if (_sessionControl.IsSystemSoundsSession)
      {
        return "System Sounds";
      }
      else
      {
        using var process = Process.GetProcessById((int) _sessionControl.ProcessID);
        return process.ProcessName;
      }
    }
  }

  public int Volume
  {
    get
    {
      if (_sessionControl.SimpleAudioVolume == null)
      {
        return 0;
      }
      else
      {
        return (int) Math.Round(_sessionControl.SimpleAudioVolume.MasterVolume * 100.0f);
      }
    }
    set
    {
      if (_sessionControl.SimpleAudioVolume != null)
      {
        _sessionControl.SimpleAudioVolume.MasterVolume = value / 100.0f;
      }
    }
  }

  public bool Muted
  {
    get
    {
      if (_sessionControl.SimpleAudioVolume == null)
      {
        return true;
      }
      else
      {
        return _sessionControl.SimpleAudioVolume.Mute;
      }
    }
    set
    {
      if (_sessionControl.SimpleAudioVolume != null)
      {
        _sessionControl.SimpleAudioVolume.Mute = value;
      }
    }
  }
}