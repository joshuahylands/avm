namespace AVM.Interfaces;

// Properties that a VolumeSlider Control expects to be present on an object
interface IMixerDescriptor
{
  string Name { get; }
  int Volume { get; set; }
  bool Muted { get; set; }
}
