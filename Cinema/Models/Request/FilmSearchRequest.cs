using Cinema.Models.Dto;
using FluentValidation;

namespace Cinema.Models.Request
{
    public class FilmSearchRequest
    {
        public string Name { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinAgeLimit { get; set; }
        public int? MaxAgeLimit { get; set; }
        public int? MinDuration { get; set; }
        public int? MaxDuration { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public TimeOnly? FromTime { get; set; }
        public bool? IsActive { get; set; } = true;
    }

    public class FilmSearchRequestValidator : AbstractValidator<FilmSearchRequest>
    {
        public FilmSearchRequestValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
        }
    }
}
