using _15._Remove_Town.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace _15._Remove_Town
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext();
            Console.WriteLine(RemoveTown(db));
        }
        public static string RemoveTown(SoftUniContext context) 
        {
            var town = context.Towns.Include(x=>x.Addresses).Where(x => x.Name == "Seattle").First();
            int count = 0;
            foreach (var address in town.Addresses) 
            {
                count++;
                town.Addresses.Remove(address);
            }
            context.Remove(town);
            context.SaveChanges();
            return count + " addresses in Seattle were deleted";
        }
    }
}