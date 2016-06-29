using SQLite;

namespace Diplom
{
    [Table ("dannie_studenta")]
    public class dannie_studenta
    {
        [PrimaryKey,  Column("_ID")]
        public int Login { get; set; }

        [MaxLength(100)]
        public int  Password { get; set; }

        [MaxLength(100)]
        public string id_gruppi { get; set; }

        [MaxLength(15)]
        public int id_studenta { get; set; }

        [MaxLength(100)]
        public string fio { get; set; }
    }
}