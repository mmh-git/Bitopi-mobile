﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BitopiApprovalSystem
{
    [Activity(Label = "Bitopi on the go")]
    public class DownloadNewVersionActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            BitopiSingelton.Instance.DownloadFile(this);


        }
    }
}