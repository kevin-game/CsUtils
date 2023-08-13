using Plugin.Common;
using System.Reflection;

var dllList = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll");

foreach (var dll in dllList)
{
    var assembly = Assembly.LoadFile(dll);
    var types = assembly.GetExportedTypes();
    var pluginTypes = types.Where(type => type.IsAssignableTo(typeof(IPlugin)));
    foreach (var pluginType in pluginTypes)
    {
        var plugin = Activator.CreateInstance(pluginType) as IPlugin;
        plugin?.Run();
    }
}

