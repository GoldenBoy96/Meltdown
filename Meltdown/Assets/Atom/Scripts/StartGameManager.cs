using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        // Add listener to the start button
        startButton.onClick.AddListener(LoadMainHome);
    }

    public void LoadMainHome()
    {
        // Load the main home scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainHome");
    }
}
