using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsUtils
{
    public class Invoke
    {
        public static void Delay<T1, T2>(int delay, Delegate func, T1 t1, T2 t2)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2);
            });
        }
    }
}
