using System.Collections.Generic; // এটা যোগ করো
using System.ComponentModel.DataAnnotations; // এটা যোগ করো

namespace EMS.Models
{
    public class Department
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // যেমন: "CSE"

        [Required]
        [StringLength(255)]
        public string FullName { get; set; } // যেমন: "Computer Science & Engineering"

        // Navigation Property: একটি ডিপার্টমেন্টে অনেক ইউজার থাকতে পারে
        public ICollection<ApplicationUser> Users { get; set; }

        // Navigation Property: একটি ডিপার্টমেন্টে অনেক কোর্স থাকতে পারে
        public ICollection<Course> Courses { get; set; }
    }
}