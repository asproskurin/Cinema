using Cinema.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models.Response
{
    public class SessionsGetResponse
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
    }
}
