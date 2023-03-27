using _12._Increase_Salary.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace _12._Increase_Salary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(IncreaseSalaries(context));

        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var list = context.Employees.Include(x => x.Department).Where(x => x.Department.Name == "Engineering"
            || x.Department.Name == "Tool Design"
            || x.Department.Name == "Marketing"
            || x.Department.Name == "Information Services")
            .OrderBy(x=>x.FirstName)
            .ThenBy(x=>x.LastName)
            .ToList();
            foreach (var item in list){item.Salary *= 0.12m;}
            context.SaveChanges();
            StringBuilder sb = new StringBuilder();
            foreach (Employee item in list)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} (${item.Salary})");
            }
            return sb.ToString();
        }
    }
}