using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreObject;

    private TextMeshProUGUI scoreText;

    private int score;

    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (Instance != this) Destroy(this.gameObject); // 이미 ScoreManager가 있으면 이 ScoreManager 삭제

        DontDestroyOnLoad(this.gameObject); // 여러 씬에서 사용

        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();

        score = 0;
    }

    public void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
    }
    
    public void ResetScore()
    {
        score = 0;
    }

}
