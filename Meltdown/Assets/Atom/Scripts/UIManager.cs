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
        _settingsPanel.transform.localScale = Vector3.zero;
        LeanTween.scale(_settingsPanel, Vector3.one, 0.3f).setEaseOutBack();
    }

    public void Close()
    {
        LeanTween.scale(_settingsPanel, Vector3.zero, 0.2f).setEaseInBack().setOnComplete(() =>
        {
            _settingsPanel.SetActive(false);
        });
    }

    public void OpenMainStagesPanel()
    {
        _mainStagesPanel.SetActive(true);
        _mainStagesPanel.transform.localScale = Vector3.zero;
        LeanTween.scale(_mainStagesPanel, Vector3.one, 0.35f).setEaseOutBack();
    }

    public void Home()
    {
        LeanTween.scale(_mainStagesPanel, Vector3.zero, 0.25f).setEaseInBack().setOnComplete(() =>
        {
            _mainStagesPanel.SetActive(false);
        });
    }

    public void PlayLevel(int levelIndex)
    {
        LeanTween.scale(_mainStagesPanel, Vector3.zero, 0.25f).setEaseInBack().setOnComplete(() =>
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
                currentLevel.transform.localScale = Vector3.zero;
                LeanTween.scale(currentLevel, Vector3.one, 0.4f).setEaseOutBack();
            }
            else
            {
                Debug.Log("Không tìm thấy level với chỉ số: " + levelIndex);
            }
        });
    }

    public void CompleteCurrentLevel(int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (levelIndex + 1 > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
            PlayerPrefs.Save();
        }

        UpdateLevelButtons();
    }
}
