using System.Security.Cryptography;
using System.Threading.Channels;
using CsUtils;
using CsUtils.Algorithm;
using GameKit;

string usrName = "我爱罗";
ArchiveManager.Save("存档1", "角色名", usrName);
var name = ArchiveManager.Load<string>("存档1", "角色名");
Console.WriteLine($"load game data: {name}");

var types = TypeScanner.GetTypesWithAttribute<FlagsAttribute>();

var a = 3;
var b = 5;
Invoke.Delay(5000, ()=>b = b+a);
Console.WriteLine($"b={b}");
Thread.Sleep(6000);
Console.WriteLine($"b={b}");
