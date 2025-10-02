namespace Cinema.Models.Request
{
    public class SessionUploadRequest
    {
        public required string FilmName { get; set; }
        public required string HallName { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required DateOnly StartDate { get; set; }
        public required int Price { get; set; }
        public required bool Status { get; set; }
    }
}
