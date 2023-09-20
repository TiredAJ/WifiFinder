using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiScanner.DataModels;
using Android.App;
using Android.Content;
using System.Threading;

namespace WifiScanner.Services
{
    /// <summary>
    /// ToDoListService
    /// </summary>
    // Data is gathered here?
    [Activity]
    public class WifiInfoService
    {
        private Context? CTX = null;

        public WifiInfoService()
        {this.CTX = Android.App.Application.Context;}

        private List<WifiInfoItem> WifiInfoItems = new List<WifiInfoItem>();

        //public IEnumerable<WifiInfoItem> GetItems() => WifiInfoItems;
        public IEnumerable<WifiInfoItem> GetItems()
        {
            //WifiDataService.CreateIntent(new WifiDataService(), "START_SCAN");

            WifiDataService WDS = new WifiDataService();

            //WDS.OnCreate(new Android.OS.Bundle(), new Android.OS.PersistableBundle());
            WDS.OnCreate();

            do
            {
                Thread.Sleep(1000);

                WDS.Scan();
            } while (true);

            return WifiInfoItems;
        }
    }
}
