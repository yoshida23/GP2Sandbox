using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 向いている方向に指定の速度で加速する機能を提供するインターフェース
    /// </summary>
    public interface IAddForward
    {
        /// <summary>
        /// 向いている方向に指定の速度を加算する
        /// </summary>
        /// <param name="speed">前方に対する速度。負なら逆方向</param>
        void AddSpeed(float speed);
    }
}