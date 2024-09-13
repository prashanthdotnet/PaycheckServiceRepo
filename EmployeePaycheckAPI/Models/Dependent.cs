namespace EmployeePaycheckAPI.Models
{
    public class Dependent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }  // "Spouse", "Child"
        public int Age { get; set; }

        public decimal MonthlyCost => Age > 50 ? 800m : 600m; // Additional $200 for dependents over 50
    }
}
