using EMS.Data;
using EMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    [Authorize(Roles = "Student")] // শুধুমাত্র স্টুডেন্টরা এই কন্ট্রোলারে ঢুকতে পারবে
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // লগইন করা স্টুডেন্টের আইডি বের করো
            var userId = _userManager.GetUserId(User);

            // স্টুডেন্টের প্রোফাইল তথ্য লোড করো
            var student = await _context.Users
                .Include(u => u.StudentProfile)
                    .ThenInclude(sp => sp.Department)
                .Include(u => u.StudentProfile)
                    .ThenInclude(sp => sp.Semester)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}