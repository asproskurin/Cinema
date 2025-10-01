using Cinema.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models.Response
{
    public class SessionsGetResponce
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required DateOnly StartDate { get; set; }
        public required int Duration { get; set; }
        public required int Cost { get; set; }
        public required bool Status { get; set; }
    }
}
