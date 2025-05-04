using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _mainStagesPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject[] _levelPrefabs;  // Array chứa tất cả prefab các level
    [SerializeField] private Transform _levelParent; // Nơi hiển thị các level

    private GameObject currentLevel;

    // Hàm để mở Settings Panel
    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
    }

    // Hàm để đóng Settings Panel
    public void Close()
    {
        _settingsPanel.SetActive(false);
    }

    // Hàm để mở Main Stages Panel
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
}
