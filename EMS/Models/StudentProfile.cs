using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class StudentProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")] // 1-to-1 রিলেশনশিপ
        public string Id { get; set; } // এটি হবে ApplicationUser-এর Id-এর কপি

        [Required]
        public string StudentRoll { get; set; }

        [Required]
        public string RegistrationNo { get; set; }

        [Required]
        public string Session { get; set; } // যেমন: "2020-2021"

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? BloodGroup { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string MotherName { get; set; }

        [Required]
        public string Address { get; set; }

        // Foreign Keys
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        // 1-to-1 রিলেশনশিপ
        public ApplicationUser ApplicationUser { get; set; }
    }
}