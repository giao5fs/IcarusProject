using System.Reflection;

namespace Icarus.Persistence;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
