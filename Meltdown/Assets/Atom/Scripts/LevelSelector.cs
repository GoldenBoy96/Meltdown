using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Transform levelParent; // Đây là nơi chứa các level đã load
    private GameObject currentLevel;

    void Start()
    {
        int levelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        LoadLevel(levelIndex);  // Load level khi bắt đầu game
    }

    public void LoadLevel(int index)
    {
        // Xóa level hiện tại nếu có
        if (currentLevel != null)
            Destroy(currentLevel);

        string prefabPath = "Prefabs/Levels/Level_" + index;
        GameObject levelPrefab = Resources.Load<GameObject>(prefabPath);

        if (levelPrefab != null)
        {
            // Instantiate prefab tại vị trí của levelParent
            currentLevel = Instantiate(levelPrefab, levelParent);
            PlayerPrefs.SetInt("CurrentLevelIndex", index);  // Lưu lại level hiện tại
        }
        else
        {
            Debug.Log("Không tìm thấy level " + index);
            // TODO: Load MainMenu hoặc cảnh thắng nếu muốn
        }
    }

    public void LoadNextLevel()
    {
        int nextIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1) + 1;
        LoadLevel(nextIndex);  // Load level tiếp theo
    }
}
