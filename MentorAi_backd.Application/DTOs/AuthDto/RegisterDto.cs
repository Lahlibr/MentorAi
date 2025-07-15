using System.ComponentModel.DataAnnotations;
using MentorAi_backd.Domain.Enums;
namespace MentorAi_backd.Application.DTOs.AuthDto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100,MinimumLength =3,ErrorMessage = "Username must be between 3-50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$",ErrorMessage = "Username can only contain letters, numbers and underscores")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100,MinimumLength = 8,ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [EnumDataType(typeof(UserEnum), ErrorMessage = "Invalid user role")]
        public UserEnum Role { get; set; } = UserEnum.Student;
    }
}
