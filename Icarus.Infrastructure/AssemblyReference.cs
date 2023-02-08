using System.Reflection;

namespace Icarus.Infrastructure;
public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}

