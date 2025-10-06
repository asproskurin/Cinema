using System.Xml;
using Cinema.Models.Dto;
using FluentValidation;

namespace Cinema.Models.Request
{
    public class FilmUploadRequest
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public Genre Genre { get; set; }
        public string Poster { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class FilmUploadRequestValidator : AbstractValidator<FilmUploadRequest>
    {
        public FilmUploadRequestValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Year).NotEmpty().GreaterThan(0);
            RuleFor(i => i.Duration).NotEmpty().GreaterThan(0);
            RuleFor(i => i.AgeLimit).NotEmpty().GreaterThan(0);
            RuleFor(i => i.Genre).NotEmpty();
            RuleFor(i => i.Poster).NotEmpty();
            RuleFor(i => i.IsActive).NotEmpty();
        }
    }
}
