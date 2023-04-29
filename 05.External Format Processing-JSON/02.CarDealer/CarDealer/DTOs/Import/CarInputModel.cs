using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Import
{
    public class CarInputModel
    {
        public string make { get; set; }
        public string model { get; set; }
        public int traveledDistance { get; set; }
        public IEnumerable<PartCar> parts { get; set; }
    }
}
