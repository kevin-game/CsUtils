using System.Security.Cryptography;
using System.Threading.Channels;
using CsUtils;
using CsUtils.Algorithm;

var types = TypeScanner.GetTypesWithAttribute<FlagsAttribute>();

var a = 3;
var b = 5;
Invoke.Delay(5000, ()=>b = b+a);
Console.WriteLine($"b={b}");
Thread.Sleep(6000);
Console.WriteLine($"b={b}");
