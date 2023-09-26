using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WifiScannerLib;

namespace WifiScannerTri.Services
{
    public class WifiInfoService
    {
        public IWS? WScanner;
        //public DispatcherTimer DT = new DispatcherTimer();

        public WifiInfoService(IWS? _WS)
        {
            WScanner = _WS;

            //DT.Tick += DT_Tick;
            //DT.Interval = new TimeSpan(0, 0, 10);
        }

        //public void StartTimer()
        //{DT.Start();}

        //public void StopTimer() 
        //{ DT.Stop();}

        private void DT_Tick(object? sender, EventArgs e)
        {
            if (WScanner != null)
            {_WifiInfoItems = WScanner.GetData();}
            else
            {
                //DT.Stop();
                throw new Exception("WScanner was null!");
            }
        }

        private List<WifiInfoItem> _WifiInfoItems = new List<WifiInfoItem>();

        public List<WifiInfoItem> GetItems() => WScanner.GetData();
    }    
}
