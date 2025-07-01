using Application.Dtos;
using Application.Services.Department;
using Application.Services.Employee;
using Data.Context;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.DtoMapping;
using Presentation.Models;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly EmployeeAppDbContext _context;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, EmployeeAppDbContext context)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            if (employees is null)
            {
                return View(employees);
            }
            var viewModel = employees.ToViewModel();
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create(Guid departmentId)
        {
            var viewModel = new CreateEmployeeViewModel()
            {
                HireDate = DateTime.Today,
                DepartmentId = departmentId
            };

            await PopulateDepartmentDropdown(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Invalid input, please check all fields are correctly filled", "error");

                await PopulateDepartmentDropdown(viewModel);
                return View(viewModel);
            }

            var employeeModel = new CreateEmployeeDto()
            {
                EmployeeId = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Salary = viewModel.Salary,
                HireDate = viewModel.HireDate,
                DepartmentId = viewModel.DepartmentId,
            };
            
            var result = await _employeeService.CreateEmployeeAsync(employeeModel);
            if (result is null)
            {
                SetFlashMessage("An error occurred while creating the employee. Please check if there's any problem or employee already exist and try again.", "error");
                await PopulateDepartmentDropdown(viewModel);
                return View(viewModel);
            }
            
            SetFlashMessage("Employee created successfully!", "success");
            var allEmployees = await _employeeService.GetAllEmployeesAsync();
            return RedirectToAction("Index");
        }

        private async Task PopulateDepartmentDropdown(CreateEmployeeViewModel viewModel)
        {
            var departments = await _context.Departments.ToListAsync();

            viewModel.DepartmentSelectList = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employee is null)
            {
                TempData["Message"] = "Employee not found!";
                return RedirectToAction("Index");
            }

            var employeeDto = new UpdateEmployeeModel()
            {
                EmployeeId = employee.Id,
                DepartmentId = employee.DepartmentId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = $"{employee.Salary:N2}",
                HireDate = employee.HireDate,
            };
            return View(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeModel updateEmployeeModel)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Invalid input detected! Check again", "error");
                return View(updateEmployeeModel);
            }
            var employee = await _employeeService.GetEmployeeByIdAsync(updateEmployeeModel.EmployeeId);
            if (employee is null)
            {
                SetFlashMessage("Employee not found!", "error");
                return View(updateEmployeeModel);
            }

            var employeeDto = new EmployeeDto()
            {
                Id = updateEmployeeModel.EmployeeId,
                FirstName = updateEmployeeModel.FirstName,
                LastName = updateEmployeeModel.LastName,
                Email = updateEmployeeModel.Email,
                Salary = updateEmployeeModel.Salary,
                HireDate = updateEmployeeModel.HireDate
            };

            var result = await _employeeService.UpdateEmployeeAsync(employeeDto);
            if (result is null)
            {
                TempData["Message"] = "Failed to update employee details!";
                return View(updateEmployeeModel);
            }

            TempData["Message"] = "Successfully updated employee details!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid employeeId)
        {
            var allEmployees = await _employeeService.GetAllEmployeesAsync();
            try
            {
                var employee = _employeeService.DeleteEmployeeAsync(employeeId);
                TempData["Message"] = "Employee deleted successfully!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(allEmployees);
            }
        }

        public async Task<IActionResult> Detail(Guid employeeId)
        {
            var employees = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employees == null)
            {
                SetFlashMessage("Unable to view employee!", "error");
                return RedirectToAction("Index");
            }

            var employeeModel = employees.ToViewModel();
            return View(employeeModel);
        }
    }
}