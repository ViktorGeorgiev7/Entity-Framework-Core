using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    public class SalesInputModel
    {
        public int carId { get; set; }
        public int customerId { get; set; }
        public int discount { get; set; }
    }
}
