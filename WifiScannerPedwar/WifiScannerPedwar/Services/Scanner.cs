using System.Collections.Generic;
using System.Linq;

using WifiScannerLib;

namespace WifiScannerPedwar.Services
{
    //effectively a wrapper?
    public class WifiService
    {
        public IWS? WScanner;

        public WifiService(IWS? _WScanner)
        { WScanner = _WScanner; }

        public IEnumerable<WifiInfoItem> GetItems()
        {
            if (WScanner != null)
            { return WScanner.GetData(); }
            else
            { return Enumerable.Empty<WifiInfoItem>(); }
        }
    }
}
