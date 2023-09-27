using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WifiScannerLib;
using WifiScannerPedwar.Models;

namespace WifiScannerPedwar.Services
{
    public class WifiService
    {
        public IEnumerable<AvWifiInfoItem> GetItems() { return new List<AvWifiInfoItem>(); }

    }
}
