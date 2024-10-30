using System.ComponentModel.DataAnnotations.Schema;

namespace MCC.Models
{
    [Table("teacher")]
    public class Teacher
    {
        public Guid id { get; set; } 
        public String name { get; set; }
        public DateTime birth_date { get; set; }
        public String email { get; set; }
        public String password { get; set; }
    }
}
