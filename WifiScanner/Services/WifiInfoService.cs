using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiScanner.DataModels;

namespace WifiScanner.Services
{
    /// <summary>
    /// ToDoListService
    /// </summary>
    // Data is gathered here?
    public class WifiInfoService
    {
        private List<WifiInfoItem> WifiInfoItems = new List<WifiInfoItem>();

        //public IEnumerable<WifiInfoItem> GetItems() => WifiInfoItems;
        public IEnumerable<WifiInfoItem> GetItems()
        {
            WifiInfoItems = WifiData.GetData();


            return WifiInfoItems;
        }
    }
}
