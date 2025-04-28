using Apollo.BusinessCard.Domain.Common.Shared;

namespace Apollo.BusinessCard.Domain.Entities;

public class BusinessCardAttachment : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string Base64File { get; set; } = null!;
}
