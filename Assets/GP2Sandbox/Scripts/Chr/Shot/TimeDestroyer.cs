using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 時間が経過したらオブジェクトを消滅させます。
    /// </summary>
    public class TimeDestroyer : MonoBehaviour
    {
        [Tooltip("寿命の秒数"), SerializeField]
        float lifeTime = 2;

        Poolable poolable;
        float leftTime;

        private void Awake()
        {
            poolable = GetComponent<Poolable>();
        }

        private void FixedUpdate()
        {
            if (!gameObject.activeSelf) return;

            leftTime -= Time.fixedDeltaTime;
            if (leftTime < 0)
            {
                poolable.Despawn();
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            leftTime = lifeTime;
        }
    }
}