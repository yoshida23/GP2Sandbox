using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// プールする弾を管理するオブジェクト
    /// </summary>
    public class Bullet : Poolable
    {
        Rigidbody rb;

        /// <summary>
        /// 速度
        /// </summary>
        float constantSpeed;

        /// <summary>
        /// ショットを撃ったオブジェクト
        /// </summary>
        public GameObject owner;

        /// <summary>
        /// 弾を消す時に呼び出したい処理を登録します。
        /// </summary>
        public readonly UnityEvent onDespawnEvent = new UnityEvent();

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 弾を発射
        /// </summary>
        /// <param name="pool">所属するオブジェクトプール</param>
        /// <param name="position">発生座標</param>
        /// <param name="velocity">速度</param>
        public void Spawn(AM1ObjectPool pool, Vector3 position, Vector3 velocity)
        {
            base.Spawn(pool, position);
            rb.velocity = velocity;
            constantSpeed = velocity.magnitude;
        }

        /// <summary>
        /// 弾を消してプールに戻します。
        /// </summary>
        public override void Despawn()
        {
            onDespawnEvent.Invoke();
            onDespawnEvent.RemoveAllListeners();
            rb.velocity = Vector3.zero;
            base.Despawn();
        }

        /// <summary>
        /// 速度を維持します
        /// </summary>
        private void FixedUpdate()
        {
            float spd = rb.velocity.magnitude;
            if ((spd > 0)
                && (spd < constantSpeed))
            {
                rb.velocity = rb.velocity.normalized * constantSpeed;
            }
        }
    }
}