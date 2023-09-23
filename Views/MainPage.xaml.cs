using System;
using AVM.ViewModels;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace AVM.Views;

partial class MainPage : Page
{
  /*
    - {Binding Devices} to the DataContext doesn't work with ComboBox.ItemsSource
    - {x:Bind DataContext.Devices} doesn't work either
    - Solution: Declare a VM (ViewModel) Property to access the DataContext through {x:Bind VM.Devices}
  */
  public MainPageViewModel VM
  {
    get => (MainPageViewModel) DataContext;
  }

  public MainPage()
  {
    InitializeComponent();
  }
  
  private void HideWindow(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    App.MainWindow.AppWindow.Hide();
    args.Handled = true;
  }

  private void IncrementSelectedDeviceIndex(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;

    if (dataContext.SelectedDeviceIndex == dataContext.Devices.Count - 1)
    {
      dataContext.SelectedDeviceIndex = 0;
    }
    else
    {
      dataContext.SelectedDeviceIndex++;
    }

    args.Handled = true;
  }

  private void DecrementSelectedDeviceIndex(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;

    if (dataContext.SelectedDeviceIndex == 0)
    {
      dataContext.SelectedDeviceIndex = dataContext.Devices.Count - 1;
    }
    else
    {
      dataContext.SelectedDeviceIndex--;
    }

    args.Handled = true;
  }

  private void IncrementSelectedMixerIndex(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;

    if (dataContext.SelectedMixerIndex == dataContext.Mixers.Count - 1)
    {
      dataContext.SelectedMixerIndex = 0;
    }
    else
    {
      dataContext.SelectedMixerIndex++;;
    }

    args.Handled = true;
  }

  private void DecrementSelectedMixerIndex(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;

    if (dataContext.SelectedMixerIndex == 0)
    {
      dataContext.SelectedMixerIndex = dataContext.Mixers.Count - 1;
    }
    else
    {
      dataContext.SelectedMixerIndex--;
    }

    args.Handled = true;
  }

  private void IncreaseVolume(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;
    dataContext.Mixers[dataContext.SelectedMixerIndex].Volume = Math.Clamp(dataContext.Mixers[dataContext.SelectedMixerIndex].Volume + 1, 0, 100);
    args.Handled = true;
  }

  private void DecreaseVolume(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;
    dataContext.Mixers[dataContext.SelectedMixerIndex].Volume = Math.Clamp(dataContext.Mixers[dataContext.SelectedMixerIndex].Volume - 1, 0, 100);
    args.Handled = true;
  }

  private void ToggleMute(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
  {
    var dataContext = (MainPageViewModel) DataContext;
    dataContext.Mixers[dataContext.SelectedMixerIndex].ToggleMute();
    args.Handled = true;
  }

  private void StackPanel_Loaded(object sender, RoutedEventArgs e)
  {
    var panel = (StackPanel) sender;

    var displayRect = DisplayArea.Primary.WorkArea;

    // Move to bottom right of the screen
    App.MainWindow.AppWindow.MoveAndResize(new(
      displayRect.Width - (int) panel.ActualWidth,    // X
      displayRect.Height - (int) panel.ActualHeight,  // Y
      (int) panel.ActualWidth,                        // Width
      (int) panel.ActualHeight                        // Height
    ));
  }  
}
