using System;
using System.IO;
using System.Reflection;

public class PluginLoader : MarshalByRefObject
{
    private AppDomain _domain;
    private FileSystemWatcher _watcher;

    public PluginLoader(string path)
    {
        _domain = AppDomain.CreateDomain("PluginDomain");
        _watcher = new FileSystemWatcher(path);
        _watcher.Created += Watcher_Created;
        _watcher.Changed += Watcher_Changed;
        _watcher.Deleted += Watcher_Deleted;
        _watcher.Renamed += Watcher_Renamed;
        _watcher.EnableRaisingEvents = true;
    }

    private void Watcher_Renamed(object sender, RenamedEventArgs e)
    {
        Unload();
        Load();
    }

    private void Watcher_Deleted(object sender, FileSystemEventArgs e)
    {
        Unload();
    }

    private void Watcher_Changed(object sender, FileSystemEventArgs e)
    {
        Unload();
        Load();
    }

    private void Watcher_Created(object sender, FileSystemEventArgs e)
    {
        Load();
    }

    private void Load()
    {
        foreach (var file in Directory.GetFiles(_watcher.Path))
        {
            if (file.EndsWith(".dll") || file.EndsWith(".exe"))
            {
                Assembly assembly = Assembly.LoadFrom(file);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract && type.GetInterface("IPlugin") != null)
                    {
                        IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                        plugin.OnLoad();
                    }
                }
            }
        }
    }

    private void Unload()
    {
        AppDomain.Unload(_domain);
        _domain = AppDomain.CreateDomain("PluginDomain");
    }
}

public interface IPlugin
{
    void OnLoad();
}

public class MyPlugin : IPlugin
{
    public void OnLoad()
    {
        Console.WriteLine("MyPlugin loaded.");
    }
}

/*
另一个方案：
using System;
using System.IO;
using System.Reflection;

public class PluginManager
{
    private AppDomain _appDomain;
    private string _pluginPath;
    private string _pluginAssemblyName;
    private string _pluginTypeName;

    public PluginManager(string pluginPath, string pluginAssemblyName, string pluginTypeName)
    {
        _pluginPath = pluginPath;
        _pluginAssemblyName = pluginAssemblyName;
        _pluginTypeName = pluginTypeName;

        AppDomainSetup setup = new AppDomainSetup();
        setup.ApplicationBase = Path.GetDirectoryName(pluginPath);
        _appDomain = AppDomain.CreateDomain("PluginDomain", null, setup);
    }

    public void ExecutePlugin()
    {
        var assembly = _appDomain.Load(_pluginAssemblyName);
        var type = assembly.GetType(_pluginTypeName);
        var instance = Activator.CreateInstance(type);

        MethodInfo methodInfo = type.GetMethod("Execute");
        methodInfo.Invoke(instance, null);
    }

    public void Unload()
    {
        AppDomain.Unload(_appDomain);
    }
}
*/
