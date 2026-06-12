using CURE.Application.DTOs.Patients;
using FluentValidation;

namespace CURE.Application.Validators.Patient
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
    {
        public UpdatePatientValidator()
        {

            RuleFor(x => x.BloodType)
                .NotEmpty().WithMessage("Blood Type Is Required.")
                .MaximumLength(5);

            RuleFor(x => x.EmergencyContact)
                .NotEmpty().WithMessage("Emergancy Contact Is Required.")
                .MaximumLength(50);
        }

    }

}
