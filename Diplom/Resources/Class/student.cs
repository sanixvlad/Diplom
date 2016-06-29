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
    public class student
    {
        public int id_studenta { get; set; }
        public string FIO { get; set; }
        public int id_gruppi { get; set; }
        public string password { get; set; }
    }

    public class student1
    {
        public static int flag;
        public static int ID_gruppi;
        public string FIO { get; set; }
        public string password { get; set; }
        public void ID_GRUPPI(int id_grippi)
        {
            student1.ID_gruppi = id_grippi;
        }
        public int Get_ID_GRUPPI()
        {
            return ID_gruppi;
        }
    }


}