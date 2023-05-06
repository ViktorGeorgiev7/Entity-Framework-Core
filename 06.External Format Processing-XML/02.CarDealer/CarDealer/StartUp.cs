using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main()
        {
            var context = new CarDealerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });
            mapper = config.CreateMapper();
            var carsXml = File.ReadAllText(@"C:\Users\35988\Desktop\02.CarDealer\CarDealer\Datasets\cars.xml");
            var customersXml = File.ReadAllText(@"C:\Users\35988\Desktop\02.CarDealer\CarDealer\Datasets\customers.xml");
            var partsXml = File.ReadAllText(@"C:\Users\35988\Desktop\02.CarDealer\CarDealer\Datasets\parts.xml");
            var salesXml = File.ReadAllText(@"C:\Users\35988\Desktop\02.CarDealer\CarDealer\Datasets\sales.xml");
            var suppliersXml = File.ReadAllText(@"C:\Users\35988\Desktop\02.CarDealer\CarDealer\Datasets\suppliers.xml");
            Console.WriteLine(ImportSuppliers(context,suppliersXml));
            Console.WriteLine(ImportParts(context,partsXml));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var suppliersDto = XmlConverter.Deserializer<SupplierInputModel>(inputXml, "Suppliers");
            var suppliers = mapper.Map<List<Supplier>>(suppliersDto);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count()}";
        }
        public static string ImportParts(CarDealerContext context, string inputXml) 
        {
            var partDto = XmlConverter.Deserializer<PartInputModel>(inputXml,"Parts");
            var parts = mapper.Map<List<Part>>(partDto);
            foreach (var supplier in context.Suppliers)
            {
                foreach (var part in parts)
                {
                    if (supplier.Id == part.Id)
                    {
                        context.Parts.Add(part);
                    }
                }
            }
            context.SaveChanges();
            return $"Successfully imported {parts.Count()}";
        }       
    }
}