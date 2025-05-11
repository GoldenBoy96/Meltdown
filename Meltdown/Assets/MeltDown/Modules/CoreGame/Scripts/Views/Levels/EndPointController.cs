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
    [SerializeField] SpriteRenderer _customerSprite;
    [SerializeField] Sprite _idleSprite;
    [SerializeField] Sprite _eatingSprite;
    [SerializeField] Sprite _buringSprite;
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
            SetResult(Result.Win);
            Time.timeScale = 0f;
            GameViewController.Instance.SetCameraFollower(transform);

            if (_winGamePanel != null)
                if (_delayEndGamePanelCoroutine == null) StartCoroutine(DelayEndGamePanelCoroutine(Result.Win));

            if (_uiManager != null)
                _uiManager.CompleteCurrentLevel();
        }
    }

    public void SetResult(Result result)
    {
        switch (result)
        {
            case Result.Normal:
                _customerSprite.sprite = _idleSprite;
                break;
            case Result.Win:
                _customerSprite.sprite = _eatingSprite;
                break;
            case Result.Lose:
                _customerSprite.sprite = _buringSprite;
                break;
        }
    }

    // Xử lý khi nhấn OK trong panel chiến thắng
    public void okBtn()
    {
        SceneManager.LoadScene(0);
    }
    private Coroutine _delayEndGamePanelCoroutine;
    IEnumerator DelayEndGamePanelCoroutine(Result result)
    {
        yield return new WaitForSeconds(2);
        switch (result)
        {
            case Result.Win:
                _winGamePanel.SetActive(true);
                break;
            case Result.Lose:
                break;
        }
    }
}

public enum Result
{
    Normal,
    Win, 
    Lose
}