namespace EmployeePaycheckAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public List<Dependent> Dependents { get; set; } = new List<Dependent>();

        public decimal BaseBenefitCost => 1000m;
        public decimal AdditionalBenefitCost { get; set; } = 0m;  // Extra for high earners
    }
}
