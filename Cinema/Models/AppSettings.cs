namespace Cinema.Models
{
    public record HallSettings
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
