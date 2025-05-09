using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    void Start()
    {
        int levelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        LoadLevel(levelIndex); // Load scene khi bắt đầu game
    }

    public void LoadLevel(int index)
    {
        string sceneName = "Level" + index; // Giả sử scene có tên "Level1", "Level2", ...
        if (SceneExists(sceneName))
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", index); // Lưu lại level hiện tại
            SceneManager.LoadScene(sceneName); // Load scene theo tên
        }
        else
        {
            Debug.LogError($"Scene {sceneName} không tồn tại. Hãy kiểm tra Build Settings!");
        }
    }

    public void LoadNextLevel()
    {
        int nextIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1) + 1;
        LoadLevel(nextIndex); // Load scene tiếp theo
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
}