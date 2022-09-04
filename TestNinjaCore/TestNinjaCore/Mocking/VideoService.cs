using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestNinjaCore.Mocking.VideoServiceExample;

namespace TestNinjaCore.Mocking
{
    public class VideoService
    {
        private readonly IFileReader _fileReader;
        private readonly IConverter<Video> _converter;

        public VideoService(IFileReader fileReader, IConverter<Video> converter)
        {
            _fileReader = fileReader;
            _converter = converter;
        }

        public string ReadVideoTitle(string path)
        {
            var str = _fileReader.ReadFileContent(path);
            var video = _converter.Deserialize(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            
            using (var context = new VideoContext())
            {
                var videos = 
                    (from video in context.Videos
                    where !video.IsProcessed
                    select video).ToList();
                
                foreach (var v in videos)
                    videoIds.Add(v.Id);

                return String.Join(",", videoIds);
            }
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}