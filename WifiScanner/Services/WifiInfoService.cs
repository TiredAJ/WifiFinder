using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiScanner.DataModels;
#if ANDROID
using Android.App;
using Android.Content;
#endif
using System.Threading;

namespace WifiScanner.Services
{
#if ANDROID
    /// <summary>
    /// ToDoListService
    /// </summary>
    // Data is gathered here?
    [Activity]
    public class WifiInfoService
    {
        private Context? CTX = null;
        private WifiDataService WDS;

        public WifiInfoService()
        {
            this.CTX = Android.App.Application.Context;
            
            WDS = new WifiDataService();

            WDS.OnCreate();
        }

        private List<WifiInfoItem> WifiInfoItems = new List<WifiInfoItem>();

        public IEnumerable<WifiInfoItem> GetItems()
        {return WDS.GetData();}
    }
#endif
}
