using AVM.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

  private void StackPanel_Loaded(object sender, RoutedEventArgs e)
  {
    var panel = (StackPanel) sender;

    App.MainWindow.AppWindow.Resize(new((int) panel.ActualWidth, (int) panel.ActualHeight));
  }  
}
