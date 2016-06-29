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
    public class EnterButtonArg : EventArgs
    {
        private string mNamber;
        private string mPassword;
        private bool mCheckBox;

        public bool CheckBox
        {
            get { return mCheckBox; }
            set { mCheckBox = value; }
        }
        public string Namber
        {
            get { return mNamber; }
            set { mNamber = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public EnterButtonArg(string namber, string password ,bool checkbox)
            : base()
        {
            Namber = namber;
            Password = password;
            CheckBox = checkbox;
        }
    }
    public class Enter_Dialog:DialogFragment 
    {
        private EditText mNamber;
        private EditText mPassword;
        private Button mEnter;
        private CheckBox mChecBox;
        public event EventHandler<EnterButtonArg> mEnterButtonArg;
       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedIns)
        {
            base.OnCreateView(inflater, container, savedIns);
            var view = inflater.Inflate(Resource.Layout.Enter_Dialog1, container, true);
            mPassword = view.FindViewById<EditText>(Resource.Id.Password);
            mNamber = view.FindViewById<EditText>(Resource.Id.Namber);
            mEnter = view.FindViewById<Button>(Resource.Id.button_enter_dialog);
            mChecBox = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
            mEnter.Click += mEnter_Click;
            return view;
        }

        void mEnter_Click(object sender, EventArgs e)
        {

            mEnterButtonArg.Invoke(this, new EnterButtonArg(mNamber.Text, mPassword.Text, mChecBox.Checked));
            this.Dismiss();

        }

        public override void OnActivityCreated(Bundle savedInstendState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstendState);
        }
    }
}