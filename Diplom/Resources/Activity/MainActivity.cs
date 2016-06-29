using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Threading;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Android.Net;

namespace Diplom
{
    [Activity(Label = "ПГУ ИТИ и ТК", Theme = "@style/MyTheme", MainLauncher = true)]
    public class MainActivity : ActionBarActivity
    {
        WebClient mClient;
        System.Uri mUrl;
        int username;
        string password;
        private SupportToolbar mToolbar;
        private Button EnterButton;
        private ProgressBar mProgressbar;
        EditText mUserName;
        EditText mPassword;
        CheckBox mCheckBox;
        public List<student> mStudent;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EnterButton = FindViewById<Button>(Resource.Id.button1);
            mProgressbar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox1);
            mUserName = FindViewById<EditText>(Resource.Id.Namber);
            mPassword = FindViewById<EditText>(Resource.Id.Password);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolStart);
            SetSupportActionBar(mToolbar);
            // создание БД
            Create_Database bdr = new Create_Database();
            var result = bdr.CeateDB();
            bdr.CreateTable();

            EnterButton.Click += (object sender, EventArgs e) =>
            {
                if (IsOnline()) //проверка соединения с интернетом
                {
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    Enter_Dialog enter = new Enter_Dialog();
                    enter.Show(transaction, "dialog fragment");
                    enter.mEnterButtonArg += enter_mEnterButtonArg;
                }
                else
                {
                    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                    alert.SetMessage("Нет соединения с интеренетом!");
                    alert.SetPositiveButton("Ок", (senderAlert, args) => { });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
            };
        }
        void enter_mEnterButtonArg(object sender, EnterButtonArg e)
        {
            // нажатие кнопки в диалоге
            mProgressbar.Visibility = ViewStates.Visible;
            password = e.Password;
            username = Convert.ToInt32(e.Namber);
            if(password ==""||username ==0)
            {
                string t = "Введите коректно данные!";
                Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
            }
            else
                ActLikeReqest();

        }

        private void ActLikeReqest()
        {
            mClient = new WebClient();
            mUrl = new System.Uri("http://sanix.prdi.ru/url_path.php");
            mClient.DownloadDataAsync(mUrl);
            mClient.DownloadDataCompleted += MClient_DownloadDataCompleted;
          
        }

        private void MClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            int flag = 0;
            string json1 = Encoding.UTF8.GetString(e.Result);
            mStudent = JsonConvert.DeserializeObject<List<student>>(json1);
            for (int i = 0; i < mStudent.Count; i++)
            {
                if (mStudent[i].id_studenta == username && mStudent[i].password == password)
                {
                    RunOnUiThread(() => { mProgressbar.Visibility = ViewStates.Invisible; });
                    var intent = new Intent(this, typeof(user));
                    intent.PutExtra("fio", mStudent[i].FIO);
                    intent.PutExtra("id_gruppi", mStudent[i].id_gruppi);
                    this.StartActivity(intent);
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                mProgressbar.Visibility = ViewStates.Invisible;
                string t = "Не верный логин или пароль!";
                Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
            }
        }

        public bool IsOnline()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool online = (activeConnection != null) && activeConnection.IsConnected;
            return online;
        }
    }
}

