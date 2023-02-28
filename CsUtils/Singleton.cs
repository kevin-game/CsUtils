using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsUtils;

/*
 * demo
 public class BagManager : Singleton<BagManager>
{
    ......
}
 */
public abstract class Singleton<T> where T : Singleton<T>, new() //泛型约束Singleton<T>，表示T只能继承自己本身的类或子类
{
    static readonly T _instance = new();

    public static T Instance
    {
        get { return _instance; }
    }
}

////Unity MonoSingleton
//public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour, MonoSingleton<T>
//{
//    public bool global = true;//是否在切换scence的时候，保留
//    static T instance;
//    public static T Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = (T)FindObjectOfType<T>();
//                if (instance == null)
//                {
//                    GameObject obj = new GameObject("MonoSingleton");//TODO：可以考虑把T的类名，也输出
//                    m_instance = obj.AddComponent<T>();
//                }
//            }
//            return instance;
//        }

//    }

//    void Awake()
//    {
//        Debug.LogWarningFormat("{0}[{1}] Awake", typeof(T), this.GetInstanceID());
//        if (global)
//        {
//            if (instance != null && instance != this.gameObject.GetComponent<T>())
//            {
//                Destroy(this.gameObject);
//                return;
//            }
//            DontDestroyOnLoad(this.gameObject);
//        }
//        this.OnStart();
//    }

//    protected virtual void OnStart()//TODO：可以考虑放到Awake之前，编程BeforeAwake
//    {

//    }
//}