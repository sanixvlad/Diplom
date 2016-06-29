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



namespace Diplom
{
    [Activity(Label = "Напоминание", Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class Napominanie : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        List<TableItem> tableItems = new List<TableItem>();
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Napominanie);
            // Create your application here

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolBar1);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.Drawer_layout1);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer1);
            listView = FindViewById<ListView>(Resource.Id.listView1);
            SetSupportActionBar(mToolbar);

            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("Расписание");
           // mLeftDataSet.Add("Преподаватели");
            mLeftDataSet.Add("Напоминание");
            //mLeftDataSet.Add("Местоположение");
            mLeftDataSet.Add("Выход");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;
            mDrawerToggle = new MyActionBarDrawerToggle(
                this,            //host activity
                mDrawerLayout,  //DrawerLayout
                Resource.String.naponinanie, //открыто сообщение
                Resource.String.naponinanie //зактрыто сообщение
                );
            mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;

            Create_Database dbr = new Create_Database();
            var res = dbr.GetAllRecords();
            if (res != "")
            {
                string[] str = res.Split('\n');
                for (int i = 0; i < str.Length - 1; i++)
                {
                    string str2 = "";
                    string[] str1 = str[i].Split(' ');
                    for (int j = 3; j < str1.Length; j++)
                        str2 += str1[j] + " ";
                    tableItems.Add(new TableItem() { Heading = str1[1] + " " + str1[2], SubHeading = str2 });
                }
                listView.Adapter = new HomeScreenAdapter(this, tableItems);
            }

            listView.ItemClick +=listView_ItemClick;
            listView.ItemLongClick +=listView_ItemLongClick;


            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();
        }
        void mLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 0: var intent = new Intent(this, typeof(user));
                    StartActivity(intent);
                    break;
                case 2:
                    var intent1 = new Intent(this, typeof(MainActivity));
                    StartActivity(intent1);
                    break;
            }
        }

        void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent n = new Intent(this, typeof(Add_Napominanie1));
            n.PutExtra("id", e.Position);
            this.StartActivity(n);
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

        void listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            //alert.SetTitle("Confirm delete");
            alert.SetMessage("Удалить напоминание?");
            alert.SetPositiveButton("Удалить", (senderAlert, args) =>
            {
                int id = 0;
                Create_Database dbr = new Create_Database();
                var res = dbr.GetAllRecords();
                if (res != "")
                {
                    string[] str = res.Split('\n');
                    string[] str1 = str[e.Position].Split(' ');
                    id = Convert.ToInt32(str1[0]);
                }
                var res1 = dbr.RemoveTask(id);
                Toast.MakeText(this, res1, ToastLength.Short).Show();
                var res2 = dbr.GetAllRecords();
                tableItems.Clear();
                if (res2 != "")
                {
                    string[] str = res2.Split('\n');
                    for (int i = 0; i < str.Length - 1; i++)
                    {
                        string str2 = "";
                        string[] str1 = str[i].Split(' ');
                        for (int j = 3; j < str1.Length; j++)
                            str2 += str1[j] + " ";
                        tableItems.Add(new TableItem() { Heading = str1[1] + " " + str1[2], SubHeading = str2 });
                    }
                }
                listView.Adapter = new HomeScreenAdapter(this, tableItems);
            });

            alert.SetNegativeButton("Отмена", (senderAlert, args) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        
    }
}