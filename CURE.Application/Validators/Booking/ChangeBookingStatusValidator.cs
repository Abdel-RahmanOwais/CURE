using CURE.Application.DTOs.Booking;
using FluentValidation;

namespace CURE.Application.Validators.Booking
{
    public class ChangeBookingStatusValidator : AbstractValidator<ChangeBookingStatusDto>
    {
        public ChangeBookingStatusValidator()
        {
            RuleFor(x => x.StatusId)
                .InclusiveBetween(1, 4)
                .WithMessage("Status ID must be between 1 and 4.");
        }
    }
}
