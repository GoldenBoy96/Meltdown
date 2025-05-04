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
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Sprite buttonLockSprite;
    [SerializeField] private Sprite withStartBtnSprite;

    private GameObject currentLevel;
    private int currentLevelIndex;

    void Start()
    {
        UpdateLevelButtons();

    }

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

    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    public void Close()
    {
        _settingsPanel.SetActive(false);
    }

    public void OpenMainStagesPanel()
    {
        _mainStagesPanel.SetActive(true);
    }

    public void Home()
    {
        _mainStagesPanel.SetActive(false);
    }

    public void PlayLevel(int levelIndex)
    {
        _mainStagesPanel.SetActive(false);
        _mainMenuPanel.SetActive(false);
        currentLevelIndex = levelIndex;

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        if (levelIndex >= 0 && levelIndex < _levelPrefabs.Length)
        {
            currentLevel = Instantiate(_levelPrefabs[levelIndex], _levelParent);
        }
        else
        {
            Debug.Log("Không tìm thấy level với chỉ số: " + levelIndex);
        }
    }

    public void CompleteCurrentLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        Debug.Log("Level hiện tại: " + currentLevelIndex + " | UnlockedLevel cũ: " + unlockedLevel);

        if (currentLevelIndex + 1 > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex + 1);
            PlayerPrefs.Save();
            Debug.Log("Đã lưu UnlockedLevel mới: " + (currentLevelIndex + 1));
        }

        UpdateLevelButtons();
    }


}
