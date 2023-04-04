using System.ComponentModel.DataAnnotations;

namespace FindWork.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password is simple")]
    public string? Password { get; set; }
}