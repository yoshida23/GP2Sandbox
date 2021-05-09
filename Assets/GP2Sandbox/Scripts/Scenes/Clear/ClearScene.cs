using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// クリアシーンの開始と終了を処理するクラス
    /// </summary>
    public class ClearScene : SimpleSingleton<ClearScene>, IScene
    {
        readonly string[] loadSceneNames = {
            "Clear"
        };

        /// <summary>
        /// クリアシーンへの切り替え処理
        /// </summary>
        public IEnumerator Change()
        {
            yield return SceneChanger.LoadScenes(loadSceneNames);
            yield return ClearManager.StartScene();
        }

        /// <summary>
        /// タイトルシーンの終了処理
        /// </summary>
        public IEnumerator Release()
        {
            yield return ClearManager.EndScene();
        }
    }
}