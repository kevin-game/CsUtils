using System.Text.Json;
using Microsoft.VisualBasic.FileIO;

namespace IndieGameKit;

public class Archive
{
    public string FilePath { get; init; }
    public string Name { get; init; }
    
    public Archive(string filePath, string name)
    {
        FilePath = filePath;
        Name = name;
    }

    #region serialize

    string Encode<T>(T value) => ArchiveManager.Encode(value);
    T? Decode<T>(string value) => ArchiveManager.Decode<T>(value);
    #endregion

    #region save/load

    void CreateFile()
    {
        if (File.Exists(FilePath))
            return;

        var fs = File.Create(FilePath);
        fs.Close();
        
        var dict = new Dictionary<string, Dictionary<string, string>>
        {
            [Name] = new Dictionary<string, string>()
        };
        File.WriteAllText(FilePath, Encode(dict));
    }

    public void Save<T>(string key, T value)
    {
        if (!File.Exists(FilePath))
            CreateFile();
      
        var fileData = File.ReadAllText(FilePath);
        var dict = Decode<Dictionary<string, Dictionary<string, string>>>(fileData) ?? new Dictionary<string, 
        Dictionary<string, string>>();
        
        if (!dict.ContainsKey(Name))
            dict[Name] = new Dictionary<string, string>();
        dict[Name][key] = Encode(value);
        
        File.WriteAllText(FilePath, Encode(dict));
    }

    public T? Load<T>(string key)
    {
        var fileData = File.ReadAllText(FilePath);
        var dict = Decode<Dictionary<string, Dictionary<string, string>>>(fileData);
        if (dict == null || !dict.ContainsKey(Name) || !dict[Name].ContainsKey(key))
            return default(T);

        return Decode<T>(dict[Name][key]);
    }

    #endregion
    
}

public static class ArchiveManager
{
    private static string _filePath = ".gamedata.archive";

    #region file
    
    public static void SetFilePath(string path) => _filePath = path;

    public static Archive Open(string archiveName) => new Archive(_filePath, archiveName);

    public static void Delete(string archiveName)
    {
        if (!File.Exists(_filePath))
            return;
        
        var fileData = File.ReadAllText(_filePath);
        var dict = Decode<Dictionary<string, Dictionary<string, string>>>(fileData);
        if (dict == null || !dict.ContainsKey(archiveName))
            return;

        dict.Remove(archiveName);
    }
    
    #endregion

    #region save/load
    
    public static void Save<T>(string archiveName, string key, T value)
    {
        var archive = Open(archiveName);
        archive.Save(key, value);
    }

    public static T? Load<T>(string archiveName, string key)
    {
        var archive = Open(archiveName);
        return archive.Load<T>(key);
    }
    
    #endregion

    #region serialize

    public static string Encode<T>(T value) => JsonSerializer.Serialize(value, typeof(T));
    public static T? Decode<T>(string value) => JsonSerializer.Deserialize<T>(value);

    #endregion

}