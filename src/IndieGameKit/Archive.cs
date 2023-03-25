using System.Text;
using LiteDB;
using Newtonsoft.Json;

namespace IndieGameKit;

public class Archive
{
    private class DataUnit
    {
        //public ObjectId Id { get; set; }
        public string Key { get; set; }
        public byte[] Value { get; set; }
    }
    
    public string FilePath { get; init; }
    public string Name { get; init; }
    
    public Archive(string filePath, string name)
    {
        FilePath = filePath;
        Name = name;
    }

    #region serialize

    public byte[] Encode<T>(T value) => Encoding.Default.GetBytes(JsonConvert.SerializeObject(value));
    public T? Decode<T>(byte[] value) => JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(value));

    #endregion

    #region save/load

    public void Save<T>(string key, T value)
    {
        byte[] data = Encode(value);
        using LiteDatabase db = new LiteDatabase(FilePath);
        var collection = db.GetCollection<DataUnit>(Name);
        
        var obj = collection.FindOne(x => x.Key == key);
        if (obj == default)
        {
            collection.Insert(new DataUnit() { Key = key, Value = data });
        }
        else
        {
            obj.Value = data;
            collection.Update(obj);
        }
    }

    public T? Load<T>(string key)
    {
        using LiteDatabase db = new LiteDatabase(FilePath);
        var collection = db.GetCollection<DataUnit>(Name);
        var obj = collection.FindOne(x => x.Key == key);

        return Decode<T>(obj.Value);
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
        using LiteDatabase db = new LiteDatabase(_filePath);
        db.DropCollection(archiveName);
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


}


