namespace Icarus.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
}
