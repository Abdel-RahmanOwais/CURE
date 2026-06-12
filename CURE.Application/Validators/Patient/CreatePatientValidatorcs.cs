using CURE.Application.DTOs.Patients;
using FluentValidation;

namespace CURE.Application.Validators.Patient
{
    public class CreatePatientValidatorcs : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientValidatorcs()
        {
            RuleFor(x => x.UserId)
             .GreaterThan(0).WithMessage("You must enter id greater than 0.");

            RuleFor(x => x.BloodType)
                .NotEmpty().WithMessage("Blood Type Is Required.")
                .MaximumLength(5);

            RuleFor(x => x.EmergencyContact)
                .NotEmpty().WithMessage("Emergancy Contact Is Required.")
                .MaximumLength(50);
        }
    }
}
