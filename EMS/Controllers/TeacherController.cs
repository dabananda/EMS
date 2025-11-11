using EMS.Data;
using EMS.Models;
using EMS.Models.ViewModels.Teacher; // <--- নতুন ViewModel-এর রেফারেন্স
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    [Authorize(Roles = "Teacher")] // শুধু টিচাররা এক্সেস পাবে
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var teacher = await _context.Users
                .Include(u => u.TeacherProfile)
                    .ThenInclude(tp => tp.Department)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (teacher == null) return NotFound();

            // ViewModel-এ ডেটা ম্যাপ করা
            var model = new TeacherDashboardViewModel
            {
                Name = $"{teacher.FirstName} {teacher.LastName}",
                Designation = teacher.TeacherProfile?.Designation ?? "N/A",
                Department = teacher.TeacherProfile?.Department?.Name ?? "N/A",
                Email = teacher.Email,
                Phone = teacher.PhoneNumber,
                TotalAssignedCourses = 0 // আপাতত ০, পরে আমরা কোর্স অ্যাসাইন করলে এটা বাড়াবো
            };

            return View(model);
        }
    }
}