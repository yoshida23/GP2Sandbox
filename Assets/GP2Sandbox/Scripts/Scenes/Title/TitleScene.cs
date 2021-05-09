using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// タイトルシーンの開始と終了を処理するクラス
    /// </summary>
    public class TitleScene : SimpleSingleton<TitleScene>, IScene
    {
        readonly string[] loadSceneNames = {
            "Title",
            "Game"
        };

        /// <summary>
        /// タイトルシーンへの切り替え処理
        /// </summary>
        public IEnumerator Change()
        {
            yield return SceneChanger.LoadScenes(loadSceneNames);
            yield return TitleManager.StartScene();
        }

        /// <summary>
        /// タイトルシーンの終了処理
        /// </summary>
        public IEnumerator Release()
        {
            yield return TitleManager.EndScene();
        }
    }
}