using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop
{
    [XmlType("Category")]
    public class CategoriesExportModel
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlElement("avgPrice")]
        public decimal AvgPrice { get; set; }
        [XmlElement("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }
}
