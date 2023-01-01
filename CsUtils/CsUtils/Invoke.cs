using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsUtils
{
    public class Invoke
    {
        #region Delay
        public static void Delay(int delay, Delegate func)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke();
            });
        }

        public static void Delay<T1>(int delay, Delegate func, T1 t1)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1);
            });
        }

        public static void Delay<T1, T2>(int delay, Delegate func, T1 t1, T2 t2)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2);
            });
        }

        public static void Delay<T1, T2, T3>(int delay, Delegate func, T1 t1, T2 t2, T3 t3)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3);
            });
        }

        public static void Delay<T1, T2, T3, T4>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
            });
        }

        public static void Delay<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(int delay, Delegate func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T15 t16)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                dynamic d = func;
                d.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
            });
        }
#endregion
   
    }
}
