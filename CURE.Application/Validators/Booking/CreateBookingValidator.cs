using CURE.Application.DTOs.Booking;
using FluentValidation;

namespace CURE.Application.Validators.Booking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator()
        {
            //RuleFor(x => x.PatientId)
            //    .GreaterThanOrEqualTo(1)
            //    .WithMessage("Patient ID must be a positive integer.");

            RuleFor(x => x.NurseId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Nurse ID must be a positive integer.");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Appointment date must be in the future.");
        }

    }
}
