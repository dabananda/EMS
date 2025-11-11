using EMS.Data;
using EMS.Models;
using EMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context; // <--- ডেটাবেস কনটেক্সট যোগ করো

        [TempData]
        public string StatusMessage { get; set; }

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context; // <--- context ইনিশিয়ালাইজ করো
        }

        public IActionResult Index()
        {
            ViewBag.StatusMessage = StatusMessage;
            return View();
        }

        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult SharedPage()
        {
            return View();
        }

        // --- CreateUser (GET) মেথডটি রিপ্লেস করো ---
        public async Task<IActionResult> CreateUser()
        {
            // ড্রপডাউনের জন্য সব ডেটা লোড করো
            var roles = await _roleManager.Roles
                .Where(r => r.Name == "Student" || r.Name == "Teacher") // শুধু স্টুডেন্ট বা টিচার রোল দেখাও
                .ToListAsync();

            var departments = await _context.Departments.ToListAsync();
            var semesters = await _context.Semesters.ToListAsync();

            var model = new CreateUserViewModel
            {
                RoleList = new SelectList(roles, "Name", "Name"),
                DepartmentList = new SelectList(departments, "Id", "Name"),
                SemesterList = new SelectList(semesters, "Id", "Name")
            };
            return View(model);
        }

        // --- CreateUser (POST) মেথডটি রিপ্লেস করো ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            // ম্যানুয়ালি ভ্যালিডেশন চেক (রোল অনুযায়ী)
            if (model.Role == "Student")
            {
                if (model.SemesterId == null)
                    ModelState.AddModelError("SemesterId", "Semester is required for Students.");
                if (string.IsNullOrEmpty(model.StudentRoll))
                    ModelState.AddModelError("StudentRoll", "Student Roll is required for Students.");
                if (string.IsNullOrEmpty(model.RegistrationNo))
                    ModelState.AddModelError("RegistrationNo", "Registration No is required for Students.");
                if (string.IsNullOrEmpty(model.Session))
                    ModelState.AddModelError("Session", "Session is required for Students.");
                if (model.DateOfBirth == null)
                    ModelState.AddModelError("DateOfBirth", "Date of Birth is required for Students.");
                if (string.IsNullOrEmpty(model.FatherName))
                    ModelState.AddModelError("FatherName", "Father's Name is required for Students.");
                if (string.IsNullOrEmpty(model.MotherName))
                    ModelState.AddModelError("MotherName", "Mother's Name is required for Students.");
                if (string.IsNullOrEmpty(model.Address))
                    ModelState.AddModelError("Address", "Address is required for Students.");
            }
            else if (model.Role == "Teacher")
            {
                if (string.IsNullOrEmpty(model.Designation))
                    ModelState.AddModelError("Designation", "Designation is required for Teachers.");
            }

            if (ModelState.IsValid)
            {
                // ধাপ ১: ApplicationUser (লগইন) তৈরি করো
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true // আমরা অ্যাডমিন প্যানেল থেকে বানাচ্ছি, তাই কনফার্মড
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // ধাপ ২: ইউজারকে রোলে অ্যাসাইন করো
                    await _userManager.AddToRoleAsync(user, model.Role);

                    // ধাপ ৩: রোল অনুযায়ী প্রোফাইল তৈরি করো
                    if (model.Role == "Student")
                    {
                        var studentProfile = new StudentProfile
                        {
                            Id = user.Id, // 1-to-1 রিলেশনশিপ
                            StudentRoll = model.StudentRoll,
                            RegistrationNo = model.RegistrationNo,
                            Session = model.Session,
                            DateOfBirth = model.DateOfBirth.Value,
                            BloodGroup = model.BloodGroup,
                            FatherName = model.FatherName,
                            MotherName = model.MotherName,
                            Address = model.Address,
                            DepartmentId = model.DepartmentId,
                            SemesterId = model.SemesterId.Value
                        };
                        _context.StudentProfiles.Add(studentProfile);
                    }
                    else if (model.Role == "Teacher")
                    {
                        var teacherProfile = new TeacherProfile
                        {
                            Id = user.Id, // 1-to-1 রিলেশনশিপ
                            Designation = model.Designation,
                            DepartmentId = model.DepartmentId
                        };
                        _context.TeacherProfiles.Add(teacherProfile);
                    }

                    await _context.SaveChangesAsync(); // প্রোফাইল সেভ করো

                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("ListUsers"); // সফল হলে ইউজার লিস্টে পাঠাও
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // --- Error হলে ড্রপডাউনগুলো আবার লোড করো ---
            var roles = await _roleManager.Roles
                .Where(r => r.Name == "Student" || r.Name == "Teacher")
                .ToListAsync();
            var departments = await _context.Departments.ToListAsync();
            var semesters = await _context.Semesters.ToListAsync();

            model.RoleList = new SelectList(roles, "Name", "Name", model.Role);
            model.DepartmentList = new SelectList(departments, "Id", "Name", model.DepartmentId);
            model.SemesterList = new SelectList(semesters, "Id", "Name", model.SemesterId);

            return View(model);
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListModel = new List<UserListViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userListModel.Add(new UserListViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = string.Join(", ", roles)
                });
            }

            return View(userListModel);
        }
    }
}