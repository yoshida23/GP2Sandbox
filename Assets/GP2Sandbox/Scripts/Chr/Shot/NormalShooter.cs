using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 通常弾を撃つかどうかを判定して弾を撃つクラス。
    /// NormalShotAsset ScriptableObjectで生成して、弾を撃つキャラクターが保持します。
    /// </summary>
    public class NormalShooter : IShooter
    {
        /// <summary>
        /// この弾を撃とうとしているShotAssetBase ScriptableObjectアセットのインスタンス
        /// </summary>
        readonly ShotAssetBase assetInstance;

        /// <summary>
        /// 同時に撃てる弾の上限数
        /// </summary>
        readonly int bulletCount;

        /// <summary>
        /// 連射間隔
        /// </summary>
        readonly float rapidInterval;

        /// <summary>
        /// 現在の弾数
        /// </summary>
        int useCount;

        /// <summary>
        /// 次に弾を撃てる時間
        /// </summary>
        float nextShotTime;

        /// <summary>
        /// 弾を撃った元のオブジェクト
        /// </summary>
        GameObject ownerObject;

        /// <summary>
        /// 銃口の位置
        /// </summary>
        Transform shotFrom;

        /// <summary>
        /// コンストラクタ。NormalShotAssetから呼び出すので通常、利用することはありません。
        /// </summary>
        /// <param name="asset">呼び出し元のShotAssetBaseを継承したScriptableObjectインスタンス</param>
        /// <param name="count">連射上限数</param>
        /// <param name="interval">連射間隔秒数</param>
        public NormalShooter(ShotAssetBase asset, int count, float interval)
        {
            assetInstance = asset;
            bulletCount = count;
            rapidInterval = interval;
            useCount = 0;
            nextShotTime = 0;
        }

        /// <summary>
        /// この弾のパラメーターを返します。
        /// </summary>
        /// <typeparam name="T">このクラスではGeneralShotParamsを指定</typeparam>
        /// <param name="shotParams">返す先</param>
        public void GetShotParams<T>(ref T shotParams)
        {
            if (shotParams is GeneralShotParams)
            {
                var general = shotParams as GeneralShotParams;
                general.ownerObject = ownerObject;
                general.position = shotFrom.position;
                general.forward = shotFrom.forward;
                return;
            }
        }

        /// <summary>
        /// 状況を確認して、弾を撃てるなら撃ちます。
        /// </summary>
        /// <param name="owner">弾を撃つオブジェクトのColliderを持っているインスタンス</param>
        /// <param name="from">呼び出し元のオブジェクトのTransform</param>
        /// <returns>撃ったらtrue</returns>
        public bool Shot(GameObject owner, Transform from)
        {
            // 制約チェック
            if ((useCount >= bulletCount)
                || (Time.time < nextShotTime))
            {
                return false;
            }

            // ショット
            ownerObject = owner;
            nextShotTime = Time.time + rapidInterval;
            useCount++;
            shotFrom = from;
            assetInstance.Shot(this);
            return true;
        }

        /// <summary>
        /// 弾のDespawn時に呼び出させるコールバックメソッドです。
        /// </summary>
        public void OnDespawn()
        {
            useCount--;
        }
    }
}