using _14._Delete_Project_by_Id.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace _14._Delete_Project_by_Id
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext db = new SoftUniContext();
            Console.WriteLine(DeleteProjectById(db));
        }
        public static string DeleteProjectById(SoftUniContext context) 
        {
           
            var projectemps = context.Projects.Include(x => x.Employees).Where(x => x.ProjectId == 2);
            var project = context.Projects.Find(2);
            foreach (var item in project.Employees)
            {
                item.Projects.Remove(project);
            }
            context.Projects.Remove(project);
            context.SaveChanges();
            var list = context.Projects.Take(10).ToList();
            StringBuilder sb  = new StringBuilder();
            foreach (var item in list) {sb.AppendLine(item.Name); }
            return sb.ToString();
        }
    }
}