using System.Security.Cryptography;
using System.Threading.Channels;
using CsUtils;
using CsUtils.Algorithm;
using IndieGameKit;
using LiteDB;

// string usrName = "我爱罗";
// ArchiveFactory.Save("存档1", "角色名", usrName);
// var name = ArchiveFactory.Load<string>("存档1", "角色名");
// Console.WriteLine($"load game data: {name}");
//
// var types = TypeScanner.GetTypesWithAttribute<FlagsAttribute>();

var key = "level";
var value = 100;

using LiteDatabase db = new LiteDatabase("lite.db");
var collection = db.GetCollection<DataUnit>("collection_1");
collection.Insert(new DataUnit() { Key = key, Value = value });
var data = collection.FindOne(x => x.Key == key);
Console.WriteLine($"data={data}");

record DataUnit
{
    public ObjectId Id { get; set; }
    public string Key { get; set; }
    public int Value { get; set; }

    public void Print()
    {
        Console.WriteLine("hello");
    }
}
