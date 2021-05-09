using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AM1
{
    /// <summary>
    /// スコアなどのゲーム全体で利用するパラメーターを管理します。
    /// </summary>
    public class GameParams : MonoBehaviour
    {
        public static GameParams Instance { get; private set; }

        /// <summary>
        /// スコア
        /// </summary>
        public static int Score { get; private set; }

        /// <summary>
        /// スコアが変わった時に呼び出す処理を登録するイベント
        /// </summary>
        public static readonly UnityEvent onScoreChanged = new UnityEvent();

        /// <summary>
        /// ハイスコア
        /// </summary>
        public static int HighScore { get; private set; }

        /// <summary>
        /// 最高スコア
        /// </summary>
        const int ScoreMax = 999999;

        /// <summary>
        /// ハイスコアの初期値
        /// </summary>
        const int DefaultHighScore = 100;

        private void Awake()
        {
            Instance = this;
            Score = 0;
            HighScore = DefaultHighScore;
        }

        /// <summary>
        /// スコア加算
        /// </summary>
        /// <param name="add">得点</param>
        public static void AddScore(int add)
        {
            Score = Mathf.Min(Score + add, ScoreMax);
            Score = Mathf.Max(Score, 0);
            onScoreChanged.Invoke();
        }

        /// <summary>
        /// 指定のスコアを設定。
        /// </summary>
        /// <param name="sc">新しく設定するスコア</param>
        public static void SetScore(int sc)
        {
            Score = Mathf.Min(sc, ScoreMax);
            Score = Mathf.Max(sc, 0);
            onScoreChanged.Invoke();
        }
    }
}