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

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}