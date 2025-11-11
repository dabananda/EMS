using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class TeacherProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")] // 1-to-1 রিলেশনশিপ
        public string Id { get; set; } // এটি হবে ApplicationUser-এর Id-এর কপি

        [Required]
        public string Designation { get; set; } // যেমন: "Professor", "Lecturer"

        // Foreign Key
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // 1-to-1 রিলেশনশিপ
        public ApplicationUser ApplicationUser { get; set; }
    }
}