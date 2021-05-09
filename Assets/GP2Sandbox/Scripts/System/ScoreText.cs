using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AM1
{
    /// <summary>
    /// スコアをUIに反映させるクラス。
    /// スコアのTextMeshProUGUIと同じオブジェクトにアタッチします。
    /// </summary>
    public class ScoreText : MonoBehaviour
    {
        TextMeshProUGUI scoreText;

        private void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            GameParams.onScoreChanged.AddListener(UpdateScore);
        }

        private void OnDestroy()
        {
            GameParams.onScoreChanged.RemoveListener(UpdateScore);
        }

        void UpdateScore()
        {
            scoreText.text = $"<mspace=0.6em>{GameParams.Score:000000}</mspace>";
        }
    }
}