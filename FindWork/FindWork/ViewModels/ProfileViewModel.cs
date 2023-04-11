using System.ComponentModel.DataAnnotations;

namespace FindWork.ViewModels;

public class ProfileViewModel
{
    public int ProfileId { get; set; }
    
    [Required]
    public string? ProfileName { get; set; }
    
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }
}