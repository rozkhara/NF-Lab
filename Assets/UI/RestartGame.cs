using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spawners;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.GetComponent<TargetSpawner>().enabled = true;

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
