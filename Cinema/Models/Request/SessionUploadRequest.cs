namespace Cinema.Models.Request
{
    public class SessionUploadRequest
    {
        public required string Film { get; set; }
        public required string Hall { get; set; }
        public required DateTime StartTime { get; set; }
        public required int Cost { get; set; }
        public required bool Status { get; set; }
    }
}
