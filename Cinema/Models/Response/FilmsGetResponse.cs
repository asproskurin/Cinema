using System.ComponentModel.DataAnnotations.Schema;
using Cinema.Models.Dto;

namespace Cinema.Models.Response
{
    public class FilmsGetResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public string? Genre { get; set; }
        public string? Poster { get; set; }
    }
}
