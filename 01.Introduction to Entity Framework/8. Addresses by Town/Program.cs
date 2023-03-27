using _8._Addresses_by_Town.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _8._Addresses_by_Town
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetAddressesByTown(context));
        }
        public static string GetAddressesByTown(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();
            var list = context.Addresses.Include(x => x.Employees).OrderByDescending(x=>x.Employees.Count()).ThenBy(x=>x.Town.Name).ThenBy(x=>x.AddressText).Take(10).ToList();
            foreach (var address in list) 
            {
                sb.AppendLine($"{address.AddressText} - {address.Employees.Count()}");
            }
            return sb.ToString();
        }
    }
}