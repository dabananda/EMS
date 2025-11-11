using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EMS.Models.ViewModels
{
    public class CreateUserViewModel
    {
        // --- ধাপ ১: লগইন তথ্য ---
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // --- ধাপ ২: সাধারণ তথ্য (সবার জন্য) ---
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User Role")]
        public string Role { get; set; } // "Student" or "Teacher"
        public SelectList? RoleList { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public SelectList? DepartmentList { get; set; }


        // --- নতুন এই প্রপার্টিটি যোগ করো ---
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        // --- ধাপ ৩: শুধুমাত্র টিচারের জন্য ---
        [Display(Name = "Designation (If Teacher)")]
        public string? Designation { get; set; } // যেমন: "Lecturer"


        // --- ধাপ ৪: শুধুমাত্র স্টুডেন্টের জন্য ---
        [Display(Name = "Student Roll (If Student)")]
        public string? StudentRoll { get; set; }

        [Display(Name = "Registration No (If Student)")]
        public string? RegistrationNo { get; set; }

        [Display(Name = "Session (If Student)")]
        public string? Session { get; set; } // যেমন: "2020-21"

        [Display(Name = "Date of Birth (If Student)")]
        [DataType(DataType.Date)] // Date picker দেখানোর জন্য
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Blood Group (If Student)")]
        public string? BloodGroup { get; set; }

        [Display(Name = "Father's Name (If Student)")]
        public string? FatherName { get; set; }

        [Display(Name = "Mother's Name (If Student)")]
        public string? MotherName { get; set; }

        [Display(Name = "Address (If Student)")]
        public string? Address { get; set; }

        [Display(Name = "Semester (If Student)")]
        public int? SemesterId { get; set; }
        public SelectList? SemesterList { get; set; }
    }
}