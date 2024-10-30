using System.ComponentModel.DataAnnotations.Schema;

namespace MCC.Models
{
    [Table("grade")]
    public class Grade
    {
        public Guid id { get; set; }
        public Guid student_id { get; set; }
        public DateTime grade_date { get; set; }
        public Guid subject_id { get; set; }
        public decimal grade { get; set; }
        public String grade_description { get; set; }
    }
}
