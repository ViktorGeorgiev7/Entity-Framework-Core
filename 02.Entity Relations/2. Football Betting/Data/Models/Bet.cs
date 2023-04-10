using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Football_Betting
{
    public class Bet
    {
        public int BetId { get; set; }
        public double Amount { get; set; }
        public string Prediction { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Game")]
        public int GameId{ get; set; }
        public Game Game { get; set; }
    }
}
