using System.ComponentModel.DataAnnotations;

namespace FindWork.ViewModels;

public class ProfileViewModel
{
    [Required]
    public string? ProfileName { get; set; }
    
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }
}