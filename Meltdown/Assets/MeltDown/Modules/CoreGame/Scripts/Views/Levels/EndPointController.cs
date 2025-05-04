using MeltDown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = MeltDown.CharacterController;
using UnityEngine.SceneManagement;

public class EndPointController : MonoBehaviour
{

    [Header("Prefab")]
    [SerializeField] AlertIconController _alertIconPrefab;

    [SerializeField] private GameObject _winGamePanel;
    private Button okButton;
    private UIManager _uiManager;

    private void Start()
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
        _uiManager = FindObjectOfType<UIManager>();
        Debug.Log("UIManager found: " + (_uiManager != null));

        Debug.Log(_alertIconPrefab.gameObject);
        Debug.Log(GameViewController.Instance.AlertRect);
>>>>>>> Stashed changes
        var alert = Instantiate(_alertIconPrefab.gameObject, GameViewController.Instance.AlertRect.transform);
        alert.GetComponent<AlertIconController>().Init(transform, GameViewController.Instance.AlertRect, alert.GetComponent<RectTransform>());

        if (_winGamePanel != null)
        {
            _winGamePanel.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log("Win Game");

            if (_uiManager != null)
            {
                _uiManager.CompleteCurrentLevel();
                Debug.Log("Đã gọi hàm CompleteCurrentLevel");
            }
            else
            {
                Debug.LogWarning("Không tìm thấy UIManager để gọi CompleteCurrentLevel");
            }

            _winGamePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void okBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
