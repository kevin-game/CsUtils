using System.Reflection;

namespace CsUtils;

public static class TypeScanner
{
    static readonly Dictionary<Type, List<Type>> _attrToTypes = new();
    static readonly Dictionary<Type, List<Type>> _interfaceToTypes = new();
    //TODO: attr to normal methods
    //TODO: attr to static methods
    //TODO: attr to properties
    //TODO: attr to fields

    static TypeScanner()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetExportedTypes())
            .Where(t => !IsSystemType(t));

        foreach (var type in types)
        {
            InitAttrToTypes(type);
            InitInterfaceToTypes(type);
        }
    }

    static void Release()
    {
        _attrToTypes.Clear();
        _interfaceToTypes.Clear();
    }

    #region Init
    
    static void InitAttrToTypes(Type type)
    {
        var attrs = type.GetCustomAttributes().Where(a => !IsSystemType(a.GetType()));
        foreach (var attribute in attrs)
        {
            DictionarySafeAdd(_attrToTypes, attribute.GetType(), type);
        }
    }
    
    static void InitInterfaceToTypes(Type type)
    {
        var interfaces = type.GetInterfaces().Where(i => !IsSystemType(i.GetType()));
        foreach (var i in interfaces)
        {
            DictionarySafeAdd(_interfaceToTypes, i, type);
        }
    }
    

    #endregion

    #region get
    
    public static List<Type>? GetTypesWithAttribute<TAttribute>()
    {
        if (_attrToTypes.ContainsKey(typeof(TAttribute)))
            return _attrToTypes[typeof(TAttribute)].ToList();

        return null;
    }
    
    public static List<Type>? GetTypesWithInterface<TAttribute>()
    {
        if (_attrToTypes.ContainsKey(typeof(TAttribute)))
            return _attrToTypes[typeof(TAttribute)].ToList();

        return null;
    }
    

    #endregion
    
    static bool IsSystemType(Type type)
    {
        if (type.FullName == null) return false;
        if (type.FullName.StartsWith("Microsoft.Win32")) return true;
        if (type.FullName.StartsWith("Internal.")) return true;
        if (type.FullName.StartsWith("System.")) return true;
        return false;
    }

    static void DictionarySafeAdd<TKey, TValue>(Dictionary<TKey, List<TValue>> dict, TKey key, TValue value) where 
    TKey: notnull
    {
        if (!dict.ContainsKey(key)) 
            dict[key] = new List<TValue>();
        dict[key].Add(value);
    }
}