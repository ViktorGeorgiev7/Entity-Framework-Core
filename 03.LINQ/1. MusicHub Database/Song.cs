using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._MusicHub_Database
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        [ForeignKey("Writer")]
        public int WriterId { get; set; }
        public Writer Writer { get; set; }
        [Required]
        public double Price { get; set; }
        [InverseProperty("Song")]
        public ICollection<SongPerformer> SongPerformers { get; set; }

    }
    public enum Genre
    {
        Blues,
        Rap,
        PopMusic,
        Rock,
        Jazz
    }
}
