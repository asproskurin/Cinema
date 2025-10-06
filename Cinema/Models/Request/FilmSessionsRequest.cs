using FluentValidation;

namespace Cinema.Models.Request
{
    public class FilmSessionsRequest
    {
        public int FilmId { get; set; }
        public bool? OnlyActive { get; set; } = true;
    }
    public class FilmSessionsRequestValidator : AbstractValidator<FilmSessionsRequest>
    {
        public FilmSessionsRequestValidator()
        {
            RuleFor(x => x.FilmId)
                .GreaterThan(0).WithMessage("ID фильма должен быть положительным числом");
        }
    }
}
