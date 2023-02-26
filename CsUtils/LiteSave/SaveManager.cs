using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace CsUtils.LiteSave;

public static class SaveManager
{
    private static string _savePath = ".gamedata.rar";


    static SaveManager()
    {
        //TODO: reflection to get auto save/load static method
    }

    public static void SetSavePath(string path)
    {
        _savePath = path;
    }

    #region auto save/load

    public static void Save()
    {
        //TODO: 
    }

    public static void Load()
    {
        //TODO: 
    }

    #endregion

    #region serialize
    static byte[] Encode<T>(T value) => Encoding.Default.GetBytes(JsonConvert.SerializeObject(value));
    static T? Decode<T>(byte[] value) => JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(value));
    #endregion

    #region save/load

    public static void Save<T>(string name, T data)
    {
        if (File.Exists(_savePath))
        {
            var bytes = File.ReadAllBytes(_savePath);
            var dict = Decode<Dictionary<string, byte[]>>(bytes) ?? new Dictionary<string, byte[]>();
            dict[name] = Encode(data);
            File.WriteAllBytes(_savePath, Encode(dict));
        }
        else
        {
            File.Create(_savePath);
            var dict = new Dictionary<string, byte[]>
            {
                [name] = Encode(data)
            };
            File.WriteAllBytes(_savePath, Encode(dict));
        }
    }

    public static T Load<T>(string name)
    {
        var bytes = File.ReadAllBytes(_savePath);
        var dict = Decode<Dictionary<string, byte[]>>(bytes);
        if (!dict.ContainsKey(name))
            throw new KeyNotFoundException($"Game data \"{name}\" not found");

        return Decode<T>(dict[name]);
    }

    public static T? TryLoad<T>(string name)
    {
        var bytes = File.ReadAllBytes(_savePath);
        var dict = Decode<Dictionary<string, byte[]>>(bytes);
        return !dict.ContainsKey(name) ? default(T) : Decode<T>(dict[name]);
    }
    
    #endregion
}