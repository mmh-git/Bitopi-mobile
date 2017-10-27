using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Threading;
using ApiRepository;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using BitopiApprovalSystem.DAL;

namespace BitopiApprovalSystem
{
    [Activity(Label = "BitopiActivity")]
    public class BitopiActivity : BaseActivity
    {
        //BitopiApplication bitopiApplication;
        RelativeLayout RLleft_drawer;
        private DrawerLayout mDrawerLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //bitopiApplication = (BitopiApplication)this.ApplicationContext;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuLayout);
            SupportActionBar.SetDisplayShowCustomEnabled(true);
            SupportActionBar.SetCustomView(Resource.Layout.custom_actionbar);
            FindViewById<RelativeLayout>(Resource.Id.rlApproval).Click += (s, e) =>
            {
                Intent i = new Intent(this, typeof(ApprovalActivity));
                StartActivity(i);
            };
            FindViewById<RelativeLayout>(Resource.Id.rlMyTask).Click += (s, e) =>
            {
                Intent i = new Intent(this, typeof(MyTaskMenu));
                StartActivity(i);
            };
            FindViewById<RelativeLayout>(Resource.Id.rlPA).Click += (s, e) =>
            {
                Intent i = new Intent(this, typeof(ProcessListActivity));
                StartActivity(i);
            };
            RLleft_drawer = FindViewById<RelativeLayout>(Resource.Id.RLleft_drawer);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            FindViewById<ImageButton>(Resource.Id.btnDrawermenu).Visibility = ViewStates.Visible;
            FindViewById<ImageButton>(Resource.Id.btnDrawermenu).Click += (s, e) =>
            {
                if (mDrawerLayout.IsDrawerOpen(RLleft_drawer))
                {
                    mDrawerLayout.CloseDrawer(RLleft_drawer);
                }
                else
                {
                    mDrawerLayout.OpenDrawer(RLleft_drawer);
                }
            };
            
            //base.LoadDrawerView();
        }
        protected override void OnStart()
        {
            base.OnStart();
        }
        void LoadDrawerView()
        {
            //FindViewById<TextView>(Resource.Id.tvUserName).Text = BitopiSingelton.Instance.User.EmployeeName;
            FindViewById<TextView>(Resource.Id.tvUserName).Text = bitopiApplication.User.EmployeeName;
            if (bitopiApplication.User.EmpImage.Length > 0)
            {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.InSampleSize = 4;
                Bitmap bitmap = BitmapFactory.DecodeByteArray(bitopiApplication.User.EmpImage, 0,
                    bitopiApplication.User.EmpImage.Length, options);
                FindViewById<ImageView>(Resource.Id.ivUserImg).SetImageBitmap(bitmap);
            }
            FindViewById<Button>(Resource.Id.btnLogout).Click += (s, e) =>
            {
                var progressDialog = ProgressDialog.Show(this, null, "", true);
                new System.Threading.Thread(new ThreadStart(() =>
                {
                    AccountRepository repo = new AccountRepository();

                    var resutl = repo.getUser("", "",
                     bitopiApplication.MacAddress,
                     "",
                     "",
                     "android", 2, bitopiApplication.CurrentVersion,bitopiApplication.User.UserCode).Result;

                    ISharedPreferences pref =
                    Application.Context.GetSharedPreferences("_bitopi_UserInfo", FileCreationMode.Private);
                    pref.Edit().Clear().Commit();
                    bitopiApplication.ClearData();
                    DBAccess.Database.DropAllTable();
                    RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();
                        Intent i = new Intent(this, typeof(LoginActivity));
                        i.SetFlags(ActivityFlags.ClearTask);
                        StartActivity(i);
                        Finish();
                    });
                })).Start();
            };
            FindViewById<RelativeLayout>(Resource.Id.rlmenuapproval).Click += (s, e) =>
            {
                Intent i = new Intent(this, typeof(ApprovalActivity));

                StartActivity(i);
            };
            FindViewById<RelativeLayout>(Resource.Id.rlmenumytask).Click += (s, e) =>
            {
                Intent i = new Intent(this, typeof(MyTaskMenu));

                StartActivity(i);
            };
        }
    }
}