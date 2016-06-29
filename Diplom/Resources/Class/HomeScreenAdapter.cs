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

namespace Diplom 
{
    public class HomeScreenAdapter : BaseAdapter<TableItem> 
  {
    List<TableItem> items;
    Activity context;
    public HomeScreenAdapter(Activity context, List<TableItem> items)
        : base()
    {
        this.context = context;
        this.items = items;
    }
    public override long GetItemId(int position)
    {
        return position;
    }
    public override TableItem this[int position]
    {
        get { return items[position]; }
    }
    public override int Count
    {
        get { return items.Count; }
    }
    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        var item = items[position];

        View view = convertView;
        if (view == null) // no view to re-use, create new
            view = context.LayoutInflater.Inflate(Resource.Layout.Obrazech, null);
        view.FindViewById<TextView>(Resource.Id.txtData).Text = item.Heading;
        view.FindViewById<TextView>(Resource.Id.txtNapominanie).Text = item.SubHeading;

        return view;
    }
  }
}