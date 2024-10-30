using System.ComponentModel.DataAnnotations.Schema;

namespace MCC.Models
{
    [Table("subject")]
    public class Subject
    {
        public Guid id { get; set; } 
        public String subject_name { get; set; }
        public Guid assigned_teacher { get; set; }
        public String period { get; set; }
    }
}
