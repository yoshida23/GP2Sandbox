using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 通常弾のパラメーター
    /// </summary>
    public class GeneralShotParams
    {
        public GameObject ownerObject;
        public Vector3 position;
        public Vector3 forward;
    }

    /// <summary>
    /// 通常弾のプールとインスタンスを管理するScriptableObject。
    /// NormalShooterとセットで運用します。引数はNormalShooterから受け取る形にします。
    /// </summary>
    [CreateAssetMenu(menuName = "AM1/Create NormalShotAsset", fileName = "NormalShotAsset")]
    public class NormalShotAsset : ShotAssetBase
    {
        [Tooltip("ショットスピード"), SerializeField]
        float speed = 10f;
        [Tooltip("弾数"), SerializeField]
        int bulletCount = 4;
        [Tooltip("連射インターバル"), SerializeField]
        float rapidInterval = 0.2f;

        GeneralShotParams shotParam = new GeneralShotParams();

        /// <summary>
        /// キャラクターがこの弾を獲得した時に呼び出して、返すIShooterのインスタンスを記録。
        /// 弾を撃つ時はこの戻り値のインスタンスのShot()を呼び出します。
        /// NormalShooterを生成して返します。
        /// </summary>
        /// <returns>制限条件を管理して、弾を撃てるかどうかを判定して、射撃するインスタンス</returns>
        public override IShooter GetShooterInstance()
        {
            return new NormalShooter(this, bulletCount, rapidInterval);
        }


        /// <summary>
        /// 向いている方向へ通常ショット。IShooter.Shot()から呼び出すので直接呼ぶことはない。
        /// </summary>
        public override bool Shot(IShooter getter)
        {
            getter.GetShotParams<GeneralShotParams>(ref shotParam);
            var shot = bulletPool.Get<Bullet>();
            if (shot == null) return false;
            shot.Spawn(bulletPool, shotParam.position, shotParam.forward * speed);
            shot.onDespawnEvent.AddListener(getter.OnDespawn);
            shot.owner = shotParam.ownerObject;
            return true;
        }
    }
}