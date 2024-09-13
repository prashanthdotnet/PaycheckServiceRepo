using EmployeePaycheckAPI.Controllers;
using EmployeePaycheckAPI.Models;
using EmployeePaycheckAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EmployeePaycheckAPI.Tests
{
    public class PayCheckServiceTests
    {
        private readonly PayCheckService _paycheckService = new PayCheckService();

        [Fact]
        public void CalculatePaycheck_ForSingleEmployee_ShouldReturnCorrectAmount()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "John Doe",
                Salary = 90000
            };

            // Act
            var paycheck = _paycheckService.CalculatePaycheck(employee);

            // Assert
            Assert.Equal(2169.23m, paycheck,2);  
        }

        [Fact]
        public void CalculatePaycheck_WithDependents_ShouldIncludeAdditionalCosts()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "Jane Smith",
                Salary = 90000,
                Dependents = new List<Dependent>
                {
                    new Dependent { Name = "Spouse", Age = 30, Relationship = "Spouse" },
                    new Dependent { Name = "Child", Age = 10, Relationship = "Child" }
                }
            };

            // Act
            var paycheck = _paycheckService.CalculatePaycheck(employee);

            // Assert
            Assert.Equal(1615.38m, paycheck,2);
        }

        [Fact]
        public void GetEmployeeWithDependents_ReturnsEmployee_WhenEmployeeExists()
        {
            // Arrange
            var controller = new EmployeeController();

            // Act
            var actionResult = controller.GetEmployeeWithDependents(1);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);  // Ensure result is OkObjectResult
            Assert.IsType<Employee>(result.Value);  // Check that result contains an Employee object

            var employee = result.Value as Employee;
            Assert.NotNull(employee);  // Ensure employee is not null
            Assert.Equal(1, employee.Id);  // Validate employee ID
            Assert.Equal("John Doe", employee.Name);  // Validate employee Name
            Assert.Equal(2, employee.Dependents.Count);  // Ensure employee has 2 dependents
        }

        [Fact]
        public void GetEmployeeWithDependents_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var controller = new EmployeeController();

            // Act
            var actionResult = controller.GetEmployeeWithDependents(999);  // Non-existent ID

            // Assert
            var notFoundResult = actionResult.Result as NotFoundResult;
            Assert.NotNull(notFoundResult);  // Ensure result is NotFoundResult
        }
    }
}
