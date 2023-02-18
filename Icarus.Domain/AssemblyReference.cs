using System.Reflection;

namespace Icarus.Domain;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}