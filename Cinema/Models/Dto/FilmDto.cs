using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models.Dto
{
    public record FilmDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Year { get; set; }
        public required int Duration { get; set; }
        public required int AgeLimit { get; set; }

        [NotMapped]
        public Genre Genre { get; set; }
        public required string GenreString
        {
            get => Genre.ToString();
            set => Genre = Enum.Parse<Genre>(value);
        }
        public required byte[] Poster { get; set; }
        public required bool IsActive { get; set; } = true;

        public virtual ICollection<SessionDto> Sessions { get; set; } = new List<SessionDto>();
    }

    public enum Genre
    {
        Комедия,
        Боевик,
        Ужастик
    }

}
