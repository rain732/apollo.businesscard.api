using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Domain.Enums;
using FluentValidation;

namespace Apollo.BusinessCard.Application.Features.BusinessCard.Commands;

public class CreateBusinessCardCommandValidation : AbstractValidator<CreateBusinessCardCommand>
{
    private readonly IDateTimeService _dateService;
    public CreateBusinessCardCommandValidation(IDateTimeService dateService)
    {
        _dateService = dateService;

        RuleFor(x => x)
            .NotNull()
            .WithMessage("Business Card is Mandatory");

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Name is Mandatory")
            .Must(x => !string.IsNullOrWhiteSpace(x) && x.Length <= 50)
            .WithMessage("Name is Mandatory");

        RuleFor(x => x.Address)
            .NotNull()
            .WithMessage("Address is Mandatory")
            .Must(x => !string.IsNullOrWhiteSpace(x) && x.Length <= 100)
            .WithMessage("Address is Mandatory");

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Email is Mandatory")
            .Must(x => !string.IsNullOrWhiteSpace(x) && x.Length <= 50)
            .WithMessage("Email is Mandatory");

        RuleFor(x => x.Phone)
            .NotNull()
            .WithMessage("Phone is Mandatory")
            .Must(IsValidPhone)
            .WithMessage("Phone is Mandatory");

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage("Gender is Mandatory")
            .Must(x => !string.IsNullOrWhiteSpace(x) && (x.Trim().ToLower() == "male" || x.Trim().ToLower() == "female"))
            .WithMessage("Gender is Mandatory");

        RuleFor(x => x.DateOfBirth)
            .NotNull()
            .WithMessage("DateOfBirth  is Mandatory")
            .Must(x => !string.IsNullOrWhiteSpace(x) && x.Length < 50)
            .WithMessage("DateOfBirth  is Mandatory")
            .Must(IsValidDate)
            .WithMessage("DateOfBirth  is Mandatory");
    }

    private bool IsValidDate(string dateOfBirth)
    {
        if(string.IsNullOrWhiteSpace(dateOfBirth) 
            || !_dateService.IsValidConvertStringToDateTime(dateOfBirth))
            return false;

        return true;
    }

    private bool IsValidPhone(string phone)
    {
        if(string.IsNullOrWhiteSpace(phone)
            || phone.Length != 10)
            return false;
        foreach (var digit in phone)
        {
            int n;
            if (!int.TryParse(digit.ToString(), out  n))
                return false;
        }

        string[] parts = phone.Split();
        if (parts[0] != "0"
            && parts[1] != "7"
            && (parts[2] != "7" || parts[2] != "8" || parts[2] != "9"))
            return false;
        return true;
    }
}
