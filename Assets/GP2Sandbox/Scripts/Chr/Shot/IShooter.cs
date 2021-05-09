using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// 弾を撃てるかどうかの制約を管理して、可能なら弾を発射するクラスに実装するインターフェース。
    /// 対応するShotAssetBaseのShotと対にします。
    /// </summary>
    public interface IShooter
    {
        /// <summary>
        /// 型を指定して、弾を撃つのに必要なパラメータを引数の参照先に設定します。
        /// </summary>
        /// <typeparam name="T">受け取りたいデータの型</typeparam>
        /// <param name="shotParams">値を受け取るオブジェクトのインスタンス</param>
        void GetShotParams<T>(ref T shotParams);

        /// <summary>
        /// 弾を撃てるかを確認して、可能なら生成元の弾アセットのショット処理を呼び出します。
        /// </summary>
        /// <param name="owner">ショットを撃ったオブジェクトのColliderを持つインスタンス</param>
        /// <param name="shotFrom">銃口の位置</param>
        /// <returns>撃ったらtrue</returns>
        bool Shot(GameObject owner, Transform shotFrom);

        /// <summary>
        /// 弾を消す時に呼ぶメソッド
        /// </summary>
        void OnDespawn();
    }
}