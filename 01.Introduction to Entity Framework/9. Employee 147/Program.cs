using _9._Employee_147.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
using System.Text;

namespace _9._Employee_147
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployee147(context));
        }
        public static string GetEmployee147(SoftUniContext context)
        {
            var a = context.Employees.Include(x=>x.Projects).First(x=>x.EmployeeId == 147);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            foreach (var x in a.Projects.OrderBy(x=>x.Name)) 
            {                
                sb.AppendLine(x.Name);
            }
            return $"{a.FirstName} {a.LastName} {a.JobTitle} {sb}";
        }
    }
}