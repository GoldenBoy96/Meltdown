using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _mainStagesPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private Transform _levelParent;

    [Header("Level Buttons UI")]
    [SerializeField] private Button[] levelButtons; // Level selection buttons
    [SerializeField] private Sprite buttonLockSprite;
    [SerializeField] private Sprite withStartBtnSprite;
    [SerializeField] private button CloseBtnSprite;;

    private GameObject currentLevel;
    private int currentLevelIndex;

    void Start()
    {
        UpdateLevelButtons();
    }

    // Update level button states based on unlocked levels
    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].image.sprite = withStartBtnSprite;
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].image.sprite = buttonLockSprite;
            }
        }
    }

    // Open Settings panel
    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    // Close Settings panel
    public void CloseSettingsPanel()
    {
        _settingsPanel.SetActive(false);
    }

    // Open stage selection panel
    public void OpenMainStagesPanel()
    {
        _mainStagesPanel.SetActive(true);
        _mainMenuPanel.SetActive(false); // Hide main menu
    }

    // Return to the main menu
    public void Home()
    {
        _mainStagesPanel.SetActive(false);
        _mainMenuPanel.SetActive(true); // Ensure the main menu is displayed
    }

    // Play selected level
    public void PlayLevel(int levelIndex)
    {
        _mainStagesPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
        currentLevelIndex = levelIndex;

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        if (levelIndex >= 0 && levelIndex < _levelPrefabs.Length && _levelPrefabs[levelIndex] != null)
        {
            currentLevel = Instantiate(_levelPrefabs[levelIndex], _levelParent);
        }
        else
        {
            Debug.LogWarning($"Level prefab is missing or index out of bounds: {levelIndex}");
        }
    }

    // Unlock next level upon completion
    public void CompleteCurrentLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Unlock next level only if completing a new one
        if (currentLevelIndex + 1 > unlockedLevel && currentLevelIndex + 1 <= levelButtons.Length)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex + 1);
            PlayerPrefs.Save();
            UpdateLevelButtons();
        }
    }
}