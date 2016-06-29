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
    [Activity(Label = "Напоминание",Theme = "@style/MyTheme")]
    public class Add_Napominanie1 : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private EditText Napominalka;
        private Button Save;
        private Button Time;
        private Button Data;
        private Button Cancel;
        private int hour;
        private int minute;
        private int Chas = 0;
        private int Den = 1;
        private DateTime data;
        private int nomer_striki;
        private int id;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Add_Napominanie);

            Time = FindViewById<Button>(Resource.Id.button2);
            Data = FindViewById<Button>(Resource.Id.button1);
            Save = FindViewById<Button>(Resource.Id.Save);
            Cancel = FindViewById<Button>(Resource.Id.Cancel);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolBarr);
            Napominalka = FindViewById<EditText>(Resource.Id.Napominanie);
            SetSupportActionBar(mToolbar);


            Time.Click += (object sender, EventArgs e) => ShowDialog(Chas);
            Data.Click += (object sender, EventArgs e) => ShowDialog(Den);
            nomer_striki = Intent.GetIntExtra("id", 0);
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            data = DateTime.Today;
            if (nomer_striki < 0)
            {
                UpdateTextVremia();
                UpdateTextData();
            }
            else 
            {
                Create_Database dbr = new Create_Database();
                var res = dbr.GetAllRecords();
                if (res != "")
                {
                    string[] str = res.Split('\n');
                    string str2 = "";
                    string[] str1 = str[nomer_striki].Split(' ');
                    id = Convert.ToInt32(str1[0]);
                    Data.Text = str1[1];
                    Time.Text = str1[2];
                    for (int j = 3; j < str1.Length; j++)
                        str2 += str1[j] + " ";
                    Napominalka.Text = str2;
                }

            }
            

            Save.Click += Save_Click;
            Cancel.Click += Cancel_Click;
            
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Napominanie));
            this.StartActivity(intent);
        }

        void Save_Click(object sender, EventArgs e)
        {
            string data = Data.Text + " " + Time.Text;
            string napom = Napominalka.Text;
            string res="";
            Create_Database dbr = new Create_Database();
            if (nomer_striki < 0)
                res = dbr.Insertrecord(napom, data);
            else
                res = dbr.UpdateRecord(id, data, napom); ;
            var intent = new Intent(this, typeof(Napominanie));
            this.StartActivity(intent);
            Toast.MakeText(this, res, ToastLength.Short).Show();
        }
        private void UpdateTextData()
        {
            Data.Text = data.ToString("dd:MM:yyyy");
        }

        private void UpdateTextVremia()
        {
            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            Time.Text = time;
        }



        private void TimePikerCallBack(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;
            UpdateTextVremia();
        }

        private void DateTimeCAllBack(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            data = e.Date;
            UpdateTextData();
        }
        protected override Dialog OnCreateDialog(int id)
        {
            if (id == Chas)
                return new TimePickerDialog(this, TimePikerCallBack, hour, minute, false);
            if (id == Den)
                return new DatePickerDialog(this, DateTimeCAllBack, data.Year, data.Month - 1, data.Day);
            return null;
        }
    }
}