using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// AM1ObjectPoolをシーンに保持して、シーン開始時に初期化するためのスクリプトです。
    /// </summary>
    public class ObjectPoolInitiator : MonoBehaviour
    {
        [Tooltip("このオブジェクト以下で管理するオブジェクトプール"), SerializeField]
        AM1ObjectPool[] pools = default;

        /// <summary>
        /// 設定されたオブジェクトプールを初期化した後、その後はオブジェクトプールの保有だけでいいのでこのコンポーネントを非アクティブにする。
        /// </summary>
        private void Awake()
        {
            for (int i=0;i<pools.Length;i++)
            {
                pools[i].Init(transform);
            }

            this.enabled = false;
        }
    }
}