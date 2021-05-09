using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// シーンの切り替え処理に関するインターフェース。??Sceneクラスに実装。
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// シーンに切り替える時に呼び出すメソッド
        /// </summary>
        IEnumerator Change();


        /// <summary>
        /// シーンを終了する時の処理
        /// </summary>
        IEnumerator Release();
    }
}