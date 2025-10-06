using FluentValidation;

namespace Cinema.Models.Request
{
    public class HallUploadRequest
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int BreakTime { get; set; }
    }

    public class HallUploadRequestValidator : AbstractValidator<HallUploadRequest>
    {
        public HallUploadRequestValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Quantity).NotEmpty().GreaterThan(0);
            RuleFor(i => i.BreakTime).NotEmpty().GreaterThan(0);
        }
    }
}
