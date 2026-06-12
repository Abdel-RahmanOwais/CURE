using CURE.Application.DTOs.Nurses;
using FluentValidation;

namespace CURE.Application.Validators.Nurse
{
    public class UpdateNurseValidator : AbstractValidator<UpdateNurseDto>
    {

        public UpdateNurseValidator()
        {
            RuleFor(x => x.Specialization)
                .MaximumLength(100)
                .WithMessage("Specialization must not exceed 100 characters.");

            RuleFor(x => x.LicenseNumber)
                .MaximumLength(50)
                .WithMessage("License number must not exceed 50 characters.");

            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Years of experience must be a non-negative value.");
        }
    }
}
