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
    public class Raspisanie_vse
    {
        public int id_raspisania { get; set; }
        public int id_prepod { get; set; }
        public int id_gruppa { get; set; }
        public string imia_pari { get; set; }
        public int chislitel { get; set; }
        public int nomer_pari { get; set; }
        public string den_nedeli { get; set; }
        public string kabinet { get; set; }
    }
}