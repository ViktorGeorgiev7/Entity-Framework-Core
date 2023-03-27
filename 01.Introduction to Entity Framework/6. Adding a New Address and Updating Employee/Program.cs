using _6._Adding_a_New_Address_and_Updating_Employee.Data.Models;
using System.Text;

namespace _6._Adding_a_New_Address_and_Updating_Employee
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(AddNewAddressToEmployee(context));


        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {           
            StringBuilder sb = new StringBuilder();
            var a = context.Employees.OrderByDescending(x => x.AddressId).Select(x => new { x.Address.AddressText}).Take(10);
            foreach (var item in a)
            {
                sb.AppendLine(item.AddressText);
            }
            return sb.ToString();
        }
    }
}