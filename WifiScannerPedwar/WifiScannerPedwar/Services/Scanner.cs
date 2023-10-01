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
    //effectively a wrapper?
    public class WifiService
    {
        public IWS? WScanner;

        public WifiService(IWS? _WScanner)
        {WScanner = _WScanner;}

        public IEnumerable<WifiInfoItem> GetItems()
        {
            if (WScanner != null)
            {return WScanner.GetData();}
            else
            {return Enumerable.Empty<WifiInfoItem>();}
        }        
    }
}
