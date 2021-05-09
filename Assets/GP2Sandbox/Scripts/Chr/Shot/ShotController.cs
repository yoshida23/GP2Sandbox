using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1 {
    /// <summary>
    /// Shotで渡された弾を発射する挙動をゲームオブジェクトに追加します。
    /// IAddForwardを実装したコンポーネントが同じオブジェクトにアタッチされている必要があります。
    /// </summary>
    public class ShotController : MonoBehaviour
    {
        [Tooltip("バックファイヤ加速"), SerializeField]
        float backSpeed = -2f;
        [Tooltip("銃口の場所"), SerializeField]
        Transform shotPoint = default;

        IAddForward addForward;

        void Awake()
        {
            addForward = GetComponent<IAddForward>();
        }

        /// <summary>
        /// 射撃処理を呼び出します。
        /// </summary>
        /// <param name="shooter">撃ちたい弾を制御するインスタンス</param>
        public void Shot(IShooter shooter)
        {
            if (shooter.Shot(gameObject, shotPoint))
            {
                addForward.AddSpeed(backSpeed);
            }
        }
    }
}