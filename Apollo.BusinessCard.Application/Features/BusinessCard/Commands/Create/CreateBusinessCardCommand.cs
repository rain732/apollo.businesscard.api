using Apollo.BusinessCard.Application.Common.DTOs;
using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Application.Common.Shared;
using Apollo.BusinessCard.Domain.Entities;
using Apollo.BusinessCard.Domain.Enums;
using Apollo.BusinessCard.Domain.ValueObjects;
using MediatR;

namespace Apollo.BusinessCard.Application.Features.BusinessCard.Commands;

public record CreateBusinessCardCommand : IRequest<Result<Unit>>
{
    public string Name { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Gender { get; set; }
    public string Photo { get; set; } = null!;
}

public sealed class CreateBusinessCardHandler : IRequestHandler<CreateBusinessCardCommand, Result<Unit>>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateService;
    private readonly IBase64Service _base64Service;
    private readonly Guid _userId = Guid.NewGuid();

    public CreateBusinessCardHandler(IApplicationDbContext context, IDateTimeService dateService, IBase64Service base64Service)
    {
        _context = context;
        _dateService = dateService;
        _base64Service = base64Service;
    }

    public async Task<Result<Unit>> Handle(CreateBusinessCardCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var attachment = GetAttachment(request, now);
        await _context.BusinessCardAttachment.AddAsync(attachment);

        int genderId = GetGenderId(request.Gender);
        if (genderId == default)
            return Result<Unit>.Failure(Localization.ERROR_GENDER_NOT_FOUND);

        var card = GetBusinessCardEntity(request, attachment.Id, genderId, now);

        await _context.BusinessCard.AddAsync(card, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }

    private int GetGenderId(string gender)
    {
        if (string.IsNullOrWhiteSpace(gender))
            return default;
        gender = gender.Trim().ToLower();
        switch (gender)
        {
            case "male":
                return (int)GenderEnum.Male;
            case "female":
                return (int)GenderEnum.Female;
            default:
                return default;
        }
    }

    private Domain.Entities.BusinessCard GetBusinessCardEntity(CreateBusinessCardCommand cardModel,Guid attachmentId, int genderId, DateTime now)
    {
        var dateOfBirth = _dateService.ConvertStringToDateTime(cardModel.DateOfBirth);
        return new Domain.Entities.BusinessCard()
        {
            Name = cardModel.Name.Trim(),
            DateOfBirth = dateOfBirth.Value.Date,
            Email = cardModel.Email.Trim(),
            Address = cardModel.Address.Trim(),
            Phone = cardModel.Phone.Trim(),
            GenderId = genderId,
            AttachmentId = attachmentId,
            Created = now,
            CreatedBy = _userId //Assuming a user exists
        };
    }
    private BusinessCardAttachment GetAttachment(CreateBusinessCardCommand command, DateTime now)
    {
        var attachment = new Domain.Entities.BusinessCardAttachment()
        {
            Id = Guid.NewGuid(),
            Base64File = command.Photo.Trim(),
            Created = now,
            CreatedBy = Guid.NewGuid(), // Assuming a user exists
        };
        return attachment;
    }
}
