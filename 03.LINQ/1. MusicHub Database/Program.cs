using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace _1._MusicHub_Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MusicHubContext context = new MusicHubContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        static public void ExportAlbumsInfo(MusicHubContext context, int producerId) 
        {
            foreach (var album in context.Albums.Where(x=>x.ProducerId == producerId).Select(x=>x)) 
            { 
                Console.WriteLine($"-AlbumName: " + album.Name );
                Console.WriteLine($"-ReleaseDate: " + string.Format("MM/dd/yyyy",album.ReleaseDate));
                Console.WriteLine($"-Producer Name: " + album.Producer.Name);
                Console.WriteLine($"-Songs: ");
                foreach (var item in album.Songs)
                {
                    var currSong = item;

                Console.WriteLine($"---#" + currSong.Id);
                Console.WriteLine($"---SongName: " + currSong.Name);
                Console.WriteLine($"---Price: " + currSong.Price);
                Console.WriteLine($"---Writer: " + currSong.Writer.Name);
                }
            Console.WriteLine($"-Album: " + album.Price);
            }
        }
        public static string ExportSongsAboveDuration(MusicHubContext context, int duration)
        {
            var songs = context.Songs.Where(x => x.Duration.TotalSeconds > duration).OrderBy(x=>x.Name).ThenBy(x=>x.Writer.Name);
            StringBuilder sb = new StringBuilder();
            foreach (var song in songs) 
            {
                //-Song #1
                //---SongName: Away
                //---Writer: Norina Renihan
                //---Performer: Lula Zuan
                //---AlbumProducer: Georgi Milkov
                //---Duration: 00:05:35
                sb.AppendLine($"-Song #" + song.Id);
                sb.AppendLine($"---SongName: " + song.Name);
                sb.AppendLine($"---Writer: " + song.Writer.Name);
                if (song.SongPerformers.Count>0)
                {
                    sb.AppendLine($"---Performer: " + string.Join(", ",song.SongPerformers.OrderBy(x=>x.Performer.FirstName)));
                }
                sb.AppendLine($"---AlbumProducer: " + song.Album.Producer.Name);
                sb.AppendLine($"---Duration: " + string.Format("c",song.Duration));
            }
            return sb.ToString();
        }
    }
}