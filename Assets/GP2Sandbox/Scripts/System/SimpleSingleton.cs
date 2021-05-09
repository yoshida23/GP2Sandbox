using System.Collections;

namespace AM1
{
    /// <summary>
    /// ジェネリックで指定するクラスをInstanceで参照できるようにするシングルトンの抽象クラス。
    /// 非MonoBehaviourを想定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SimpleSingleton<T> where T : new()
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
        static T instance;
    }
}