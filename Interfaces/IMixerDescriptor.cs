using System.Drawing;

namespace AVM.Interfaces;

// Properties that a mixer has
interface IMixerDescriptor
{
  string Name { get; }
  int Volume { get; set; }
  bool Muted { get; set; }
  Icon MixerIcon { get; }
  IMixerDescriptor[] Children { get; }
}
