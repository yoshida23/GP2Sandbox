using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AM1
{
    /// <summary>
    /// シーンの切り替えを行うクラス。
    /// staticのChange()メソッドに、ISceneを実装したクラスのInstanceを渡して切り替えを要求します。
    /// 切り替え中はIsChangingがtrueになります。
    /// </summary>
    public class SceneChanger : MonoBehaviour
    {
        public static SceneChanger Instance { get; private set; }

        /// <summary>
        /// 次のシーン。未設定の時はnull
        /// </summary>
        static IScene nextScene = TitleScene.Instance;
        /// <summary>
        /// 現在のシーン
        /// </summary>
        static IScene currentScene;

        /// <summary>
        /// シーン切り替え中
        /// </summary>
        public static bool IsChanging { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (IsChanging || (nextScene == null)) return;

            IsChanging = true;
            StartCoroutine(ChangeSequence());
        }

        IEnumerator ChangeSequence()
        {
            yield return currentScene?.Release();
            currentScene = nextScene;
            nextScene = null;
            yield return currentScene?.Change();
            IsChanging = false;
        }

        /// <summary>
        /// 次のシーンを設定。すでに設定済みの時は無効。
        /// </summary>
        /// <param name="next">設定するインスタンス</param>
        public static void Change(IScene next)
        {
            if (nextScene != null) return;

            nextScene = next;
        }

        /// <summary>
        /// 指定のシーンをマルチシーンで読み込む
        /// </summary>
        /// <param name="scname">シーン名</param>
        public static IEnumerator LoadScenes(string []scnames)
        {
            for (int i = 0; i < scnames.Length; i++)
            {
                if (!SceneManager.GetSceneByName(scnames[i]).IsValid())
                {
                    yield return SceneManager.LoadSceneAsync(scnames[i], LoadSceneMode.Additive);
                }
            }
        }

        /// <summary>
        /// 指定のシーンを解放する。
        /// </summary>
        /// <param name="scname">シーン名</param>
        public static IEnumerator UnloadScenes(string []scnames)
        {
            for (int i = 0; i < scnames.Length ;i++) {
                if (SceneManager.GetSceneByName(scnames[i]).IsValid())
                {
                    yield return SceneManager.UnloadSceneAsync(scnames[i]);
                }
            }
        }
    }
}