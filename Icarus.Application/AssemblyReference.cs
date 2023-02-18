using System.Reflection;

namespace Icarus.Application;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
