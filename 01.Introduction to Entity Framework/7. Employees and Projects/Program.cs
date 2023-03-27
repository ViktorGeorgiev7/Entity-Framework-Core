using _7._Employees_and_Projects.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _7._Employees_and_Projects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            GetEmployeesInPeriod(context);
           
        }
        public static void GetEmployeesInPeriod(SoftUniContext context)
        {
            var a = context.Employees.Include(x => x.Projects).Include(x=>x.Manager).Take(10);
            foreach (var employee in a) 
            {
                var list = employee.Projects.Where(x => x.StartDate.Year>2001).Where(x=>x.StartDate.Year<2003).ToList();  
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.Manager.FirstName} {employee.Manager.LastName}");                   
                foreach (var item in list)
                {
                    Console.WriteLine($"--{item.Name} - {item.StartDate} - {item.EndDate}");
                }
            }
        }
    }
}
