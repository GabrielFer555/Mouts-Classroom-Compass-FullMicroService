using System.ComponentModel.DataAnnotations.Schema;

namespace MCC.Models
{
    [Table("student")]
    public class Student
    {
        public Guid id { get; set; }
        public String name { get; set; }
        public DateTime birth_date { get; set; }
        public String period { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String course { get; set; }
    }
}
