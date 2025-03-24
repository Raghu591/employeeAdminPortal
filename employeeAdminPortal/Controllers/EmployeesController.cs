using employeeAdminPortal.Data;
using employeeAdminPortal.Models;
using employeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employeeAdminPortal.Controllers
{
    /// <summary>
    /// localhost:xxxx/api/employees
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmploees = dbContext.Employees.ToList();

            return Ok(allEmploees);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto  addEmployeeDto)
        {
            var empEntity = new Employee()
            {
                Email = addEmployeeDto.Email,
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            dbContext.Employees.Add(empEntity); // it will not save the changes
            dbContext.SaveChanges(); // then only changes will apply, it mandatory to save changes

            return Ok(empEntity);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee);

            
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();

            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var emp = dbContext.Employees.Find(id);
            if(emp is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();

            return Ok();
        }

    }
}
