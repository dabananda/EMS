using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class StudentAttendance
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now; // ডিফল্ট: আজকের তারিখ

        [Required]
        public AttendanceStatus Status { get; set; } // Present, Absent, or Late

        // --- সম্পর্ক (Relationships) ---

        // কোন কোর্সের অ্যাটেনডেন্স?
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // কোন স্টুডেন্টের অ্যাটেনডেন্স?
        [Required]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }
    }
}