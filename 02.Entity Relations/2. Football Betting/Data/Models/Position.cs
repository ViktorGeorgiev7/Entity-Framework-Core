using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2._Football_Betting
{
    public class Position
    {
        public Position()
        {
            Players = new HashSet<Player>();
        }
        [Key]
        public int PositionId { get; set; }
        public bool Name { get; set; }
        [InverseProperty("Position")]
        public ICollection<Player> Players { get; set; }
    }
}
