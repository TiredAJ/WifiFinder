﻿using Avalonia.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WifiScannerLib;
using WifiScannerTri.Services;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerTri.ViewModels;

public class MainViewModel : ViewModelBase
{
    public WifiInfoViewModel WifiInfoList { get; private set; }    
    //public static IWS? IWScanner;

    private DispatcherTimer DT_Timer = new DispatcherTimer();
    private WifiInfoService Service;

    public MainViewModel()
    {
        SDD.WriteLine("**** Called empty constructor ****");
    }

    public MainViewModel(IWS? _IWScanner)
    {
        SDD.WriteLine("**** Called from main constructor ****");

        if (_IWScanner != null)
        {
            Service = new WifiInfoService(_IWScanner);

            //UpdateInfoList();

            DT_Timer.Interval = new System.TimeSpan(0, 0, 10);
            DT_Timer.Tick += Tmr_Updater_Tick;
            //DT_Timer.Start();
        }
        //else
        //{throw new System.Exception("IWScanner was null!");}
    }

    private void Tmr_Updater_Tick(object? sender, System.EventArgs e)
    {
        //UpdateInfoList();
        Debug.WriteLine("Ticked");
    }

    private void UpdateInfoList()
    {
        if (Service == null)
        { throw new System.Exception("Service was null!"); }

        WifiInfoList = new WifiInfoViewModel(Service.GetItems());
    }
}