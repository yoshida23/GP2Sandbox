using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 弾の発射を制御するScriptableObject。
    /// IShooterを実装した特定の射撃の可否を制御するクラスとセットで運用します。
    /// </summary>

    [System.Serializable]
    public abstract class ShotAssetBase : ScriptableObject
    {
        [Tooltip("弾のオブジェクトプール"), SerializeField]
        protected AM1ObjectPool bulletPool = default;

        /// <summary>
        /// 弾を撃てるかどうかを管理するクラスのインスタンスを生成して返します。
        /// これをキャラクター側で保持して、Shot()を呼び出して弾を撃つようにします。
        /// </summary>
        /// <returns>弾を撃てるかどうかを判定して弾を撃つクラスのインスタンス</returns>
        public abstract IShooter GetShooterInstance();

        /// <summary>
        /// 弾を発射する処理をオーバーライドします。
        /// パラメーターはgetterのGetShotParams<T>(ref T shotParams)で受け取ります。
        /// </summary>
        /// <param name="getter">パラメーターを返すオブジェクト。通常は呼び出し元のthis</param>
        /// <returns>1発でも撃てたらtrue。オブジェクト切れなどはfalse</returns>
        public abstract bool Shot(IShooter getter);
    }
}