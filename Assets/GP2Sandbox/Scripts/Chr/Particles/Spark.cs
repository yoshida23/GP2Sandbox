using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// パーティクルをプールから取り出して作動させたり、消える時にプールに戻します。
    /// </summary>
    public class Spark : Poolable
    {
        ParticleSystem particleInstance;

        private void Awake()
        {
            particleInstance = GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// 座標を指定して、パーティクルを発生させます。
        /// </summary>
        /// <param name="pool">発生させるパーティクルを管理しているオブジェクトプール</param>
        /// <param name="pos">発生座標</param>
        public override void Spawn(AM1ObjectPool pool, Vector3 pos)
        {
            base.Spawn(pool, pos);
            particleInstance.Play();
        }

        /// <summary>
        /// パーティクルが停止した時に呼び出されるコールバック
        /// </summary>
        private void OnParticleSystemStopped()
        {
            Despawn();
        }
    }
}