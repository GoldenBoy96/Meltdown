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
    [SerializeField] private Button[] levelButtons; // Các button trong màn chọn level
    [SerializeField] private Sprite buttonLockSprite;
    [SerializeField] private Sprite withStartBtnSprite;

    private GameObject currentLevel;

    void Start()
    {
        UpdateLevelButtons();
    }

    // Cập nhật hình ảnh nút theo trạng thái mở/khóa
    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Mặc định mở level 1

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

    // Gọi khi hoàn thành level hiện tại
    public void CompleteCurrentLevel(int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (levelIndex + 1 > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1); // Mở khóa level tiếp theo
            PlayerPrefs.Save();
        }

        UpdateLevelButtons(); // Cập nhật giao diện button
    }
}