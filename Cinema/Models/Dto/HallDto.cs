namespace Cinema.Models.Dto
{
    public record HallDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required int BreakTime { get; set; }
        public virtual ICollection<SessionDto> Sessions { get; set; } = new List<SessionDto>();
    }
}
