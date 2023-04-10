using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2._Football_Betting
{
    public class Team
    {
        public Team()
        {
            HomeGames = new HashSet<Game>();
            AwayGames = new HashSet<Game>();
            Players = new HashSet<Player>();
        }

        [Key]
        public int TeamId { get; set; }

        public string Name { get; set; }
        public string LogoUrl { get; set; }

        [MaxLength(3)]
        public string Initials { get; set; }

        public double Budget { get; set; }
        public int PrimaryKitColorId { get; set; }

        [ForeignKey("PrimaryKitColorId")]
        [InverseProperty("PrimaryKitTeams")]
        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }
        [ForeignKey("SecondaryKitColorId")]
        [InverseProperty("SecondaryKitTeams")]
        public Color SecondaryKitColor { get; set; }

        [ForeignKey("TownId")]
        public Town Town { get; set; }
        [InverseProperty("HomeTeam")]
        public ICollection<Game> HomeGames { get; set; }
        [InverseProperty("AwayTeam")]
        public ICollection<Game> AwayGames { get; set; }
        [InverseProperty("Team")]
        public ICollection<Player> Players { get; set; }
    }

}
