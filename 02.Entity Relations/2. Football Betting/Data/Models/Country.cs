using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2._Football_Betting
{
    public class Country
    {
        public Country()
        {
            Towns = new HashSet<Town>();
        }
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        [InverseProperty("Country")]
        public ICollection<Town> Towns { get; set; }
    }
}
