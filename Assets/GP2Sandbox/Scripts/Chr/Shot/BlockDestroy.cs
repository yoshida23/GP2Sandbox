using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// ownerに設定したオブジェクト以外にぶつかったら消します。
    /// パーティクルプールが設定されていたらパーティクルを発生させます。
    /// </summary>
    public class BlockDestroy : MonoBehaviour
    {
        [Tooltip("火花オブジェクトプール"), SerializeField]
        AM1ObjectPool sparkObjectPool = default;

        Poolable poolable;

        /// <summary>
        /// ショットを撃ったオブジェクト
        /// </summary>
        public GameObject owner;

        private void Awake()
        {
            poolable = GetComponent<Poolable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // オーナーとぶつかっても何もしない
            if (other.gameObject == owner) return;

            if (sparkObjectPool != null)
            {
                var spark = sparkObjectPool.Get() as Spark;
                if (spark != null)
                {
                    spark.Spawn(sparkObjectPool, transform.position);
                }
            }

            if (poolable != null)
            {
                poolable.Despawn();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}