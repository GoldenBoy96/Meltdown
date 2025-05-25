using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;


    public void OpenPausePanel()
    {
        Time.timeScale = 0f;
        _pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
    }
}
