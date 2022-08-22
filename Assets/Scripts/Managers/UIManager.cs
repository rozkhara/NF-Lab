using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI streakText;

        public float Score { get; private set; } = 0f;

        public int Streak { get; private set; } = 0;

        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            scoreText.text = "Score: \n" + Score.ToString();
            streakText.text = "Streak: \n" + Streak.ToString() + "X";
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        /// <summary>
        /// 점수 증가
        /// </summary>
        public void IncreaseScore(float score)
        {
            Score += score;
            scoreText.text = "Score: \n" + Score.ToString();
        }

        public void ResetScore()
        {
            Score = 0f;
            scoreText.text = "Score: \n" + Score.ToString();
        }

        /// <summary>
        /// 스트릭 증가
        /// </summary>
        public void IncreaseStreak()
        {
            Streak++;
            streakText.text = "Streak: \n" + Streak.ToString() + "X";
        }

        public void ResetStreak()
        {
            Streak = 0;
            streakText.text = "Streak: \n" + Streak.ToString() + "X";
        }
    }
}
