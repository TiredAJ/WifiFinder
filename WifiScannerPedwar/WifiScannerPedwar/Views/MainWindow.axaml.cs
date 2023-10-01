using SDD = System.Diagnostics.Debug;
using Avalonia.Controls;

namespace WifiScannerPedwar.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        SDD.WriteLine($"*** Width: {DGRD_APData.Width} ***");
    }
}
