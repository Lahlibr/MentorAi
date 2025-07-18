using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProfileDto
{
    public class StudentUpdateDto
    {
        [Required]
        [Range (10, 100, ErrorMessage = "Age must be between 10 and 100.")]
        public int Age { get; set; }
        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your current class/standard if you have not graduated.")]
        public string Graduation { get; set; }
        [Required(ErrorMessage = "Enter graduation year or your current STD  year.")]
        [Range(1980, 2100, ErrorMessage = "Graduation year must be between 1980 and 2100.")]
        public int GraduationYear { get; set; }
        public string? University { get; set; }
        public string? Major { get; set; }
        public string? ProfileImageUrl { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone(ErrorMessage = "Invalid phone number format in GuardianPhoneNu.")]
        public string GuardianPhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your GuardianName.")]
        public string GuardianName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string GuardianEmail { get; set; }
        
        [Required(ErrorMessage = "Please enter your GuardianRelationship.")]
        public string GuardianRelationship { get; set; }



    }
}
