using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// Search Rangeで設定した距離内に、自分以外の指定のレイヤーのオブジェクトがあったら返します。
    /// </summary>
    public class SearchRange : MonoBehaviour, ISearch
    {
        [Tooltip("探索範囲"), SerializeField]
        float searchRange = 15f;

        Transform foundObject;
        readonly Collider[] results = new Collider[2];

        /// <summary>
        /// 探索処理を実施。
        /// </summary>
        /// <param name="layer">探索するレイヤー</param>
        public IEnumerator Search(int layer)
        {
            foundObject = null;

            int count = Physics.OverlapSphereNonAlloc(transform.position, searchRange, results, layer);
            if (count < 2)
            {
                // 自分のみの時は見つけられr図
                yield break;
            }

            for (int i=0;i<count;i++)
            {
                if (results[i].gameObject != gameObject)
                {
                    foundObject = results[i].transform;
                    break;
                }
            }
            yield break;
        }

        /// <summary>
        /// Search()の探索結果を返します。
        /// 行動処理内で yield return Search(レイヤー); を実行した後に呼び出して、結果を取り出すのに使います。
        /// </summary>
        /// <returns>見つけたオブジェクト。なければnull</returns>
        public Transform GetFoundObjectTransform()
        {
            return foundObject;
        }
    }
}