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

namespace WifiScannerPedwar.Services
{
    public class WifiService
    {
        static List<AvWifiInfoItem> Wifis = new List<AvWifiInfoItem>();
        
        private static AutoResetEvent Resetter = new AutoResetEvent(false);

        System.Threading.Timer Amserwr = new Timer(Tick, Resetter, 0, 1000);

        public static void Start()
        { 
        
        }

        public IEnumerable<AvWifiInfoItem> GetItems() => Wifis;

        private static void Tick(object? state)
        {
            Wifis.Add
            (new AvWifiInfoItem()
            {
                BSSID = "bc23ba3a2343",
                SSID = "Ur mom",
                RSSI = new Random((int)DateTime.Now.Ticks).Next(-40, -30),
                LastUpdated = new TimeSpan(0, 0, 2),
                Capabilities = "WiFi 6e"
            });

            SDD.WriteLine($"***** Ticked! {Wifis.Count()} *****");
        }
    }
}
