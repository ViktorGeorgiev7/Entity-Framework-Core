using _5._Employees_from_Research_and_Development.Data.Models;
using System.Text;

namespace _5._Employees_from_Research_and_Development
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext(); 
            foreach (var e in GetEmployeesFromResearchAndDevelopment(db)) 
            {
                Console.Write(e);
            }
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var employee in context.Employees.Where(x => x.Department.Name == "Research and Development").OrderBy(x => x.Salary).ThenByDescending(x => x.FirstName))
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:f2}");
            }
            return sb.ToString();
        }
    }
}