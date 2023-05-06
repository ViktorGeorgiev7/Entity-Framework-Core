using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductShop;
using System.Xml;
using System.Text.Json.Serialization;
using ProductShop.DTOs.Export;
using Castle.DynamicProxy.Generators;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new ProductShopContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var usersXml = File.ReadAllText("./Datasets/users.xml");
            var categoriesXml = File.ReadAllText("./Datasets/categories.xml");
            var productsXml = File.ReadAllText("./Datasets/products.xml");
            var categoriesProductsXml = File.ReadAllText("./Datasets/categories-products.xml");
            Console.WriteLine(ImportUsers(context, usersXml));
            Console.WriteLine(ImportProducts(context, productsXml));
            Console.WriteLine(ImportCategories(context, categoriesXml));
            Console.WriteLine(ImportCategoryProducts(context, categoriesProductsXml));
            //Console.WriteLine(GetProductsInRange(context));
            //Console.WriteLine(GetSoldProducts(context));
            //Console.WriteLine(GetCategoriesByProductsCount(context));
            Console.WriteLine(GetUsersWithProducts(context));
        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(UserInputModel[]), new XmlRootAttribute("Users"));
            var textReader = new StringReader(inputXml);
            var userDto = xmlSerializer.Deserialize(textReader) as UserInputModel[];

            var users = userDto
                .Select(x => new User { FirstName = x.FirstName, LastName = x.LastName, Age = x.Age })
                .ToList();
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count()}";
        }
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ProductInputModel[]), new XmlRootAttribute("Products"));
            var textReader = new StringReader(inputXml);
            var productDto = xmlSerializer.Deserialize(textReader) as ProductInputModel[];
            var products = productDto
                .Select(x => new Product { Name = x.Name, Price = x.Price, BuyerId = x.BuyerId, SellerId = x.SellerId })
                .ToList();
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count()}";
        }
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CategoryInputModel[]), new XmlRootAttribute("Categories"));
            var textReader = new StringReader(inputXml);
            var categoryDto = xmlSerializer.Deserialize(textReader) as CategoryInputModel[];
            var categories = categoryDto.Select(x => new Category { Name = x.Name }).ToList();
            context.Categories.AddRange(categories.Where(x => x.Name != null));
            context.SaveChanges();
            return $"Successfully imported {categories.Count()}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CategoryProductsInputModel[]), new XmlRootAttribute("CategoryProducts"));
            var textReader = new StringReader(inputXml);
            var categoryProductsDto = xmlSerializer.Deserialize(textReader) as CategoryProductsInputModel[];
            var categoryProducts = categoryProductsDto
                .Select(x => new CategoryProduct { CategoryId = x.CategoryId, ProductId = x.ProductId })
                .ToList();
            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count()}";
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductsInRangeExportModel{ Name = x.Name, Price = x.Price, FullName = x.Buyer.FirstName + " " + x.Buyer.LastName })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToList();
            const string root = "ProductsInRange";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ProductsInRangeExportModel>) , new XmlRootAttribute(root));
            var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, productsInRange);
            var result = textWriter.ToString();
            return result;
        }
        public static string GetSoldProducts(ProductShopContext context) 
        {
            var soldProducts = context.Users
                .Where(x => x.ProductsSold.Count() > 0)
                .Select(x => new SoldProductsExportModel { FirstName = x.FirstName, LastName = x.LastName, soldProducts = x.ProductsSold
                .Select(x => new SoldProductsModel { Name = x.Name, Price = x.Price }).ToArray() })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToList();
            var result = XmlConverter.Serialize(soldProducts,"User");
            return result;
        }
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new CategoriesExportModel { Name = x.Name, Count = x.CategoriesProducts.Count(), AvgPrice = x.CategoriesProducts.Average(x => x.Product.Price), TotalRevenue = x.CategoriesProducts
                .Sum(x => x.Product.Price) })
                .OrderByDescending(x => x.Count).ThenBy(x => x.TotalRevenue)
                .ToList();
            var result = XmlConverter.Serialize(categories,"Category");
            return result;
        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Count() > 0)
                .OrderByDescending(x => x.ProductsSold.Count())
                .Select(x => new UserProductsExportModel { FirstName = x.FirstName, LastName = x.LastName, Age = x.Age, soldProducts = new UserSoldProductsModel { Count = x.ProductsSold.Count(), Products = x.ProductsSold
                .Select(x => new ProductsExportModel { Name = x.Name, Price = x.Price })
                .OrderByDescending(x=>x.Price)
                .ToArray() } })
                .Take(10)
                .ToList();
            var result = XmlConverter.Serialize(users,"User");
            return result;
        }
    }
}