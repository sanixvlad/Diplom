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
using Android.Util;
using Android.Animation;
using Android.Support.V4.View;

namespace Diplom
{
    public class SlidingTabScrolView : HorizontalScrollView
    {
        private const int TITLE_OFFSET_DIPS = 24;
        private const int TAB_VIEW_PADDING_DIPS = 16;
        private const int TAB_VIEW_TEXT_SIZE_SP = 12;
        private int mTitleOffset;

        //private int mTitleViewLayoutID;
        //private int mTabViewTextViedID;
        private ViewPager mViewPager;
        private ViewPager.IOnPageChangeListener mViewPagerChanngeLListener;

        private static SlidingTabStrip mTabStrip1;

        private int mScrollState;

        public interface TabColorizer
        {
            int GetIndicatorColor(int position);
            int GetDividerColor(int position);
        }

        public SlidingTabScrolView(Context context)
            : this(context, null)
        {

        }

        public SlidingTabScrolView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {

        }

        public SlidingTabScrolView(Context context, IAttributeSet attrs, int defaultSryle)
            : base(context, attrs, defaultSryle)
        {
            //disable the scroll bar
            HorizontalFadingEdgeEnabled = false;
            //Make sure the tab strips fill the view
            FillViewport = true;
            this.SetBackgroundColor(Android.Graphics.Color.Rgb(0xE5, 0xE5, 0xE5));//Gray color
            mTitleOffset = (int)(TITLE_OFFSET_DIPS * Resources.DisplayMetrics.Density);
            mTabStrip1 = new SlidingTabStrip(context);
            this.AddView(mTabStrip1, LayoutParams.MatchParent, LayoutParams.MatchParent);

        }

        public TabColorizer CastomTabColorizer
        {
            set
            { mTabStrip1.CustomTabColorizer = value; }
        }
        public int[] SelectedIndicatoColor
        {
            set { mTabStrip1.SelectedIndicatorColors = value; }
        }

        public int[] DividerColors
        {
            set { mTabStrip1.DividerColors = value; }
        }

        public ViewPager.IOnPageChangeListener OnPageListener
        {
            set { mViewPagerChanngeLListener = value; }
        }

        public ViewPager ViewPager
        {
            set
            {
                mTabStrip1.RemoveAllViews();
                mViewPager = value;
                if (value != null)
                {
                    value.PageSelected += value_PageSelected;
                    value.PageScrollStateChanged += value_PageScrollStateChanged;
                    value.PageScrolled += value_PageScrolled;
                    PopulateTabStrip();
                }
            }
        }

        void value_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            int tabCont = mTabStrip1.ChildCount;
            if (tabCont == 0 || (e.Position < 0) || (e.Position >= tabCont))
            {
                return;
            }
            mTabStrip1.OnViewPagerPageChanged(e.Position, e.PositionOffset);
            View selectedTitle = mTabStrip1.GetChildAt(e.Position);
            int extraOffset = (selectedTitle != null ? (int)(e.Position * selectedTitle.Width) : 0);
            ScrolltoTab(e.Position, extraOffset);
            if (mViewPagerChanngeLListener != null)
            {
                mViewPagerChanngeLListener.OnPageScrolled(e.Position, e.PositionOffset, e.PositionOffsetPixels);
            }
        }

        private void ScrolltoTab(int tabIndex, int extraOffset)
        {
            int tabCount = mTabStrip1.ChildCount;
            if (tabCount == 0 || tabIndex < 0 || tabIndex >= tabCount)
            {
                return;
            }
            View selectedChild = mTabStrip1.GetChildAt(tabIndex);
            if (selectedChild != null)
            {
                int scrollAmountX = selectedChild.Left + extraOffset;
                if (tabIndex > 0 || extraOffset > 0)
                {
                    scrollAmountX -= mTitleOffset;
                }
                this.ScrollTo(scrollAmountX, 0);
            }
        }

        private void PopulateTabStrip()// присвоение номера страницы на активити и ее цвета
        {
            PagerAdapter adapter = mViewPager.Adapter;
            for (int i = 0; i < adapter.Count; i++)
            {
                TextView tabView = CreateDefaultTabView(Context);
                tabView.Text = ((SlidingTabsFragment.SamplePagerAdapter)adapter).GetHeaderTitle(i);
                tabView.SetTextColor(Android.Graphics.Color.Black);
                tabView.Tag = i;
                tabView.Click += tabView_Click;
                mTabStrip1.AddView(tabView);
            }
        }

        void tabView_Click(object sender, EventArgs e)
        {
            TextView clickTab = (TextView)sender;
            int pageToScrollTo = (int)clickTab.Tag;
            mViewPager.CurrentItem = pageToScrollTo;

        }

        private TextView CreateDefaultTabView(Android.Content.Context context)
        {
            TextView textView = new TextView(context);
            textView.Gravity = GravityFlags.Center;
            textView.SetTextSize(ComplexUnitType.Sp, TAB_VIEW_TEXT_SIZE_SP);
            textView.Typeface = Android.Graphics.Typeface.Default;
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Honeycomb)
            {
                TypedValue outValue = new TypedValue();
                Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, outValue, false);
                textView.SetBackgroundResource(outValue.ResourceId);
            }

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.IceCreamSandwich)
            {
                textView.SetAllCaps(true);
            }
            int padding = (int)(TAB_VIEW_PADDING_DIPS * Resources.DisplayMetrics.Density);
            textView.SetPadding(padding, padding, padding, padding);
            return textView;
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            if (mViewPager != null)
            {
                ScrolltoTab(mViewPager.CurrentItem, 0);
            }
        }

        void value_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            mScrollState = e.State;
            if (mViewPagerChanngeLListener != null)
            {
                mViewPagerChanngeLListener.OnPageScrollStateChanged(e.State);
            }
        }

        void value_PageSelected(object sender, Android.Support.V4.View.ViewPager.PageSelectedEventArgs e)
        {
            if (mScrollState == ViewPager.ScrollStateIdle)
            {
                mTabStrip1.OnViewPagerPageChanged(e.Position, 0f);
                ScrolltoTab(e.Position, 0);
            }
            if (mViewPagerChanngeLListener != null)
            {
                mViewPagerChanngeLListener.OnPageSelected(e.Position);
            }
        }
    }
}