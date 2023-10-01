using Avalonia.Remote.Protocol.Designer;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SDD = System.Diagnostics.Debug;

using WifiScannerLib;
using WifiScannerPedwar.Models;
using Avalonia.Threading;

namespace WifiScannerPedwar.Services
{
    public class WifiService
    {
        public IWS? WScanner;

        static List<WifiInfoItem> Wifis = new List<WifiInfoItem>();

        public WifiService(IWS? _WScanner)
        {WScanner = _WScanner;}

        public IEnumerable<WifiInfoItem> GetItems()
        {
            //Wifis.Add
            //(new WifiInfoItem()
            //{
            //    BSSID = new Random((int)DateTime.Now.Ticks).Next(10000, 50000).ToString(),
            //    SSID = "Ur mom",
            //    RSSI = new Random((int)DateTime.Now.Ticks).Next(-40, -30).ToString(),
            //    LastUpdated = new TimeSpan(0, 0, 2),
            //    Capabilities = "WiFi 6e"
            //});

            return WScanner.GetData();
        }        
    }
}
