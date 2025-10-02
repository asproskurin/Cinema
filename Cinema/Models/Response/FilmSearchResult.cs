using Cinema.Models.Dto;

namespace Cinema.Models.Response
{
    public class FilmSearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public string GenreString { get; set; }
        public Genre Genre { get; set; }
        public byte[] Poster { get; set; }
        public bool IsActive { get; set; }
        public NearestSession NearestSession { get; set; }
    }

    public class NearestSession
    {
        public int SessionId { get; set; }
        public TimeOnly StartTime { get; set; }
        public string HallName { get; set; }
        public decimal Price { get; set; }
        public int MinutesUntilStart { get; set; }
    }
}
