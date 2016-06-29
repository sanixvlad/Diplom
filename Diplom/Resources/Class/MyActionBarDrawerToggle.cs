using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace Diplom
{
    public  class MyActionBarDrawerToggle:SupportActionBarDrawerToggle 
    {
        private ActionBarActivity mHostActivity;
        private int mOpenedResource;
        private int mClosedResource;
        public MyActionBarDrawerToggle (ActionBarActivity host, DrawerLayout drawerLayout,int openedResource,int closerResource)
            :base(host ,drawerLayout ,openedResource ,closerResource)
        {
            mHostActivity = host;
            mOpenedResource = openedResource;
            mClosedResource = closerResource;
        }

        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            mHostActivity.SupportActionBar.SetTitle(mOpenedResource); //текст открыто
        }

        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            mHostActivity.SupportActionBar.SetTitle(mClosedResource);//текст зактрыто
        }

        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}