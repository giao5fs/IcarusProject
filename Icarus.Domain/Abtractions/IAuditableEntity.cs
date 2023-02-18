namespace Icarus.Domain.Abtractions;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
}
