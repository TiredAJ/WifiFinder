using Avalonia.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WifiScanner.Services;

namespace WifiScanner.ViewModels
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// ToDoList
        /// </summary>
        public WifiInfoViewModel WifiInfoList { get; private set; }
        private DispatcherTimer tmr_Updater = new DispatcherTimer();

        public MainViewModel()
        {
            UpdateInfoList();

            tmr_Updater.Interval = new System.TimeSpan(0, 0, 10);
            tmr_Updater.Tick += Tmr_Updater_Tick;
            tmr_Updater.Start();
        }

        private void Tmr_Updater_Tick(object? sender, System.EventArgs e)
        {
            UpdateInfoList();
            Debug.WriteLine("Ticked");
        }

        private void UpdateInfoList()
        {
            var Service = new WifiInfoService();
            WifiInfoList = new WifiInfoViewModel(Service.GetItems());
        }
    }
}
