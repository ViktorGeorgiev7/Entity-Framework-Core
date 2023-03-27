using _10._Departments_with_More_Than_5_Employees.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _10._Departments_with_More_Than_5_Employees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var list = context.Departments.Include(x => x.Employees).ThenInclude(x=>x.Manager).Where(x=>x.Employees.Count()>5).OrderBy(x=>x.Employees.Count()).ThenBy(x=>x.Name).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var d in list) {
                sb.AppendLine($"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}");
                foreach (var e in d.Employees.OrderBy(x=>x.FirstName).ThenBy(x=>x.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }
            return sb.ToString();
        }
    }
}