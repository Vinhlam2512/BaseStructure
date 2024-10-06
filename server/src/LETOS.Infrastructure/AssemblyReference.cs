using System.Reflection;

namespace LETOS.Infrastructure;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
