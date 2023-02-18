using System.Reflection;

namespace Icarus.Presentation;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
