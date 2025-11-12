using System.ComponentModel.DataAnnotations;
using EMS.Models; // AttendanceStatus Enum-এর জন্য

namespace EMS.Models.ViewModels.Teacher
{
    public class AttendanceViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now; // ডিফল্ট: আজকের তারিখ

        // স্টুডেন্টদের লিস্ট
        public List<StudentAttendanceRow> Students { get; set; } = new List<StudentAttendanceRow>();
    }

    // টেবিলের প্রতিটি লাইনের জন্য ছোট ক্লাস
    public class StudentAttendanceRow
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present; // ডিফল্ট: উপস্থিত
    }
}