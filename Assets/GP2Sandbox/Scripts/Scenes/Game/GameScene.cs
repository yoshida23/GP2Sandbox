using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// ゲームシーンの開始と終了を処理するクラス
    /// </summary>
    public class GameScene : SimpleSingleton<GameScene>, IScene
    {
        readonly string[] loadSceneNames = {
            "Game"
        };

        /// <summary>
        /// ゲームシーンの開始処理
        /// </summary>
        public IEnumerator Change()
        {
            yield return SceneChanger.LoadScenes(loadSceneNames);
            yield return GameManager.StartScene();
        }

        public IEnumerator Release()
        {
            yield return GameManager.EndScene();
        }
    }
}