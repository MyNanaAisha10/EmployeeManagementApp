
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class EditProfileViewModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; } = default!;
}
