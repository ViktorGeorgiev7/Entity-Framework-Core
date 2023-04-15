using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._MusicHub_Database
{
    public class MusicHubContext : DbContext
    {
        public MusicHubContext()
        {
            
        }
        public MusicHubContext(DbContextOptions<MusicHubContext> options ):base(options)
        {
            
        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<SongPerformer> SongPerformers { get; set; }
        public virtual DbSet<Writer> Writers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongPerformer>().HasKey(x => new { x.SongId, x.PerformerId });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=1. MusicHub Database;TrustServerCertificate=True");
    }
}
