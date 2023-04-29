using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    public class CustomerInputModel
    {
        public string name { get; set; }
        public DateTime birthdate { get; set; }
        public bool IsYoungDriver { get; set; }
    }
}
