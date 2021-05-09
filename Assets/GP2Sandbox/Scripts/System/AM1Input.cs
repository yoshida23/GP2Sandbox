using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// 入力管理クラス。システムへの依存を引き受け、外部に処理済みの入力データを提供する。
    /// ScriptExecutionOrderでデフォルトより先に実行
    /// </summary>
    public class AM1Input : MonoBehaviour
    {
        public static AM1Input Instance { get; private set; }

        /// <summary>
        /// マウスが指している床の位置のワールド座標
        /// </summary>
        public static Vector3 PointOnFloor;

        /// <summary>
        /// ファイアーに割り当てられているキーが押されている時、true
        /// </summary>
        public static bool IsFire;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            var ray = CameraController.CurrentCamera.ScreenPointToRay(Input.mousePosition);
            float h = 0 - ray.origin.y;
            PointOnFloor = ray.origin + ray.direction * h / ray.direction.y;

            IsFire = Input.GetButtonDown("Fire1");
        }
    }
}