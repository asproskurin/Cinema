namespace Cinema.Models.Response
{
    public class ActiveSessionsResponse
    {
        public int TotalFilms { get; set; }
        public int TotalSessions { get; set; }
        public List<FilmSessionsGroup> Films { get; set; } = new();
    }

    public class FilmSessionsGroup
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public string GenreString { get; set; }
        public byte[] Poster { get; set; }
        public string Description { get; set; }
        public TimeOnly FirstSessionTime { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<SessionShortInfo> Sessions { get; set; } = new();
    }

    public class SessionShortInfo
    {
        public int SessionId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string HallName { get; set; }
        public int HallId { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public int TotalSeats { get; set; }
    }
}
