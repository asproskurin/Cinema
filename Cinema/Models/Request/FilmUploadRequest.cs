using System.Xml;
using Cinema.Models.Dto;

namespace Cinema.Models.Request
{
    public class FilmUploadRequest
    {
        public required string Name { get; set; }
        public required int Year { get; set; }
        public required int Duration { get; set; }
        public required int AgeLimit { get; set; }
        public required Genre Genre { get; set; }
        public required string Poster { get; set; }
        public required bool IsActive { get; set; } = true;
    }
}
