namespace BookShop
{
    using BookShop.Models;
    using Data;
    using Initializer;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            
            var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            Console.WriteLine(GetTotalProfitByCategory(db));
            db.SaveChanges();
        }
        //02.
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var booksSorted = context.Books.OrderBy(x => x.Title).Select(x => new { x.Title, x.AgeRestriction }).ToList();
            var booksFiltered = booksSorted.Where(x => x.AgeRestriction.ToString().ToLower() == command.ToLower());
            StringBuilder sb = new StringBuilder();
            foreach (var book in booksFiltered)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString();
        }
        //03.
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Copies < 5000).Select(x => new { x.EditionType, x.Title, x.BookId }).ToList();
            var booksGolden = books.Where(x => x.EditionType.ToString() == "Gold").OrderBy(x => x.BookId);
            StringBuilder sb = new StringBuilder();
            foreach (var book in booksGolden)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString();
        }
        //04.
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Price > 40).Select(x => new { x.Title, x.Price }).OrderByDescending(x => x.Price).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title + " - " + book.Price);
            }
            return sb.ToString();
        }
        //05.
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year != year).Select(x => new { x.Title, x.BookId }).OrderBy(x => x.BookId).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString();
        }
        //06.
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> bookTitles = new List<string>();
            foreach (var category in categories) {
                List<string> books = context.Books.Where(x => x.BookCategories.Any(x => x.Category.ToString().ToLower() == category.ToLower())).Select(x => x.Title).ToList();
                bookTitles.AddRange(books);
            }
            return string.Join(Environment.NewLine, bookTitles);
        }
        //07.
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime release = DateTime.Parse(date);
            var books = context.Books
            .Where(x => x.ReleaseDate.HasValue && x.ReleaseDate.Value < release)
            .Select(x => new { x.Title, x.EditionType, x.Price, x.ReleaseDate })
            .OrderByDescending(x => x.ReleaseDate)
            .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var x in books) { sb.AppendLine($"{x.Title} - {x.EditionType.ToString()} - {x.Price} - " + x.ReleaseDate); }
            return sb.ToString();
        }
        //08.
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors.Where(x => x.FirstName.EndsWith(input)).Select(x => new { FullName = x.FirstName + " " + x.LastName }).OrderBy(x => x.FullName).ToList();
            return string.Join(Environment.NewLine, authors);
        }
        //09.
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books.Where(x => x.Title.ToLower().Contains(input.ToLower())).Select(x => new { x.Title }).OrderBy(x => x.Title).ToList();
            return string.Join(Environment.NewLine, books);
        }
        //010.
        public static string GetBooksByAuthor(BookShopContext context, string input) 
        {
            var books = context.Books.Where(x => x.Author.LastName.StartsWith(input)).Select(x => new { x.BookId, FullBookName = x.Title + "  (" +x.Author.FirstName + " " + x.Author.LastName +")"}).OrderBy(x=>x.BookId).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in books)
            {
                sb.AppendLine(item.FullBookName);
            }
            return sb.ToString();
        }
        //011.
        public static int CountBooks(BookShopContext context, int lengthCheck) 
        {
            return context.Books.Where(x=>x.Title.Length>lengthCheck).Count();
        }
        //012.
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors.Select(x => new { FullName = x.FirstName + " " + x.LastName + " - ", Copies = x.Books.Sum(x => x.Copies) }).ToList();
            StringBuilder sb= new StringBuilder();
            foreach (var item in authors)
            {
                sb.AppendLine(item.FullName + item.Copies);
            }
            return sb.ToString();
        }
        //013.
        public static string GetTotalProfitByCategory(BookShopContext context) 
        {
            var categories = context.Books.GroupBy(x=>x.BookCategories).Select(x=> new {Category= x.Key , Profit = x.Sum(x=>x.Copies*x.Price)}).OrderByDescending(g => g.Profit)
            .ThenBy(g => g.Category)
            .ToList();
            return string.Join(", ", categories);
        }
        //014.
        public static string GetMostRecentBooks(BookShopContext context) 
        {
            var books = context.Books.OrderByDescending(x=>x.ReleaseDate).ToList();
            var categories = context.Categories.Select(x=> new {CategoryName = x.Name,MostRecentBooks = x.CategoryBooks.OrderByDescending(x=>x.Book.ReleaseDate).Take(3).Select(x=> new {x.Book.Title,x.Book.ReleaseDate.Value.Year})}).ToList();
            return string.Join (", ", categories);
        }
        //015.
        public static void IncreasePrices(BookShopContext context) 
        {
            var booksBefore2005 = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010).ToList();
            foreach (var item in context.Books)
            {
                foreach (var book in booksBefore2005)
                {
                    if (item.BookId == book.BookId)
                    {
                        item.Price += 5;
                    }
                }
            }
            
        }
        //016.
        public static int RemoveBooks(BookShopContext context) 
        {
            var count = context.Books.Where(x => x.Copies < 4200).ToList();

            context.Books.RemoveRange(count);
            return count.Count;
        }
    }
}


