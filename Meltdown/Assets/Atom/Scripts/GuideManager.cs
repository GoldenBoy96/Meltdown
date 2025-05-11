using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    [SerializeField] private GameObject _guidePanel;

    private Button openButton;
    private Button _closeButton;


    public void OpenPanelGuide()
    {
        _guidePanel.SetActive(true);
    }

    public void ClosePanelGuide()
    {
        _guidePanel.SetActive(false);
    }


}
