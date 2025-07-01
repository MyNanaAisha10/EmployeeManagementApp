using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CreateEmployeeViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]


        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "Email name is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }

        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }

        [BindNever]
        public IEnumerable<SelectListItem> DepartmentSelectList { get; set; } = default!;
    }
}
