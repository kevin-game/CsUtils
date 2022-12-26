using CsUtils;

Invoke.Delay(1000, Add, 3, 5);

void Add(int a, int b) => Console.WriteLine($"{a}+{b}={a + b}");

await Task.Delay(3000);