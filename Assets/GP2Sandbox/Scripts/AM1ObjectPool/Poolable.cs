using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// AM1ObjectPoolで管理するオブジェクトにアタッチするスクリプトです。
    /// デフォルトで、オブジェクトの有効・無効の設定と、プールへの変換をするSpawn(pool, position)とDespawn()が定義されています。
    /// 必要に応じてオーバーライドしてください。
    /// </summary>
    public class Poolable : MonoBehaviour
    {
        protected AM1ObjectPool poolInstance;

        /// <summary>
        /// AM1ObjectPoolのSpawn()で取得したインスタンスから呼び出します。
        /// オブジェクトの座標を設定して有効にします。
        /// </summary>
        /// <param name="pool">所属するプールオブジェクト</param>
        /// <param name="pos">出現座標</param>
        public virtual void Spawn(AM1ObjectPool pool, Vector3 pos)
        {
            poolInstance = pool;
            transform.position = pos;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// オブジェクトを無効化して、オブジェクトプールに回収します。
        /// </summary>
        public virtual void Despawn()
        {
            gameObject.SetActive(false);
            if (poolInstance != null)
            {
                poolInstance.Release(this);
            }
        }
    }
}
