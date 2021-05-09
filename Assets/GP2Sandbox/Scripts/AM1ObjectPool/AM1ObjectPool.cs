using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    [CreateAssetMenu(menuName = "AM1/ObjectPoolAsset", fileName = "ObjectPool")]
    /// <summary>
    /// オブジェクトプール用ScriptableObject。
    /// Assetsメニュー > AM1 > Create ObjectPoolAssetでScriptableObjectアセットを作成できます。
    /// ScriptableObjectに、管理するPoolableスクリプトをアタッチしてあるプレハブと、生成上限数を設定します。
    /// シーンにプールオブジェクトを管理するゲームオブジェクトを作成して、ObjectPoolInitiatorをアタッチします。
    /// AM1ObjectPoolアセットをObjectPoolInitiatorにアタッチします。
    /// </summary>
    public class AM1ObjectPool : ScriptableObject
    {
        [Tooltip("管理対象のプレハブ"), SerializeField]
        Poolable prefab = default;
        [Tooltip("管理上限"), SerializeField]
        int objectMax = 10;

        /// <summary>
        /// 使用可能なオブジェクトのプール
        /// </summary>
        public List<Poolable> ObjectPool { get; private set; }

        /// <summary>
        /// 使用中のオブジェクト
        /// </summary>
        public List<Poolable> UsingPool { get; private set; }

        /// <summary>
        /// オブジェクトプールを生成します。
        /// 生成済みなら現状のプールを利用可能な状態にします。
        /// </summary>
        /// <param name="parentTransform">オブジェクトの配置先の親オブジェクト</param>
        public void Init(Transform parentTransform = null)
        {
            if (ObjectPool == null)
            {
                ObjectPool = new List<Poolable>(objectMax);
            }
            else if (ObjectPool.Capacity < objectMax)
            {
                ObjectPool.Capacity = objectMax;
            }
            if (UsingPool == null)
            {
                UsingPool = new List<Poolable>(objectMax);
            }
            else if (UsingPool.Capacity < objectMax)
            {
                UsingPool.Capacity = objectMax;
            }

            ReleaseAll();

            for (int i = 0; i < objectMax; i++)
            {
                if ((i < ObjectPool.Count)
                    && (ObjectPool[i] != null))
                {
                    continue;
                }

                var obj = Instantiate(prefab, parentTransform);
                if (i < ObjectPool.Count)
                {
                    ObjectPool[i] = obj;
                }
                else
                {
                    ObjectPool.Add(obj);
                }
                obj.Despawn();
            }
        }

        /// <summary>
        /// オブジェクトプールから使えるオブジェクトを返します。
        /// </summary>
        /// <returns>生成成功したらインスタンス。オブジェクトが無かったらnull</returns>
        public Poolable Get()
        {
            if (ObjectPool.Count == 0) return null;

            var obj = ObjectPool[ObjectPool.Count - 1];
            ObjectPool.RemoveAt(ObjectPool.Count - 1);
            UsingPool.Add(obj);
            return obj;
        }

        /// <summary>
        /// Poolableを継承したTクラスのインスタンスとして返します。
        /// </summary>
        /// <typeparam name="T">変換型</typeparam>
        /// <returns>T型にキャストした値</returns>
        public T Get<T>() where T : Poolable
        {
            return Get() as T;
        }

        /// <summary>
        /// 指定のインスタンスを未使用に変換します。
        /// </summary>
        /// <param name="instance">未使用にするインスタンス</param>
        public void Release(Poolable instance)
        {
            if (UsingPool.Remove(instance))
            {
                ObjectPool.Add(instance);
            }
        }

        /// <summary>
        /// 全てのオブジェクトを未使用にします。
        /// </summary>
        public void ReleaseAll()
        {
            while (UsingPool.Count > 0)
            {
                if (UsingPool[UsingPool.Count - 1] == null)
                {
                    UsingPool.RemoveAt(UsingPool.Count - 1);
                }
                else
                {
                    UsingPool[UsingPool.Count - 1].Despawn();
                }
            }
        }
    }
}