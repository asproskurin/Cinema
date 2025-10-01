namespace Cinema.Models.Request
{
    public class HallUploadRequest
    {
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required int BreakTime { get; set; }
    }
}
