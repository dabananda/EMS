using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        // --- এই ৪টি লাইন ডিলিট ---
        // public int? DepartmentId { get; set; } 
        // public Department? Department { get; set; }
        // public int? SemesterId { get; set; } 
        // public Semester? Semester { get; set; }

        public StudentProfile? StudentProfile { get; set; }
        public TeacherProfile? TeacherProfile { get; set; }
    }
}