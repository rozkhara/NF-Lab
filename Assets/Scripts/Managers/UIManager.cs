using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI streakText;

    public float Score { get; private set; } = 0f;

    public int Streak { get; private set; } = 0;

    private float multiplier = 1f;

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
        if (Streak < 10)
        {
            multiplier = 1f;
        }
        else if (Streak < 20)
        {
            multiplier = 1.15f;
        }
        else if (Streak < 30)
        {
            multiplier = 1.3f;
        }
        else if (Streak < 40)
        {
            multiplier = 1.45f;
        }
        else if (Streak < 50)
        {
            multiplier = 1.6f;
        }
        else
        {
            multiplier = 1.75f;
        }
    }
}
