using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ProductShop
{
    public class StartUp
    {
        static IMapper mapper;
        static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.SaveChanges();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            mapper = config.CreateMapper();
            string jsonUsers = File.ReadAllText(@"C:\Users\35988\Desktop\01. Import Users_Skeleton (ProductShop)\ProductShop\bin\Debug\net6.0\users.json");
            string jsonProducts = File.ReadAllText(@"C:\Users\35988\Desktop\01. Import Users_Skeleton (ProductShop)\ProductShop\bin\Debug\net6.0\products.json");
            string jsonCategories = File.ReadAllText(@"C:\Users\35988\Desktop\01. Import Users_Skeleton (ProductShop)\ProductShop\bin\Debug\net6.0\categories.json");
            string jsonCategoryProducts = File.ReadAllText(@"C:\Users\35988\Desktop\01. Import Users_Skeleton (ProductShop)\ProductShop\bin\Debug\net6.0\categories-products.json");
            Console.WriteLine(ImportUsers(db, jsonUsers));
            Console.WriteLine(ImportProducts(db, jsonProducts));
            Console.WriteLine(ImportCategories(db, jsonCategories));
            Console.WriteLine(ImportCategoryProducts(db, jsonCategoryProducts));
            //Console.WriteLine(GetProductsInRange(db));
            //Console.WriteLine(GetSoldProducts(db));
            //Console.WriteLine(GetCategoriesByProductsCount(db));
            Console.WriteLine(GetUsersWithProducts(db));
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var dtoUsers = JsonConvert.DeserializeObject<List<UserInputModel>>(inputJson);
            var users = mapper.Map<List<User>>(dtoUsers);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var dtoProducts = JsonConvert.DeserializeObject<List<ProductInputModel>>(inputJson);
            var products = mapper.Map<List<Product>>(dtoProducts);
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var dtoCategories = JsonConvert.DeserializeObject<List<CategoryInputModel>>(inputJson);
            var categories = mapper.Map<List<Category>>(dtoCategories);
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);
            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count()}";
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductsExport { name = x.Name, price = x.Price, seller = x.Seller.FirstName + " " + x.Seller.LastName })
                .OrderBy(x => x.price)
                .ToList();
            var result = JsonConvert.SerializeObject(productsInRange, Formatting.Indented);
            //File.WriteAllText(@"C: \Users\35988\Desktop\01.Import Users_Skeleton(ProductShop)\ProductShop\Results\products-in-range.json",result);
            return result;
        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            var products = context.Users
                .Where(x => x.ProductsSold.Count > 0)
                .Select(x => new { FirstName = x.FirstName, LastName = x.LastName, SoldProducts = x.ProductsSold.Select(x => new { name = x.Name, price = x.Price, firstname = x.Buyer.FirstName, lastname = x.Buyer.LastName }) })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();
            var result = JsonConvert.SerializeObject(products, Formatting.Indented);
            //File.WriteAllText(@"C: \Users\35988\Desktop\01.Import Users_Skeleton(ProductShop)\ProductShop\Results\users-sold-products.json",result);
            return result;
        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Where(x=>x.Name != null)
                .Select(x => new { x.Name, ProductCount = x.CategoriesProducts.Count(), AveragePrice = Math.Round(x.CategoriesProducts.Average(x => x.Product.Price), 2), Revenue = Math.Round(x.CategoriesProducts.Sum(x => x.Product.Price), 2) })
                .OrderByDescending(x=>x.ProductCount)
                .ToList();
            var result = JsonConvert.SerializeObject(categories, Formatting.Indented);
            //File.WriteAllText(@"C: \Users\35988\Desktop\01.Import Users_Skeleton(ProductShop)\ProductShop\Results\categories-by-products.json",result);

            return result;
        }
        public static string GetUsersWithProducts(ProductShopContext context) 
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Count() > 0)
                .Select(x=> new {x.FirstName,x.LastName,x.Age,Products = x.ProductsSold
                .Select(x=> new {x.Name,x.Price})
                .Where(x=>x.Name != null)})
                .OrderByDescending(x=>x.Products.Count())
                .ToList();
            var result = JsonConvert.SerializeObject (users, Formatting.Indented);
            //File.WriteAllText(@"C: \Users\35988\Desktop\01.Import Users_Skeleton(ProductShop)\ProductShop\Results\users-and-products.json",result);
            return result;
        }
    }
}