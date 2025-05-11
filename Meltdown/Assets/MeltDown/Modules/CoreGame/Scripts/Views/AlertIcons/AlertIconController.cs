using MeltDown;
using System.Collections;
using System.Collections.Generic;
using MeltDown.Modules.CoreGame.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

public class AlertIconController : MonoBehaviour
{
    public const float opacityLevel0 = 0.0f;
    public const float opacityLevel1 = 0.1f;
    public const float opacityLevel2 = 0.2f;
    public const float opacityLevel3 = 0.3f;
    public const float opacityLevel4 = 0.4f;
    public const float opacityLevel5 = 0.5f;

    public const float distanceLevel1 = 25;
    public const float distanceLevel2 = 20;
    public const float distanceLevel3 = 15;
    public const float distanceLevel4 = 10;
    public const float distanceLevel5 = 5;

    [SerializeField] float _opacity;
    public Transform target;
    public Camera mainCamera;
    public RectTransform canvasRect;
    public RectTransform indicatorUI;
    public Image indicatorImage;

    private void OnEnable()
    {
        SetOpacity(opacityLevel0);
    }

    public void Init(Transform target, RectTransform canvasRect, RectTransform indicatorUI, Sprite indicatorSprite)
    {
        this.target = target;
        mainCamera = Camera.main;
        this.canvasRect = canvasRect;
        this.indicatorUI = indicatorUI;
        this.indicatorImage.sprite = indicatorSprite;
    }
    void Update()
    {
        if (target == null || mainCamera == null || canvasRect == null || indicatorUI == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        bool isOffScreen = screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height;

        //indicatorImage.gameObject.SetActive(isOffScreen);

        if (isOffScreen)
        {
            SetOpacity();
            // Clamp to screen edges
            screenPos.x = Mathf.Clamp(screenPos.x, 50, Screen.width - 50);
            screenPos.y = Mathf.Clamp(screenPos.y, 50, Screen.height - 50);

            // Convert screen position to Canvas space
            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, mainCamera, out canvasPos);

            indicatorUI.anchoredPosition = canvasPos;

            // Optional: Rotate to point at enemy
            Vector3 direction = (target.position - mainCamera.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicatorUI.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {

            SetOpacity(opacityLevel0);
        }
    }


    public void SetOpacity(float opacity)
    {
        _opacity = opacity;
        Color color = indicatorImage.color;
        color.a = Mathf.Clamp01(opacity); // Clamp to [0, 1] range
        indicatorImage.color = color;
    }
    public void SetOpacity()
    {
        var distance = Vector3.Distance(target.position, GameViewController.Instance.Player.transform.position);
        if (distance < distanceLevel5)
        {
            _opacity = opacityLevel5;
        } else if (distance > distanceLevel5 && distance <= distanceLevel4)
        {
            _opacity = opacityLevel4;
        } else if (distance > distanceLevel4 && distance <= distanceLevel3)
        {
            _opacity = opacityLevel3;
        } else if (distance > distanceLevel3 && distance <= distanceLevel2)
        {
            _opacity = opacityLevel2;
        } else if (distance > distanceLevel2 && distance <= distanceLevel1)
        {
            _opacity = opacityLevel1;
        }
        else if (distance > distanceLevel1)
        {
            _opacity = opacityLevel0;
        }
        Color color = indicatorImage.color;
        color.a = Mathf.Clamp01(_opacity); // Clamp to [0, 1] range
        indicatorImage.color = color;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
