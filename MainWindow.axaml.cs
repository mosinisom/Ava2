using Avalonia.Controls;

namespace Ava2;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        DataContext = new MyModel();
        InitializeComponent();
    }
}