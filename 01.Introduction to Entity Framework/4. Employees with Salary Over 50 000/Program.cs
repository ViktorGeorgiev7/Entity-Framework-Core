using _4._Employees_with_Salary_Over_50_000.Data.Models;
using System.Text;

namespace _4._Employees_with_Salary_Over_50_000
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext();  
            var ab = db.Employees.Where(x => x.Salary > 50000M).Select(x => new { x.FirstName, x.Salary }).ToList();
            foreach (var item in GetEmployeesWithSalaryOver50000(db))
            {
                Console.Write(item);
            }
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in context.Employees.Where(x => x.Salary > 50000M).OrderBy(x => x.FirstName))
            {
                sb.AppendLine(item.FirstName + " " + Math.Round(item.Salary, 2));
            }
            return sb.ToString();
        }
    }
}