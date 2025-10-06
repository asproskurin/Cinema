using FluentValidation;

namespace Cinema.Models.Request
{
    public class SessionUploadRequest
    {
        public string FilmName { get; set; }
        public string HallName { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly StartDate { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
    }

    public class SessionUploadRequestValidator : AbstractValidator<SessionUploadRequest>
    {
        public SessionUploadRequestValidator()
        {
            RuleFor(i => i.FilmName).NotEmpty();
            RuleFor(i => i.HallName).NotEmpty();
            RuleFor(i => i.StartTime).NotEmpty();
            RuleFor(i => i.StartDate).NotEmpty();
            RuleFor(i => i.Price).NotEmpty();
            RuleFor(i => i.Status).NotEmpty();
        }
    }
}
