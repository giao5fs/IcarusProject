namespace Icarus.Domain.Abtractions;

public interface ISoftDeletableEntity
{
    DateTime? DeletedOnUtc { get; }
    bool Deleted { get; }
}
