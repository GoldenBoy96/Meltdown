using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public void OnLevelComplete()
    {
        int currentIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        int nextIndex = currentIndex + 1;
        string nextLevel = "Level" + nextIndex;

        if (Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", nextIndex);
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.Log("Hết level hoặc chưa tạo scene tiếp theo.");
            SceneManager.LoadScene("MainMenu"); // Hoặc scene thắng
        }
    }
}
