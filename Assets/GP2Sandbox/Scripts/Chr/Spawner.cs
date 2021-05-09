using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// プレイヤーやNPCなどのキャラクターを生成するスタート座標を管理するクラス。
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// 利用可能な開始位置
        /// </summary>
        static List<Spawner> spawners = new List<Spawner>();

        /// <summary>
        /// 使用済み開始位置
        /// </summary>
        readonly static List<Spawner> used = new List<Spawner>();

        /// <summary>
        /// 全Spawnerインスタンス
        /// </summary>
        static Spawner[] allSpawners;

        /// <summary>
        /// シーンにあるSpawnerオブジェクトのインスタンスをリストアップ
        /// </summary>
        public static void Listup()
        {
            allSpawners = GameObject.FindObjectsOfType<Spawner>();
            spawners = new List<Spawner>(allSpawners);
            used.Clear();
        }

        /// <summary>
        /// 出現座標をランダムで返します。
        /// 出現座標がなければnull
        /// </summary>
        /// <returns>出現座標。なければnull</returns>
        public static Vector3? GetSpawnPoint()
        {
            if (spawners.Count == 0) return null;

            int idx = Random.Range(0, spawners.Count);
            used.Add(spawners[idx]);
            spawners.RemoveAt(idx);
            return used[used.Count - 1].transform.position;
        }

        /// <summary>
        /// 全スポーンポイントからランダムなものを返します。
        /// </summary>
        /// <returns>Spawnのうちの1つのTransform</returns>
        public static Transform GetRandomTransform()
        {
            int idx = Random.Range(0, allSpawners.Length);
            return allSpawners[idx].transform;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}