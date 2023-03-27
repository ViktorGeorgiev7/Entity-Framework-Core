using _13._Find_Employees_by_First_Name_Starting_with_Sa.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _13._Find_Employees_by_First_Name_Starting_with_Sa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
        }
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context) 
        { 
            var list = context
                .Employees
                .Where(x=>x.FirstName.StartsWith("Sa"))
                .Select(x=> new {x.FirstName,x.LastName,x.JobTitle,x.Salary })
                .OrderBy(x=>x.FirstName)
                .ThenBy(x=>x.LastName)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in list) 
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} - {item.JobTitle} - (${item.Salary:f2})");
            }
            return sb.ToString();
        }
    }
}