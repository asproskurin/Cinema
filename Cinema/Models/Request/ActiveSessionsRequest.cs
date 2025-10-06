using Cinema.Models.Dto;
using FluentValidation;

namespace Cinema.Models.Request
{
    public class ActiveSessionsRequest
    {
        public List<Genre>? Genres { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinAgeLimit { get; set; }
        public int? MaxAgeLimit { get; set; }
        public int? MinDuration { get; set; }
        public int? MaxDuration { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int>? HallIds { get; set; }
    }

    public class ActiveSessionsRequestValidator : AbstractValidator<ActiveSessionsRequest>
    {
        public ActiveSessionsRequestValidator()
        {
            RuleFor(x => x.MinYear)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .When(x => x.MinYear.HasValue)
                .WithMessage($"Год выпуска должен быть между 1900 и {DateTime.Now.Year}");

            RuleFor(x => x.MaxYear)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .When(x => x.MaxYear.HasValue)
                .WithMessage($"Год выпуска должен быть между 1900 и {DateTime.Now.Year + 1}");

            RuleFor(x => x.MinAgeLimit)
                .InclusiveBetween(0, 21).When(x => x.MinAgeLimit.HasValue)
                .WithMessage("Возрастное ограничение должно быть от 0 до 21");

            RuleFor(x => x.MaxAgeLimit)
                .InclusiveBetween(0, 21).When(x => x.MaxAgeLimit.HasValue)
                .WithMessage("Возрастное ограничение должно быть от 0 до 21");

            RuleFor(x => x)
            .Must(HaveValidAgeLimitRange).WithMessage("Минимальное возрастное ограничение не может быть больше максимального");

            RuleFor(x => x.MinDuration)
                .InclusiveBetween(1, 480).When(x => x.MinDuration.HasValue)
                .WithMessage("Длительность должна быть от 1 до 480 минут");

            RuleFor(x => x.MaxDuration)
                .InclusiveBetween(1, 480).When(x => x.MaxDuration.HasValue)
                .WithMessage("Длительность должна быть от 1 до 480 минут");

            RuleFor(x => x)
                .Must(HaveValidDurationRange).WithMessage("Минимальная длительность не может быть больше максимальной");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
                .WithMessage("Цена не может быть отрицательной");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MaxPrice.HasValue)
                .WithMessage("Цена не может быть отрицательной");

            RuleFor(x => x)
                .Must(HaveValidPriceRange).WithMessage("Минимальная цена не может быть больше максимальной");

            RuleForEach(x => x.Genres)
                .Must(BeValidGenre).WithMessage("Указан недопустимый жанр")
                .When(x => x.Genres != null && x.Genres.Any());
        }

        private bool HaveValidAgeLimitRange(ActiveSessionsRequest request)
        {
            return !request.MinAgeLimit.HasValue || !request.MaxAgeLimit.HasValue ||
                   request.MinAgeLimit <= request.MaxAgeLimit;
        }

        private bool HaveValidDurationRange(ActiveSessionsRequest request)
        {
            return !request.MinDuration.HasValue || !request.MaxDuration.HasValue ||
                   request.MinDuration <= request.MaxDuration;
        }

        private bool HaveValidPriceRange(ActiveSessionsRequest request)
        {
            return !request.MinPrice.HasValue || !request.MaxPrice.HasValue ||
                   request.MinPrice <= request.MaxPrice;
        }

        private bool BeValidGenre(Genre genre)
        {
            return Enum.IsDefined(typeof(Genre), genre);
        }
    }
}
