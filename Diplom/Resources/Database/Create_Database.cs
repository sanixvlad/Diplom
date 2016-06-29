using System;
using System.Data;
using System.IO;
using SQLite;

namespace Diplom
{
    public class Create_Database
    {
        public string CeateDB()
        {
            var output = "";
            output += "Creating Database if it doesnt exist";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            var db = new SQLiteConnection(dbPath);
            output += "Database Created\n";
            return output;
        }

        public string CreateTable()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Tablicha_Napominanie>();
                string res = "Table Created";
                return res;
            }
            catch (Exception ex)
            {
                return "error " + ex.Message;
            }
        }

        public string Insertrecord(string napominanie1,string Data1)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
                var db = new SQLiteConnection(dbPath);
                Tablicha_Napominanie item= new Tablicha_Napominanie();
                item.napominanie =napominanie1;
                item.Data = Data1 ;
                db.Insert (item );
                return "Добавлено";
            }
            catch (Exception ex)
            {
                return "error :" + ex.Message;
            }
        }

        public string GetAllRecords()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            var db = new SQLiteConnection(dbPath);
            string uotput = "";
            var table = db.Table<Tablicha_Napominanie>();
            foreach (var item in table )
            {
                uotput += item.Id + " " + item.Data + " " + item.napominanie + "\n";
            }

            return uotput;
        }

        public string UpdateRecord(int id, string data, string napominalka)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<Tablicha_Napominanie>(id);
            item.Data = data;
            item.napominanie = napominalka;
            db.Update(item);
            return "Отредактировано...";
        }


        public string RemoveTask (int id)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            var db = new SQLiteConnection(dbPath);
            var item = db.Get<Tablicha_Napominanie>(id);
            db.Delete(item);
            return "Удалено...";
        }
    }
}