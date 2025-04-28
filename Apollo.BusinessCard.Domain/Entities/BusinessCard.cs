using Apollo.BusinessCard.Domain.Common.Shared;

namespace Apollo.BusinessCard.Domain.Entities;

public class BusinessCard : BaseAuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int GenderId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Guid AttachmentId { get; set; }

    public virtual GenderLookup Gender { get; set; } = null!;
    public virtual BusinessCardAttachment Attachement { get; set; } = null!;
}
