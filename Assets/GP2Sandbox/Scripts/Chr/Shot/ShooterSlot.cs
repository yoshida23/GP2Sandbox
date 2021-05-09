using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 弾を管理するスロットクラス。
    /// 弾を撃つShotAsset ScriptableObjectのインスタンスを渡して初期化すると、対応するIShooterインスタンスを`ShooterInstance`に返して利用可能にします。
    /// </summary>
    [System.Serializable]
    public class ShooterSlot
    {
        /// <summary>
        /// 弾を撃つ
        /// </summary>
        ShotAssetBase shotAsset;

        /// <summary>
        /// 弾を撃てるかどうかを管理するクラスのインスタンス
        /// </summary>
        public IShooter ShooterInstance { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// 射撃に利用するShotAssetBaseの派生クラスのインスタンスを渡して、弾を撃つ準備をします。
        /// </summary>
        /// <param name="setAsset">このスロットで管理する弾を撃つShotAssetBase ScriptableObjectのインスタンス</param>
        public ShooterSlot(ShotAssetBase setAsset)
        {
            SetShotAsset(setAsset);
        }

        /// <summary>
        /// 弾を入れ替える場合に新しいShotAssetBaseの派生クラスのインスタンスを渡して呼び出します。
        /// </summary>
        /// <param name="newAsset">差し替える射撃ScriptableObjectのインスタンス</param>
        public void SetShotAsset(ShotAssetBase newAsset)
        {
            shotAsset = newAsset;
            ShooterInstance = shotAsset.GetShooterInstance();
        }
    }
}