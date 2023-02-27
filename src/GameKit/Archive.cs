using System.Text.Json;

namespace GameKit;

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

    string Encode<T>(T value) => JsonSerializer.Serialize(value, typeof(T));
    static T? Decode<T>(string value) => JsonSerializer.Deserialize<T>(value);
    #endregion

    #region save/load

    void CreateFile()
    {
        if (File.Exists(FilePath))
            throw new Exception("File already exist");

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

    #region file/archive operation
    
    public static void SetFilePath(string path) => _filePath = path;

    public static Archive Open(string archiveName) => new Archive(_filePath, archiveName);
    
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

}