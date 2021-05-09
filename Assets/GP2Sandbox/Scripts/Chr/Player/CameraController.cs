using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// プレイヤー追うカメラ
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        /// <summary>
        /// カメラの高さ
        /// </summary>
        const float CameraHeight = 40;

        /// <summary>
        /// 最高速度
        /// </summary>
        const float MaxSpeed = 40f;

        /// <summary>
        /// 視野の中心にする位置。nullの時、追跡なし
        /// </summary>
        static Transform targetTransform = null;

        public static Camera CurrentCamera { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
            CurrentCamera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (targetTransform == null) return;

            float h = CameraHeight- targetTransform.position.y;
            var targetPos = 
                targetTransform.position
                + transform.forward * h / transform.forward.y;
            var to = targetPos - transform.position;
            float tickDistMax = MaxSpeed * Time.deltaTime;
            float dist = Mathf.Min(to.magnitude, tickDistMax);
            var next = transform.position + to.normalized * dist;
            transform.position = next;
        }

        /// <summary>
        /// カメラが追う対象のTransformを設定ます。
        /// </summary>
        /// <param name="tg">カメラが追う対象のTransform</param>
        public void SetTargetTransform(Transform tg)
        {
            targetTransform = tg;
        }
    }
}