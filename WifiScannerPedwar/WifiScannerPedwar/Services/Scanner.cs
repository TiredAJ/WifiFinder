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
        static List<AvWifiInfoItem> Wifis = new List<AvWifiInfoItem>();

        private event EventHandler TickCB;

        Avalonia.Threading.DispatcherTimer Amserwr;

        public WifiService()
        {
            TickCB += Tick;

            Amserwr = new Avalonia.Threading.DispatcherTimer
            (new TimeSpan(0, 0, 2), DispatcherPriority.Background, TickCB);

            Amserwr.Start();
        }

        private void Tick(object? Sender, EventArgs e)
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

        public IEnumerable<AvWifiInfoItem> GetItems() => Wifis;
    }
}
