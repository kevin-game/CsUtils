using System.Security.Cryptography;
using System.Threading.Channels;
using CsUtils;
using CsUtils.Algorithm;
using CsUtils.LiteSave;

SaveManager.Save("abc", 123);
Console.WriteLine($"load game data: {SaveManager.Load<int>("abc")}");
var types = TypeScanner.GetTypesWithAttribute<FlagsAttribute>();

var a = 3;
var b = 5;
Invoke.Delay(5000, ()=>b = b+a);
Console.WriteLine($"b={b}");
Thread.Sleep(6000);
Console.WriteLine($"b={b}");
