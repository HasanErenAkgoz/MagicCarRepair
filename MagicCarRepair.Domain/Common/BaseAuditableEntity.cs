using Core.Packages.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity<Guid>, IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
} 