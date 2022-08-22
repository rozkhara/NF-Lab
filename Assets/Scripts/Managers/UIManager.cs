using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI streakText;

        public float Score { get; private set; } = 0f;

        public int Streak { get; private set; } = 0;

        public Sprite crossHairSprite;

        public float CrossHairScale { get; set; } = 20f;

        public int CrossHairIdx { get; set; }

        private static UIManager instance;

        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                }

                return instance;
            }
        }

        private void Awake()
        {
            if (Instance != this) Destroy(this.gameObject); // 이미 UIManager가 있으면 이 UIManager 삭제

            DontDestroyOnLoad(this.gameObject); // 여러 씬에서 사용

            scoreText.text = "Score: \n" + Score.ToString();
            streakText.text = "Streak: \n" + Streak.ToString() + "X";
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
