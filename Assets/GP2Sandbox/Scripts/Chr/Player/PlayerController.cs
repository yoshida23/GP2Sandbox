using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// AM1Inputの入力を読み取って、必要なMoverの処理を呼び出すクラス。
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [Tooltip("ショットを管理するScriptableObject"), SerializeField]
        ShotAssetBase[] shotObjects = default;

        IHeadToPoint headToPoint;
        int currentShotIndex = 0;

        /// <summary>
        /// 複数の種類の弾を持てる前提で用意している射撃スロット
        /// </summary>
        ShooterSlot[] shooterSlots;

        /// <summary>
        /// 弾の発射と同時にバックオフする機能を提供するコンポーネントのインスタンス
        /// </summary>
        ShotController shotController;

        void Awake()
        {
            Instance = this;
            headToPoint = GetComponent<IHeadToPoint>();
            shooterSlots = new ShooterSlot[shotObjects.Length];
            shotController = GetComponent<ShotController>();
            for (int i=0;i<shotObjects.Length;i++)
            {
                shooterSlots[i] = new ShooterSlot(shotObjects[i]);
            }
        }

        private void Update()
        {
            if (SceneChanger.IsChanging) return;

            // マウスカーソルの場所をワールドにする
            headToPoint.TargetPoint(AM1Input.PointOnFloor);

            // ショット
            if (AM1Input.IsFire)
            {
                shotController.Shot(shooterSlots[currentShotIndex].ShooterInstance);
            }
        }
    }
}