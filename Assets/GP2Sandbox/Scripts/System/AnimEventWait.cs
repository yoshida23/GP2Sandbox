using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// アニメのイベントが呼ばれたかを判断するためのスクリプト。
    /// これをアニメから呼ばせてフラグで確認
    /// </summary>
    public class AnimEventWait : MonoBehaviour
    {
        /// <summary>
        /// アニメから呼び出されたらtrueにします。
        /// </summary>
        public static bool IsCalled { get; private set; }

        /// <summary>
        /// アニメのイベントが呼ばれるまで待機
        /// </summary>
        /// <returns></returns>
        public static IEnumerator WaitEvent()
        {
            IsCalled = false;
            while (!IsCalled)
            {
                yield return null;
            }
        }

        /// <summary>
        /// アニメから呼ばせるメソッド
        /// </summary>
        public void AnimEvent()
        {
            IsCalled = true;
        }
    }
}
