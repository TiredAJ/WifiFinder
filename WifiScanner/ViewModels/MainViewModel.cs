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
#if ANDROID
        private WifiInfoService Service;

        public MainViewModel()
        {
            UpdateInfoList();
            Service = new WifiInfoService();

            tmr_Updater.Interval = new System.TimeSpan(0, 0, 10);
            tmr_Updater.Tick += Tmr_Updater_Tick;
            //tmr_Updater.Start();
        }

        private void Tmr_Updater_Tick(object? sender, System.EventArgs e)
        {
            UpdateInfoList();
            Debug.WriteLine("Ticked");
        }

        private void UpdateInfoList()
        {
            if (Service == null)
            {Service = new WifiInfoService();}

            WifiInfoList = new WifiInfoViewModel(Service.GetItems());
        }
#endif
    }
}
