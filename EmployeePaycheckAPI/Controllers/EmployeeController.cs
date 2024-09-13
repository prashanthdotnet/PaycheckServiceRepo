using EmployeePaycheckAPI.Models;
using EmployeePaycheckAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePaycheckAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PayCheckService _paycheckService = new PayCheckService();

        private static List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John Doe",
                Salary = 90000m,
                Dependents = new List<Dependent>
                {
                    new Dependent { Id = 1, Name = "Jane Doe", Age = 35, Relationship = "Spouse" },
                    new Dependent { Id = 2, Name = "Johnny Doe", Age = 8, Relationship = "Child" }
                }
            }
        };

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return employees;
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            employees.Add(employee);
            return Ok();
        }

        [HttpGet("{id}/paycheck")]
        public ActionResult<decimal> GetPaycheck(int id)
        {
            var employee = employees.Find(e => e.Id == id);
            if (employee == null)
                return NotFound();

            var paycheck = _paycheckService.CalculatePaycheck(employee);
            return paycheck;
        }

       

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeWithDependents(int id)
        {
            var employee = employees.SingleOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
