using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 指定した座標を向かせる機能を提供するインターフェース
    /// </summary>
    public interface IHeadToPoint
    {
        /// <summary>
        /// ワールドポイントで向きたい座標を指定
        /// </summary>
        /// <param name="wpos">向きたい座標</param>
        void TargetPoint(Vector3 wpos);
    }
}