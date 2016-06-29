using System;
using System.Data;
using System.IO;
using SQLite;

namespace Diplom
{
    [Table ("Tablicha_Napominanie")]
    public class Tablicha_Napominanie
    {
        [PrimaryKey, AutoIncrement, Column("_ID")]
        public int Id { get; set; }

        [MaxLength(100)]
        public string napominanie { get; set; }

        [MaxLength(100)]
        public string Data { get; set; }
    }
}