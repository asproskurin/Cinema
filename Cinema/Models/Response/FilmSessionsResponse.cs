using Cinema.Models.Dto;

namespace Cinema.Models.Response
{
    public class FilmSessionsResponse
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public string GenreString { get; set; }
        public byte[] Poster { get; set; }
        public string Description { get; set; }
        public List<SessionDto> Sessions { get; set; } = new();
    }
}
