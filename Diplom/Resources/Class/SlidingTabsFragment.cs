using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using System.Net;
using Newtonsoft.Json;

namespace Diplom
{
    public class SlidingTabsFragment : Android.Support.V4.App.Fragment 
    {
        private SlidingTabScrolView mSlidingTabScrollView;
        private ViewPager mViewPager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Raspisanie, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrolView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter();

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class SamplePagerAdapter : PagerAdapter
        {
            List<string> items = new List<string>();
            WebClient mClient;
            List<string> raspisanie = new List<string>();
            List<string> raspisanie1 = new List<string>();
            List<string> raspisanie2 = new List<string>();
            List<string> raspisanie3 = new List<string>();
            List<string> raspisanie4 = new List<string>();
            List<string> raspisanie5 = new List<string>();
            List<string> raspisanie6 = new List<string>();
            System.Uri mUrl;
            public List<Raspisanie_vse> mRaspisanie_vse;
            public ArrayAdapter adapter, adapter1, adapter2, adapter3, adapter4, adapter5, adapter6;
            ViewGroup p;
            ProgressBar progressbar, progressbar1, progressbar2, progressbar3, progressbar4, progressbar5, progressbar6;
            int flag,flag2,flag3,flagurl;
            ListView ListRaspisanie, ListRaspisanie1, ListRaspisanie2, ListRaspisanie3, ListRaspisanie4, ListRaspisanie5, ListRaspisanie6;

            public SamplePagerAdapter()
                : base()
            {
                items.Add("Сегодня");
                items.Add("Понедельник");
                items.Add("Вторник");
                items.Add("Среда");
                items.Add("Четверг");
                items.Add("Пятница");
                items.Add("Суббота");
            }

            public override int Count
            {
                get { return items.Count; }
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                /////////////////
                DateTime data_segodnia = DateTime.Today;
                string st = data_segodnia.DayOfWeek.ToString();
                switch (data_segodnia.DayOfWeek.ToString())
                {
                    case "Monday":
                        st = "понедельник";
                        break;
                    case "Tuesday":
                        st = "вторник";
                        break;
                    case "Wednesday":
                        st = "среда";
                        break;
                    case "Thursday":
                        st = "четверг";
                        break;
                    case "Friday":
                        st = "пятница";
                        break;
                    case "Saturday":
                        st = "суббота";
                        break;
                    default:
                        break;
                }
                p = container;
                View view = null;
                if (flagurl < 2)
                {
                    string URL1 = "http://sanix.prdi.ru/Raspisanie_Vse.php";
                    Zapolnenie_Raspisania(URL1, container);
                    flagurl++;
                }
                switch (position)
                    {
                    case 0:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_segodnia, container, false);
                        container.AddView(view);
                        progressbar = view.FindViewById<ProgressBar>(Resource.Id.progressBar2);
                        ListRaspisanie = view.FindViewById<ListView>(Resource.Id.listViewRaspisanie);
                        progressbar.Visibility = ViewStates.Invisible;
                        ListRaspisanie.Visibility = ViewStates.Visible;
                        if(flag2==0)
                        {
                            flag2++;
                        }
                        else
                        {
                            if (adapter != null)
                                adapter.Clear();
                            if (raspisanie.Count != 0)
                                raspisanie.Clear();
                            for (int j = 0; j < mRaspisanie_vse.Count; j++)
                                if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == st)
                                    raspisanie.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                            adapter = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie);
                            ListRaspisanie.Adapter = adapter;
                        }
                        break;
                    case 1:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_ponedelnik, container, false);
                        container.AddView(view);
                        progressbar1 = view.FindViewById<ProgressBar>(Resource.Id.progressBar3);
                        ListRaspisanie1 = view.FindViewById<ListView>(Resource.Id.listViewRaspisaniePonedelnik);
                        progressbar1.Visibility = ViewStates.Invisible;
                        ListRaspisanie1.Visibility = ViewStates.Visible;
                        if (flag3 == 0)
                            flag3++;
                        else
                        {
                            if (adapter1 != null)
                                adapter1.Clear();
                            if (raspisanie1.Count != 0)
                                raspisanie1.Clear();
                            for (int j = 0; j < mRaspisanie_vse.Count; j++)
                                if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "понедельник")
                                    raspisanie1.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                            adapter1 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie1);
                            ListRaspisanie1.Adapter = adapter1;
                        }
                        break;
                    case 2:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_vtornik, container, false);
                        container.AddView(view);
                        progressbar2 = view.FindViewById<ProgressBar>(Resource.Id.progressBar4);
                        ListRaspisanie2 = view.FindViewById<ListView>(Resource.Id.listViewRaspisanieVtornik);
                        progressbar2.Visibility = ViewStates.Invisible;
                        ListRaspisanie2.Visibility = ViewStates.Visible;
                        if (adapter2 != null)
                            adapter2.Clear();
                        if (raspisanie2.Count != 0)
                            raspisanie2.Clear();
                        for (int j = 0; j < mRaspisanie_vse.Count; j++)
                            if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "вторник")
                                raspisanie2.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                        adapter2 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie2);
                        ListRaspisanie2.Adapter = adapter2;
                        break;
                    case 3:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_sreda, container, false);
                        container.AddView(view);
                        progressbar3 = view.FindViewById<ProgressBar>(Resource.Id.progressBar5);
                        ListRaspisanie3 = view.FindViewById<ListView>(Resource.Id.listViewRaspisanieSreda);
                        progressbar3.Visibility = ViewStates.Invisible;
                        ListRaspisanie3.Visibility = ViewStates.Visible;
                        flag = 3;
                        if (adapter3 != null)
                            adapter3.Clear();
                        if (raspisanie3.Count != 0)
                            raspisanie3.Clear();
                        for (int j = 0; j < mRaspisanie_vse.Count; j++)
                            if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "среда")
                                raspisanie3.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                        adapter3 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie3);
                        ListRaspisanie3.Adapter = adapter3;
                        break;
                    case 4:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_chetverg, container, false);
                        container.AddView(view);
                        progressbar4 = view.FindViewById<ProgressBar>(Resource.Id.progressBar6);
                        ListRaspisanie4 = view.FindViewById<ListView>(Resource.Id.listViewRaspisanieChetverg);
                        progressbar4.Visibility = ViewStates.Invisible;
                        ListRaspisanie4.Visibility = ViewStates.Visible;
                        flag = 4;
                        if (adapter4 != null)
                            adapter4.Clear();
                        if (raspisanie4.Count != 0)
                            raspisanie4.Clear();
                        for (int j = 0; j < mRaspisanie_vse.Count; j++)
                            if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "четверг")
                                raspisanie4.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                        adapter4 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie4);
                        ListRaspisanie4.Adapter = adapter4;
                        break;
                    case 5:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_piatnicha, container, false);
                        container.AddView(view);
                        progressbar5 = view.FindViewById<ProgressBar>(Resource.Id.progressBar7);
                        ListRaspisanie5 = view.FindViewById<ListView>(Resource.Id.listViewRaspisaniePiatnicha);
                        progressbar5.Visibility = ViewStates.Invisible;
                        ListRaspisanie5.Visibility = ViewStates.Visible;
                        flag = 5;
                        if (adapter5 != null)
                            adapter5.Clear();
                        if (raspisanie5.Count != 0)
                            raspisanie5.Clear();
                        for (int j = 0; j < mRaspisanie_vse.Count; j++)
                            if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "пятница")
                                raspisanie5.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                        adapter5 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie5);
                        ListRaspisanie5.Adapter = adapter5;
                        break;
                    case 6:
                        view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.Raspisanie_subbota, container, false);
                        container.AddView(view);
                        progressbar6 = view.FindViewById<ProgressBar>(Resource.Id.progressBar8);
                        ListRaspisanie6 = view.FindViewById<ListView>(Resource.Id.listViewRaspisanieSubbota);
                        progressbar6.Visibility = ViewStates.Invisible;
                        ListRaspisanie6.Visibility = ViewStates.Visible;
                        if (adapter6 != null)
                            adapter6.Clear();
                        if (raspisanie6.Count != 0)
                            raspisanie6.Clear();
                        for (int j = 0; j < mRaspisanie_vse.Count; j++)
                            if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "суббота")
                                raspisanie6.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                        adapter6 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie6);
                        ListRaspisanie6.Adapter = adapter6;
                        break;
                    default:
                        break;

                    }

                return view;

                
            }

            public string GetHeaderTitle(int position)
            {
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }

            private void MClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
            {
                DateTime data_segodnia = DateTime.Today;
                string st = data_segodnia.DayOfWeek.ToString();
                switch (data_segodnia.DayOfWeek.ToString())
                {
                    case "Monday":
                        st = "понедельник";
                        break;
                    case "Tuesday":
                        st = "вторник";
                        break;
                    case "Wednesday":
                        st = "среда";
                        break;
                    case "Thursday":
                        st = "четверг";
                        break;
                    case "Friday":
                        st = "пятница";
                        break;
                    case "Saturday":
                        st = "суббота";
                        break;
                    default:
                        break;
                }
                string json1 = Encoding.UTF8.GetString(e.Result);
                mRaspisanie_vse = JsonConvert.DeserializeObject<List<Raspisanie_vse>>(json1);
                int t = student1.ID_gruppi;
                    switch (flag)
                    {
                        case 0:
                        if (adapter != null)
                            adapter.Clear();
                        if (raspisanie.Count != 0)
                            raspisanie.Clear();
                            for (int j = 0; j < mRaspisanie_vse.Count; j++)
                                if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == st)
                                    raspisanie.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                            adapter = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie);
                            ListRaspisanie.Adapter = adapter;
                            flag++;
                            break;
                    case 1:
                        if (adapter1 != null)
                            adapter1.Clear();
                        if (raspisanie1.Count != 0)
                            raspisanie1.Clear();
                        for (int j = 0; j < mRaspisanie_vse.Count; j++)
                            if (mRaspisanie_vse[j].id_gruppa == student1.ID_gruppi && mRaspisanie_vse[j].den_nedeli == "понедельник")
                                raspisanie1.Add(mRaspisanie_vse[j].nomer_pari.ToString() + "  " + mRaspisanie_vse[j].imia_pari + " кабинет:" + mRaspisanie_vse[j].kabinet);
                        adapter1 = new ArrayAdapter<string>(p.Context, Android.Resource.Layout.SimpleExpandableListItem1, raspisanie1);
                        ListRaspisanie1.Adapter = adapter1;
                        flag++;
                        break;
                    default:
                            break;
                    }
                  
            }

            public void Zapolnenie_Raspisania(string URL, ViewGroup container)
            {
                mClient = new WebClient();
                mUrl = new System.Uri(URL);
                mClient.DownloadDataAsync(mUrl);
                mClient.DownloadDataCompleted += MClient_DownloadDataCompleted;
            }

        }
    }
}