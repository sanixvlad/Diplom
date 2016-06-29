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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Diplom
{
    [Activity(Label = "Расписание", Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class user : ActionBarActivity
    {

        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        string FIO;
        int ID_gruppi;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.user);
            // Create your application here

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

            SetSupportActionBar(mToolbar);
            var trans = SupportFragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            trans.Add(Resource.Id.sample_content_fragment, fragment);
            trans.Commit();



            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Расписание");
            //mLeftDataSet.Add("Преподаватели");
            mLeftDataSet.Add("Напоминание");
           // mLeftDataSet.Add("Местоположение");
            mLeftDataSet.Add("Выход");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;
            mDrawerToggle = new MyActionBarDrawerToggle(
                this,            //host activity
                mDrawerLayout,  //DrawerLayout
                Resource.String.raspisanie, //открыто сообщение
                Resource.String.raspisanie //зактрыто сообщение
                );

            mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();
            //////////////
            FIO = Intent.GetStringExtra("fio");
            ID_gruppi = Intent.GetIntExtra("id_gruppi", 1);
            student1 ts = new student1();
            ts.ID_GRUPPI(ID_gruppi);
            if (student1.flag == 0)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetMessage("Здравствуйте " + FIO);
                Dialog dialog = alert.Create();
                dialog.Show();
                student1.flag++;
            }
        }

        void mLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 1:
                    var intent = new Intent(this, typeof(Napominanie));
                    StartActivity(intent);
                    break;
                case 2: var intent1 = new Intent(this, typeof(MainActivity));
                    StartActivity(intent1);
                    break;
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            int s = -1;
            switch (item.ItemId)
            {
                case Resource.Id.add:
                    var intent = new Intent(this, typeof(Add_Napominanie1));
                    intent.PutExtra("id", s);
                    this.StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


    }
}