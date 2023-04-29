using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Castle.DynamicProxy.Generators;
using Newtonsoft.Json;
using System.IO;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = config.CreateMapper();
            string jsonSuppliers = File.ReadAllText(@"C:\Users\35988\Desktop\02.Import_Car_Skeleton\CarDealer\Datasets\suppliers.json");
            string jsonParts = File.ReadAllText(@"C:\Users\35988\Desktop\02.Import_Car_Skeleton\CarDealer\Datasets\parts.json");
            string jsonCars = File.ReadAllText(@"C:\Users\35988\Desktop\02.Import_Car_Skeleton\CarDealer\Datasets\cars.json");
            string jsonCustomers = File.ReadAllText(@"C:\Users\35988\Desktop\02.Import_Car_Skeleton\CarDealer\Datasets\customers.json");
            string jsonSales = File.ReadAllText(@"C:\Users\35988\Desktop\02.Import_Car_Skeleton\CarDealer\Datasets\sales.json");
            Console.WriteLine(ImportSuppliers(db,jsonSuppliers));
            Console.WriteLine(ImportParts(db, jsonParts));
            Console.WriteLine(ImportCars(db,jsonCars));
            Console.WriteLine(ImportCustomers(db,jsonCustomers));
            Console.WriteLine(ImportSales(db,jsonSales));
            //Console.WriteLine(GetOrderedCustomers(db));
            //Console.WriteLine(GetCarsFromMakeToyota(db));
            //Console.WriteLine(GetCarsWithTheirListOfParts(db));
            //Console.WriteLine(GetTotalSalesByCustomer(db));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var dtoSuppliers = JsonConvert.DeserializeObject<IEnumerable<SuppliersInputModel>>(inputJson);
            var suppliers = mapper.Map<IEnumerable<Supplier>>(dtoSuppliers);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count()}";
        }
        public static string ImportParts(CarDealerContext context, string inputJson) 
        {
            var dtoParts = JsonConvert.DeserializeObject<IEnumerable<PartsInputModel>>(inputJson);
            var parts = mapper.Map<IEnumerable<Part>>(dtoParts).Where(x=>x.SupplierId < 32);
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count()}";
        }
        public static string ImportCars(CarDealerContext context, string inputJson) 
        {
            var dtoCars = JsonConvert.DeserializeObject<IEnumerable<Class1>>(inputJson);
            var cars = mapper.Map<IEnumerable<Car>>(dtoCars);
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count()}";
        }
        public static string ImportCustomers(CarDealerContext context, string inputJson) 
        {
            var dtoCustomers = JsonConvert.DeserializeObject<IEnumerable<CustomerInputModel>>(inputJson);
            var customers = mapper.Map<IEnumerable<Customer>>(dtoCustomers);
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count()}";
        }
        public static string ImportSales(CarDealerContext context, string inputJson) 
        {
            var dtoSales = JsonConvert.DeserializeObject<IEnumerable<SalesInputModel>>(inputJson);
            var sales = mapper.Map<IEnumerable<Sale>>(dtoSales);
            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count()}";
        }
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers.Select(x => new CustomerInputModel {name= x.Name,birthdate=x.BirthDate,IsYoungDriver = x.IsYoungDriver }).OrderBy(x=>x.birthdate).ThenBy(x=>x.IsYoungDriver).ToList();
            var json = JsonConvert.SerializeObject(customers,Formatting.Indented);
            File.WriteAllText("/.././customers.json",json);
            return json;
        }
        public static string GetCarsFromMakeToyota(CarDealerContext context) 
        {
            var cars = context.Cars
                .Select(x=> new {id =x.Id, make = "Toyota",model = x.Model,travelledDistance = x.TravelledDistance})
                .Where(x => x.make == "Toyota")
                .OrderBy(x=>x.model)
                .ThenByDescending(x=>x.travelledDistance)
                .ToList();
            var json = JsonConvert.SerializeObject (cars,Formatting.Indented);
            //File.WriteAllText("/.././cars.json", json);
            return json;
        }
        public static string GetLocalSuppliers(CarDealerContext context) 
        {
            var suppliers = context.Suppliers.Select(x=> new SuppliersExportModel{Id =x.Id,Name = x.Name, PartsCount = x.Parts.Count()}).ToList();
            var json = JsonConvert.SerializeObject(suppliers,Formatting.Indented);
            //File.WriteAllText("/.././suppliers.json", json);
            return json;
        }
        public static string GetCarsWithTheirListOfParts(CarDealerContext context) 
        {
            var cars = context.Cars
                .Select(x => new { x.Make, x.Model, x.TravelledDistance, parts = x.PartsCars.Select(x => new { x.Part.Name, x.Part.Price }) }).ToArray();
            var json = JsonConvert.SerializeObject((cars),Formatting.Indented);
            //File.WriteAllText("/.././carPartList.json", json);
            return json;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context) 
        {
            var customers = context.Customers.Where(x => x.Sales.Count() > 0).Select(x => new { x.Name, boughtCars = x.Sales.Count() }).ToList() ;
            var json = JsonConvert.SerializeObject(customers,Formatting.Indented);
            //File.WriteAllText("/.././customerTotalSales.json", json);
            return json;
        }

    }
}