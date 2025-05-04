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
    private int currentLevelIndex;

    void Start()
    {
        UpdateLevelButtons();
    }

    // Cập nhật trạng thái nút level
    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Lấy level mở khóa hiện tại

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                levelButtons[i].interactable = true; // Bật nút
                levelButtons[i].image.sprite = withStartBtnSprite; // Hình ảnh nút có thể bấm
            }
            else
            {
                levelButtons[i].interactable = false; // Tắt nút
                levelButtons[i].image.sprite = buttonLockSprite; // Hình ảnh nút khóa
            }
        }
    }

    // Mở panel Settings
    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    // Đóng panel Settings
    public void Close()
    {
        _settingsPanel.SetActive(false);
    }

    // Mở panel chọn stage
    public void OpenMainStagesPanel()
    {
        _mainStagesPanel.SetActive(true);
    }

    // Trở về màn chính
    public void Home()
    {
        _mainStagesPanel.SetActive(false);
    }

    // Chơi level
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

    // Khi người chơi hoàn thành level
    public void CompleteCurrentLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Lấy level hiện tại đã mở

        // Kiểm tra và mở khóa level tiếp theo nếu cần
        if (currentLevelIndex + 1 > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex + 1); // Mở khóa level tiếp theo
            PlayerPrefs.Save(); // Lưu lại thay đổi
        }

        // Cập nhật lại giao diện nút level
        UpdateLevelButtons();
    }
}
