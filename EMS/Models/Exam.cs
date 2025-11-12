using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Exam
    {
        public int Id { get; set; }

        [Required]
        public ExamType ExamType { get; set; } // যেমন: Midterm

        [Required]
        [StringLength(100)]
        public string Title { get; set; } // যেমন: "CT-1" বা "Midterm Exam 2025"

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = "Total marks must be between 1 and 200")]
        public double TotalMarks { get; set; } // পরীক্ষার পূর্ণমান

        // --- Foreign Key (কোন কোর্সের এক্সাম?) ---
        [Required]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}