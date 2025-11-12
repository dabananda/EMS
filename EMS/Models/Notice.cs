using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Notice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)] // বড় টেক্সট এরিয়া দেখানোর জন্য
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; } = DateTime.Now; // ডিফল্ট: বর্তমান সময়

        // --- টার্গেট অডিয়েন্স (কাদের জন্য নোটিশ?) ---
        [Display(Name = "For Students")]
        public bool IsForStudents { get; set; } = true; // ডিফল্ট: হ্যাঁ

        [Display(Name = "For Teachers")]
        public bool IsForTeachers { get; set; } = true; // ডিফল্ট: হ্যাঁ
    }
}