using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;

namespace AVM.Views;

partial class KeybindInput : UserControl
{
  public static DependencyProperty HeaderProperty = DependencyProperty.Register(
    nameof(Header),
    typeof(string),
    typeof(KeybindInput),
    new PropertyMetadata("")
  );
  public string Header
  {
    get => (string) GetValue(HeaderProperty);
    set => SetValue(HeaderProperty, value);
  }

  public static DependencyProperty VirtualKeyProperty = DependencyProperty.Register(
    nameof(VirtualKey),
    typeof(VirtualKey),
    typeof(KeybindInput),
    new PropertyMetadata(null)
  );
  public VirtualKey VirtualKey
  {
    get => (VirtualKey) GetValue(VirtualKeyProperty);
    set => SetValue(VirtualKeyProperty, value);
  }

  public KeybindInput()
  {
    InitializeComponent();
  }

  private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
  {
    VirtualKey = e.Key;
  }
}
