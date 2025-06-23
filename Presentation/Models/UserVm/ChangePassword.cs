
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class ChangePasswordViewModel
{
    [Required]
    [PasswordPropertyText]
    public required string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
