using EmployeePaycheckAPI.Models;

namespace EmployeePaycheckAPI.Services
{
    public class PayCheckService
    {
        public decimal CalculatePaycheck(Employee employee)
        {
            decimal yearlySalary = employee.Salary;
            decimal benefitCost = employee.BaseBenefitCost;

            // Add dependent costs
            benefitCost += employee.Dependents.Sum(d => d.MonthlyCost);

            // Add 2% if employee earns more than $80,000
            if (yearlySalary > 80000)
            {
                benefitCost += yearlySalary * 0.02m;
            }

            // Calculate net annual salary after benefits cost
            decimal annualBenefitsCost = benefitCost * 12;

            // Calculate net annual salary after benefits cost
            decimal netAnnualSalary = employee.Salary - annualBenefitsCost;

            // Calculate paycheck
            decimal paycheck = netAnnualSalary / 26;

            return paycheck;
;
        }
    }
}
