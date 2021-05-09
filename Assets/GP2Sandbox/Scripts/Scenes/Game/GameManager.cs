#define IS_DEBUG_KEY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace AM1
{
    /// <summary>
    /// ゲームシーン専用の管理クラス
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Tooltip("プレイヤープレハブ"), SerializeField]
        Transform playerPrefab = default;
        [Tooltip("NPCプレハブ"), SerializeField]
        Transform npcPrefab = default;
        [Tooltip("NPCの出現数"), SerializeField]
        int npcCount = 5;
        [Tooltip("NPCの親オブジェクト"), SerializeField]
        Transform npcParent = default;

        const int CharacterMax = 25;
        static bool isAwaked;
        public static bool IsGameStarted { get; private set; }

        static readonly List<Transform> spawnedTransforms = new List<Transform>(CharacterMax);

        private void Awake()
        {
            Instance = this;
            isAwaked = true;
            IsGameStarted = false;
        }

        private void OnDestroy()
        {
            isAwaked = false;
            Instance = null;
        }

        private void Update()
        {
#if IS_DEBUG_KEY
            if (Input.GetKeyDown(KeyCode.O))
            {
                ToGameover();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                ToClear();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameParams.AddScore(100);
            }
#endif
        }

        /// <summary>
        /// ゲーム開始処理
        /// </summary>
        /// <returns></returns>
        public static IEnumerator StartScene()
        {
            while (!isAwaked)
            {
                yield return null;
            }

            GameParams.SetScore(0);
            Spawner.Listup();

            // プレイヤー出現
            var chr = SpawnCharacter(Instance.playerPrefab);
            if (chr != null)
            {
                // カメラ設定
                CameraController.Instance.SetTargetTransform(chr);
            }

            // NPC出現
            for (int i = 0; i < Instance.npcCount; i++)
            {
                SpawnCharacter(Instance.npcPrefab, Instance.npcParent);
            }

            IsGameStarted = true;
        }

        /// <summary>
        /// 指定のプレハブを出現させる。
        /// </summary>
        /// <param name="prefab">出現させるプレハブ</param>
        /// <param name="parent">親オブジェクトのTransform。省略時はこのオブジェクトと同じシーンへ移動</param>
        /// <returns>生成したオブジェクトのTransform</returns>
        static Transform SpawnCharacter(Transform prefab, Transform parent = null)
        {
            var pos = Spawner.GetSpawnPoint();
            if (pos == null) return null;

            var chr = Instantiate(prefab, pos.Value, Quaternion.identity);
            if (parent == null)
            {
                SceneManager.MoveGameObjectToScene(chr.gameObject, Instance.gameObject.scene);
            }
            else
            {
                chr.SetParent(parent);
            }
            spawnedTransforms.Add(chr);

            return chr;
        }

        /// <summary>
        /// シーン終了時の処理
        /// </summary>
        public static IEnumerator EndScene()
        {
            IsGameStarted = false;
            CameraController.Instance.SetTargetTransform(null);
            yield break;
        }

        /// <summary>
        /// ゲームオーバーへ
        /// </summary>
        public void ToGameover()
        {
            if (IsGameStarted)
            {
                IsGameStarted = false;
                SceneChanger.Change(GameoverScene.Instance);
            }
        }

        /// <summary>
        /// クリアへ
        /// </summary>
        public void ToClear()
        {
            if (IsGameStarted)
            {
                IsGameStarted = false;
                SceneChanger.Change(ClearScene.Instance);
            }
        }

        /// <summary>
        /// スポーンした全てのオブジェクトを破棄
        /// </summary>
        public void RemoveAllSpawnedObjects()
        {
            while (spawnedTransforms.Count > 0)
            {
                Destroy(spawnedTransforms[0].gameObject);
                spawnedTransforms.RemoveAt(0);
            }
        }

        [System.Diagnostics.Conditional("DEBUG_LOG")]
        public static void DebugLog(object mes)
        {
            Debug.Log(mes);
        }
    }
}