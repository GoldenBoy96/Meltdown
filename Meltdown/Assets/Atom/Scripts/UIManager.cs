using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _mainStagesPanel;
    [SerializeField] private GameObject _mainMenuPanel;

    [Header("Level Buttons UI")]
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Sprite buttonLockSprite;
    [SerializeField] private Sprite withStartBtnSprite;

    private int _nonGameScenesCount = 2;

    private void Start()
    {
        UpdateLevelButtons();
    }

    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 4);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndexTemp = i + 1; // Dùng biến tạm để tránh lỗi Lambda

            // Xóa sự kiện cũ trước khi thêm mới, tránh lỗi click nhiều lần
            levelButtons[i].onClick.RemoveAllListeners();

            if (i < unlockedLevel)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].image.sprite = withStartBtnSprite;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndexTemp));
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].image.sprite = buttonLockSprite;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        string sceneName = "Level" + levelIndex;

        if (SceneExists(sceneName))
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", levelIndex);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not esisted.");
        }
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (path.Contains(sceneName)) return true;
        }
        return false;
    }

    public void OpenSettingsPanel() => _settingsPanel.SetActive(true);
    public void CloseSettingsPanel() => _settingsPanel.SetActive(false);
    public void OpenMainStagesPanel() { _mainStagesPanel.SetActive(true); _mainMenuPanel.SetActive(false); }
    public void Home() { SceneManager.LoadScene("MainHome"); }

    public void CompleteCurrentLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);

        if (currentLevelIndex + 1 > unlockedLevel && currentLevelIndex + 1 <= levelButtons.Length)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex + 1);
            PlayerPrefs.Save();
            UpdateLevelButtons();
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex < (5 + _nonGameScenesCount - 1))
            SceneManager.LoadScene(currentLevelIndex + 1);
    }
}