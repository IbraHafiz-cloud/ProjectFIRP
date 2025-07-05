using System.ComponentModel.DataAnnotations;

namespace ProjectFIRP.Models
{
    public class UserManagementViewModel
    {
        public List<UserDisplayModel> Users { get; set; } = new List<UserDisplayModel>();
        public CreateUserViewModel CreateUser { get; set; } = new CreateUserViewModel();
    }

    public class UserDisplayModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }

    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Username wajib diisi")]
        [StringLength(50, ErrorMessage = "Username maksimal 50 karakter")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        [StringLength(100, ErrorMessage = "Password minimal {2} karakter", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Konfirmasi password wajib diisi")]
        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password")]
        [Compare("Password", ErrorMessage = "Password dan konfirmasi password tidak sama")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; } = "User";
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Username wajib diisi")]
        [StringLength(50, ErrorMessage = "Username maksimal 50 karakter")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Status Aktif")]
        public bool IsActive { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password baru wajib diisi")]
        [StringLength(100, ErrorMessage = "Password minimal {2} karakter", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password Baru")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Konfirmasi password wajib diisi")]
        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password Baru")]
        [Compare("NewPassword", ErrorMessage = "Password dan konfirmasi password tidak sama")]
        public string ConfirmPassword { get; set; }
    }
}