﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class EmployeeViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Department { get; set; } = default!;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

   public List<SelectListItem> DepartmentSelectList { get; set; } = new List<SelectListItem>();
}

public class EmployeesViewModel
{
    public List<EmployeeViewModel> Employees { get; set; } = [];
}

public class EmployeeDetailsViewModel
{ 
    public List<EmployeeViewModel> Employees { get; set; } = default!;
}






