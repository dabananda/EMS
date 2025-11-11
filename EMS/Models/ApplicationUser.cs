using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations; // এটা যোগ করো

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

        // --- এই ৪টি লাইন ডিলিট করো ---
        // public int? DepartmentId { get; set; } 
        // public Department? Department { get; set; }
        // public int? SemesterId { get; set; } 
        // public Semester? Semester { get; set; }

        // --- নতুন এই ২টি লাইন যোগ করো ---
        // Navigation Properties (1-to-1)
        public StudentProfile? StudentProfile { get; set; }
        public TeacherProfile? TeacherProfile { get; set; }
    }
}