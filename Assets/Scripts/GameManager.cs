using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController Player { get; set; }

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void GameOver()
    {
        Player.transform.GetComponent<TargetSpawner>().enabled = false;

        IsGameOver = true;

        Debug.Log("게임 오버!");
    }
}
