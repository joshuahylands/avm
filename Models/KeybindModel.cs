using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Windows.System;

namespace AVM.Models;

// Defines properties to access settings of the application along with the saving and loading the settings from disk
partial class KeybindModel
{
  // Converts a VirtualKey to and from a JSON number type
  private class VirtualKeyJsonConverter : JsonConverter<VirtualKey>
  {
    public override VirtualKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return (VirtualKey) reader.GetInt32();
    }

    public override void Write(Utf8JsonWriter writer, VirtualKey value, JsonSerializerOptions options)
    {
      writer.WriteNumberValue((int) value);
    }
  }

  // Defines the keybind settings of the application
  private struct KeybindSettings
  {
    public VirtualKey SelectedDeviceUp { get; set; }
    public VirtualKey SelectedDeviceDown { get; set; }

    public VirtualKey SelectedMixerUp { get; set; }
    public VirtualKey SelectedMixerDown { get; set; }

    public VirtualKey VolumeUp { get; set; }
    public VirtualKey VolumeDown { get; set; }

    public VirtualKey Mute { get; set; }
  }

  private static string SettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".avm");
  private static JsonSerializerOptions JsonSerializerOptions => new()
  {
    Converters = {
      new VirtualKeyJsonConverter()
   },
   WriteIndented = true
  };

  // KeybindSettings with the default settings if no settings are defined
  private KeybindSettings _settings = new()
  {
    SelectedDeviceUp = VirtualKey.PageUp,
    SelectedDeviceDown = VirtualKey.PageDown,

    SelectedMixerUp = VirtualKey.Left,
    SelectedMixerDown = VirtualKey.Right,

    VolumeUp = VirtualKey.Up,
    VolumeDown = VirtualKey.Down,

    Mute = VirtualKey.M
  };

  // Reads the settings file and creates one with the default settings if it doesn't exist
  public KeybindModel()
  {
    try
    {
      var settingsBytes = File.ReadAllBytes(SettingsPath);

      _settings = JsonSerializer.Deserialize<KeybindSettings>(settingsBytes, JsonSerializerOptions);
    }
    finally
    {
      Save();
    }
  }

  // Serialize the settings and save them to the settings file
  private void Save()
  {
    using var file = new StreamWriter(SettingsPath);

    var serializedSettings = JsonSerializer.Serialize(_settings, JsonSerializerOptions);

    file.Write(serializedSettings);
  }

  // Properties to get and set the the settings. When the set method is used it saves the setting to disk
  public VirtualKey SelectedDeviceUp
  {
    get => _settings.SelectedDeviceUp;
    set
    {
      _settings.SelectedDeviceUp = value;
      Save();
    }
  }
  public VirtualKey SelectedDeviceDown
  {
    get => _settings.SelectedDeviceDown;
    set
    {
      _settings.SelectedDeviceDown = value;
      Save();
    }
  }

  public VirtualKey SelectedMixerUp
  {
    get => _settings.SelectedMixerUp;
    set
    {
      _settings.SelectedMixerUp = value;
      Save();
    }
  }
  public VirtualKey SelectedMixerDown
  {
    get => _settings.SelectedMixerDown;
    set
    {
      _settings.SelectedMixerDown = value;
      Save();
    }
  }

  public VirtualKey VolumeUp
  {
    get => _settings.VolumeUp;
    set
    {
      _settings.VolumeUp = value;
      Save();
    }
  }
  public VirtualKey VolumeDown
  {
    get => _settings.VolumeDown;
    set
    {
      _settings.VolumeDown = value;
      Save();
    }
  }

  public VirtualKey Mute
  {
    get => _settings.Mute;
    set
    {
      _settings.Mute = value;
      Save();
    }
  }
}
