namespace Cinema.Models.Request
{
    public class SessionUploadRequest
    {
        public required string FilmName { get; set; }
        public required string HallName { get; set; }
        public required DateTime StartTime { get; set; }
        public required int Cost { get; set; }
        public required bool Status { get; set; }
    }
}
