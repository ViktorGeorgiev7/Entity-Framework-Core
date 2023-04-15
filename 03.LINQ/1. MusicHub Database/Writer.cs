using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._MusicHub_Database
{
    public class Writer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public string Pseudonym  { get; set; }
        [InverseProperty("Writer")]
        public ICollection<Song> Songs { get; set; }

    }
}
