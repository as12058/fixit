// ViewModels/UserSignupViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace Fixit.ViewModels
{
    public class UserSignupViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        // You might want to consider how UserType is set.
        // For a public signup, it's usually fixed (e.g., "Customer").
        // public string UserType { get; set; } = "Customer"; // Example for a fixed type
    }
}