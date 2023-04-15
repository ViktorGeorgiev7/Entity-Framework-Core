using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._MusicHub_Database
{
    public class SongPerformer
    {//do in onconfiguring compo key
        [ForeignKey("Song")]
        public int SongId { get; set; }
        [ForeignKey("Performer")]
        public int PerformerId { get; set; }
        [Required]
        public Song Song { get; set; }
        [Required]
        public Performer Performer { get; set; }
    }
}
