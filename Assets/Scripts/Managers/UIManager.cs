using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        private TextMeshProUGUI scoreText;
        private TextMeshProUGUI streakText;

        public float Score { get; private set; } = 0f;

        public int Streak { get; private set; } = 0;

        public Sprite crossHairSprite;

        public float CrossHairScale { get; set; } = 20f;

        public int CrossHairIdx { get; set; }

        private float multiplier = 1f;

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

        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GameScene")
            {
                scoreText = GameObject.Find("ScoreCanvas").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                streakText = GameObject.Find("ScoreCanvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                scoreText.text = "Score: \n" + Score.ToString();
                streakText.text = "Streak: \n" + Streak.ToString() + "X";
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// 점수 증가
        /// </summary>
        public void IncreaseScore(float score)
        {
            score *= multiplier;
            Score += score;
            scoreText.text = "Score: \n" + ((int)Score).ToString();
        }

        public void ResetScore()
        {
            Score = 0f;
            scoreText.text = "Score: \n" + ((int)Score).ToString();
        }

        /// <summary>
        /// 스트릭 증가
        /// </summary>
        public void IncreaseStreak()
        {
            Streak++;
            SetMultiplier();
            streakText.text = "Streak: \n" + Streak.ToString() + "X";
        }

        public void ResetStreak()
        {
            Streak = 0;
            SetMultiplier();
            streakText.text = "Streak: \n" + Streak.ToString() + "X";
        }

        private void SetMultiplier()
        {
            if (Streak < 10) multiplier = 1f;
            else if (Streak < 20) multiplier = 1.15f;
            else if (Streak < 30) multiplier = 1.3f;
            else if (Streak < 40) multiplier = 1.45f;
            else if (Streak < 50) multiplier = 1.6f;
            else multiplier = 1.75f;
        }
    }
}
