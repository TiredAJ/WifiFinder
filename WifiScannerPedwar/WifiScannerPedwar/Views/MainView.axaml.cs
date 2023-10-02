using Avalonia.Controls;
using Avalonia.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        //sets the default sorting (Rssi, best > worst signal)
        Dispatcher.UIThread.InvokeAsync(() =>
        this.FindControl<DataGrid>("DGRD_APData").Columns[2].Sort());
    }
}
