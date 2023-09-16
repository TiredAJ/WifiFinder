using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiScanner.DataModels;

namespace WifiScanner.ViewModels
{
    /// <summary>
    /// ToDoListViewModel
    /// </summary>
    public class WifiInfoViewModel : ViewModelBase
    {
        /// <summary>
        /// ToDoListViewModel()
        /// </summary>
        /// <param name="wifiItems"></param>
        public WifiInfoViewModel(IEnumerable<WifiInfoItem> _WifiItems)
        {WifiItems = new ObservableCollection<WifiInfoItem>(_WifiItems);}

        /// <summary>
        /// ListItems
        /// </summary>
        public ObservableCollection<WifiInfoItem> WifiItems { get;}
    }
}
