using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialComponent : MonoBehaviour
{
    [SerializeField] private Button confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        confirmButton.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }
}
