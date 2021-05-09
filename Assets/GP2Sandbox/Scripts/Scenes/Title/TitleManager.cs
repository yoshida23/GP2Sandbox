using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// タイトルシーン専用の管理クラス
    /// </summary>
    public class TitleManager : MonoBehaviour
    {
        public static TitleManager Instance { get; private set; }

        [Tooltip("タイトル画面アニメーター"), SerializeField]
        Animator titleAnimator = default;

        static bool isAwaked;

        private void Awake()
        {
            Instance = this;
            isAwaked = true;
        }

        private void OnDestroy()
        {
            isAwaked = false;
            Instance = null;
        }

        /// <summary>
        /// タイトル画面表示
        /// </summary>
        /// <returns></returns>
        public static IEnumerator StartScene()
        {
            while (!isAwaked)
            {
                yield return null;
            }

            GameManager.Instance.RemoveAllSpawnedObjects();
            Instance.titleAnimator.SetBool("Show", true);
            yield return  AnimEventWait.WaitEvent();
        }

        /// <summary>
        /// シーン終了時の処理
        /// </summary>
        public static IEnumerator EndScene()
        {
            Instance.titleAnimator.SetBool("Show", false);
            yield return AnimEventWait.WaitEvent();
        }

        /// <summary>
        /// 画面をクリックしたらボタンから呼び出す。
        /// </summary>
        public void GameStart()
        {
            SceneChanger.Change(GameScene.Instance);
        }
    }
}