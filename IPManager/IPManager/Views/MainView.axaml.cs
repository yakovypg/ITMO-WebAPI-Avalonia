using Avalonia.Controls;
using IPManager.ViewModels;

namespace IPManager.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}