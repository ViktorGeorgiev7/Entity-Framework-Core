using ProductShop.Models;

namespace ProductShop.DTOs.Export
{
    public class SoldProductsExportModule
    {
        public string FirstName {get;set;}
        public string LastName { get; set; }
        public List<Product> SoldItems { get; set; }
    }
}