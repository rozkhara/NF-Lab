using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public GameObject scoreObject;
    private TextMeshProUGUI scoreText;
    public int Score { get; private set; }

    private void Awake()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        Score = 0;
    }

    public void Update()
    {
        scoreText.text = "Score: " + Score.ToString();
    }

    public void IncreaseScore(int increment)
    {
        Score += increment;
    }
    
    public void ResetScore()
    {
        Score = 0;
    }

}
