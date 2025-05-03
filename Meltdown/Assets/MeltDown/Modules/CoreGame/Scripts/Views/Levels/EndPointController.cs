using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<CharacterController>();
        if (collision.name == "Player") Debug.Log("End Game");
        //if (player != null)
        //{
        //    Debug.Log("End Game");
        //}
    }
}
