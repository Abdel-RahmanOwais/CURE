using CURE.Application.DTOs.Nurses;
using FluentValidation;

namespace CURE.Application.Validators.Nurse
{
    public class CreateNurseValidator : AbstractValidator<CreateNurseDto>
    {
        public CreateNurseValidator()
        {

            //RuleFor(x => x.UserId)
            //    .NotEmpty()
            //    .WithMessage("User ID is required.")
            //    .GreaterThan(0)
            //    .WithMessage("User ID must be a Greater Than Zero");

            RuleFor(x => x.Specialization)
                .NotEmpty()
                .WithMessage("Specialization is required.")
                .MaximumLength(100)
                .WithMessage("Specialization must not exceed 100 characters.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .WithMessage("License number is required.")
                .MaximumLength(50)
                .WithMessage("License number must not exceed 50 characters.");

            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Years of experience must be a non-negative value.");
        }
    }
}
