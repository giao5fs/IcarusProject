using Icarus.Domain.Exceptions;
using System.Reflection;

namespace Icarus.Domain.Primitives;

public abstract class Enumeration<T> : IEquatable<T>
    where T : Enumeration<T>
{
    private static readonly Dictionary<int, T> Enumerations = CreateEnumerations();
    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; protected init; }
    public string Name { get; protected init; }
    public static T[] GetValues()
    {
        return Enumerations.Values.ToArray();
    }
    public static T? FromValue(int value)
    {
        return Enumerations.TryGetValue(value, out T? result) ? result : default(T?);
    }

    public static T? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(x => x.Name == name);
    }
    public bool Equals(T? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<T> other &&
            Equals(other);
    }

    private static Dictionary<int, T> CreateEnumerations()
    {
        var enumerationType = typeof(T);

        var fieldsForType = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => enumerationType.IsAssignableFrom(f.FieldType))
            .Select(f => (T)f.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }
}
