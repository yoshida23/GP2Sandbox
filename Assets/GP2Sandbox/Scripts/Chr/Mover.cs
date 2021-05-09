using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 旋回と加速を実装するクラス。
    /// プレイヤーなら入力に対応して呼び出し、NPCは行動判定に応じて呼び出すことで、共通して利用できます。
    /// </summary>
    public class Mover : MonoBehaviour, IAddForward, IHeadToPoint
    {
        [Tooltip("旋回の秒速角度"), SerializeField]
        float rotateSpeed = 90;
        [Tooltip("ターゲットがこれより近ければ旋回しない"), SerializeField]
        float ignoreDistance = 0.1f;

        Rigidbody rb;
        Vector3 targetPoint;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            targetPoint = transform.position;
        }

        void FixedUpdate()
        {
            // 旋回
            var to = targetPoint - transform.position;
            to.y = 0;
            if (to.magnitude < ignoreDistance) {
                rb.angularVelocity = Vector3.zero;
                return;
            }

            // 水平を保つ
            var eu = transform.eulerAngles;
            eu.x = eu.z = 0;
            transform.eulerAngles = eu;


            float ang = Vector3.SignedAngle(transform.forward, to, Vector3.up);
            float rotSpeed = Mathf.Min(ang, rotateSpeed);
            var angVel = Vector3.zero;
            angVel.y = rotSpeed;
            rb.angularVelocity = angVel;
        }

        /// <summary>
        /// 加速
        /// </summary>
        /// <param name="speed">加速したいスピード</param>
        public void AddSpeed(float speed)
        {
            var v = rb.velocity;
            v += transform.forward * speed;
            rb.velocity = v;
        }

        /// <summary>
        /// 向かせたい目的座標
        /// </summary>
        /// <param name="wpos">目的の座標</param>
        public void TargetPoint(Vector3 wpos)
        {
            targetPoint = wpos;
        }
    }
}