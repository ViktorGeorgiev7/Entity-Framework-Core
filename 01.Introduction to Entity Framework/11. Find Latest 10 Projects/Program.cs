using _11._Find_Latest_10_Projects.Data.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace _11._Find_Latest_10_Projects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetLatestProjects(context));
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            var list = context.Projects.OrderByDescending(x => x.StartDate).OrderBy(x => x.Name).Take(10).Select(x => new { x.Name, x.Description, x.StartDate }).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var project in list) 
            {
                sb.AppendLine(project.Name);
                sb.AppendLine(project.Description);
                sb.AppendLine(project.StartDate.ToString());
            }
            return sb.ToString();

        }
    }
}