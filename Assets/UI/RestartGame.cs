using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spawners;
using Managers;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        Target.UnloadResources();
        Target.UnloadPool();

        GameManager.Instance.GetComponent<TargetSpawner>().enabled = true;

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMain()
    {
        Target.UnloadResources();
        Target.UnloadPool();

        GameManager.Instance.GetComponent<TargetSpawner>().enabled = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene("StartScene");
    }
}
