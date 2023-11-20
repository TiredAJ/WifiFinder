using Avalonia.Controls;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

    }

    private void Snapshot_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SDD.WriteLine("Snapshot taken!");
    }
}
