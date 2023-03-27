using _3._Employees_Full_Information.Data.Models;
using System.Text;

namespace _3._Employees_Full_Information
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext();
            Console.WriteLine(GetEmployeesFullInformation(db));
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in context.Employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} {item.MiddleName} {item.JobTitle} {item.Salary}");
            }
            return sb.ToString();
        }
    }
}