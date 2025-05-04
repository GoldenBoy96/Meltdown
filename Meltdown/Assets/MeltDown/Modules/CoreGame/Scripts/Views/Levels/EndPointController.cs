using MeltDown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = MeltDown.CharacterController;

public class EndPointController : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] AlertIconController _alertIconPrefab;

    private void Start()
    {
        Debug.Log(_alertIconPrefab.gameObject);
        Debug.Log(GameViewController.Instance.AlertRect);
        var alert = Instantiate(_alertIconPrefab.gameObject, GameViewController.Instance.AlertRect.transform);
        alert.GetComponent<AlertIconController>().Init(transform, GameViewController.Instance.AlertRect, alert.GetComponent<RectTransform>());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<CharacterController>();
        if (collision.name == "Player") Debug.Log("Win Game");
        //if (player != null)
        //{
        //    Debug.Log("End Game");
        //}
    }
}
