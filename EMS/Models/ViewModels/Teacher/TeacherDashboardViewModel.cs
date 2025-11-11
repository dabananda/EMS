using System.ComponentModel.DataAnnotations;

namespace EMS.Models.ViewModels.Teacher // <--- লক্ষ্য করো: নতুন ফোল্ডার অনুযায়ী Namespace
{
    public class TeacherDashboardViewModel
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // ভবিষ্যতে আমরা এখানে "Assigned Courses" এর লিস্ট দেখাবো
        public int TotalAssignedCourses { get; set; } = 0;
    }
}