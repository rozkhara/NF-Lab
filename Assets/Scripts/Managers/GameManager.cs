using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject crossHair;
        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private GameObject optionButton;
        [SerializeField] private GameObject homeButton;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject optionCanvas;
        [SerializeField] private GameObject gameOverCanvas;

        public static GameManager Instance { get; private set; }

        public PlayerController Player { get; set; }

        public bool IsGameOver { get; private set; }

        public bool IsGamePaused { get; private set; }

        public bool IsOptionCanvasOn { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsGameOver)
                {
                    OptionsPanelOnOff();
                }
            }
        }
        public void OptionsPanelOnOff()
        {
            if (IsGamePaused)
            {
                if (IsOptionCanvasOn)
                {
                    optionCanvas.SetActive(false);
                    optionButton.SetActive(true);
                    homeButton.SetActive(true);
                    restartButton.SetActive(true);

                    IsOptionCanvasOn = false;
                }
                else
                {
                    crossHair.SetActive(true);
                    pauseCanvas.SetActive(false);

                    IsGamePaused = false;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    Time.timeScale = 1f;
                }
            }
            else
            {
                crossHair.SetActive(false);
                pauseCanvas.SetActive(true);

                IsGamePaused = true;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                Time.timeScale = 0f;
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void GameOver()
        {
            GetComponent<TargetSpawner>().enabled = false;

            IsGameOver = true;

            GameOverPanelOn();
        }

        public void GameOverPanelOn()
        {
            crossHair.SetActive(false);
            gameOverCanvas.SetActive(true);

            IsGamePaused = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        }
    }
}
