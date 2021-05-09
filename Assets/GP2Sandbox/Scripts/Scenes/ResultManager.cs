using System.Collections;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// ゲームオーバーやクリアの管理クラス
    /// </summary>
    public class ResultManager : MonoBehaviour
    {
        public static ResultManager Instance { get; private set; }

        [Tooltip("アニメーター"), SerializeField]
        Animator animator = default;

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
        /// 画面表示
        /// </summary>
        public static IEnumerator StartScene()
        {
            while (!isAwaked)
            {
                yield return null;
            }

            Instance.animator.SetBool("Show", true);
            yield return  AnimEventWait.WaitEvent();
        }

        /// <summary>
        /// シーン終了時の処理
        /// </summary>
        public static IEnumerator EndScene()
        {
            Instance.animator.SetBool("Show", false);
            yield return AnimEventWait.WaitEvent();
        }

        /// <summary>
        /// 画面をクリックしたらボタンから呼び出す。
        /// </summary>
        public void ToTitle()
        {
            SceneChanger.Change(TitleScene.Instance);
        }
    }
}