using Icarus.Domain.Exceptions;
using System.Reflection;

namespace Icarus.Domain.Primitives;

public abstract class Enumeration : IComparable
{
    public int Value { get; private set; }
    public string Name { get; private set; }
    protected Enumeration()
    {
        Value = default;
        Name = string.Empty;
    }

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
        T matchingItem = GetAll<T>().First(item => item.Value == value);
        return matchingItem;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        Type type = typeof(T);

        object? o = Activator.CreateInstance(type, true);

        if (o is null)
        {
            throw new EnumerationInvalidException(type);
        }

        FieldInfo[] fields = type.GetFields(
            BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (FieldInfo info in fields)
        {
            var instance = (T)o;

            if (info.GetValue(instance) is T locatedValue)
            {
                yield return locatedValue;
            }
        }
    }

    public int CompareTo(object? other)
    {
        return other is null ? 1 : Value.CompareTo(((Enumeration)other).Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        return GetType() == obj.GetType() && otherValue.Value.Equals(Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}
