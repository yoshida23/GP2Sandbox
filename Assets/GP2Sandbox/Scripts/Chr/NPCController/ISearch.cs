using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// NPCの探索処理のインターフェース
    /// </summary>
    public interface ISearch
    {
        /// <summary>
        /// 探索処理。探索に必要なパラメーターは実装するクラス側に持たせる。
        /// </summary>
        /// <param name="layer">探索対象のレイヤー。GetLayer()の値</param>
        IEnumerator Search(int layer);

        /// <summary>
        /// Searchで見つけたオブジェクトのTransformを返します。
        /// </summary>
        /// <returns>見つけたオブジェクトのTransform。見つからなかった場合はnull</returns>
        Transform GetFoundObjectTransform();
    }
}