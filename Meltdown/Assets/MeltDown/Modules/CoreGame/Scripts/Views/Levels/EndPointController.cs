using MeltDown;
using System.Collections;
using System.Collections.Generic;
using MeltDown.Modules.CoreGame.Scripts.Views;
using UnityEngine;
using CharacterController = MeltDown.CharacterController;
using UnityEngine.SceneManagement;

public class EndPointController : MonoBehaviour
{

    [Header("Prefab")]
    [SerializeField] AlertIconController _alertIconPrefab;
    [SerializeField] Sprite _alertIconSprite;

    [SerializeField] private GameObject _winGamePanel;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();

        var alert = Instantiate(_alertIconPrefab.gameObject, GameViewController.Instance.AlertRect.transform);
        alert.GetComponent<AlertIconController>().Init(transform, GameViewController.Instance.AlertRect, alert.GetComponent<RectTransform>(), _alertIconSprite);
        if (_winGamePanel != null)
        {
            _winGamePanel.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<CharacterController>();
        if (collision.name == "Player")
        {
            Debug.Log("Win Game");
            Time.timeScale = 0f;


            if (_winGamePanel != null)
                _winGamePanel.SetActive(true);

            if (_uiManager != null)
                _uiManager.CompleteCurrentLevel();
        }
    }

    // Xử lý khi nhấn OK trong panel chiến thắng
    public void okBtn()
    {
        SceneManager.LoadScene(0);
    }
}
