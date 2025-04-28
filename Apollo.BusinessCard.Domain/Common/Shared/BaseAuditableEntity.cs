namespace Apollo.BusinessCard.Domain.Common.Shared;

public class BaseAuditableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
