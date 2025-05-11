using MeltDown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterExecutionArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            var monster = collision.GetComponentInParent<MonsterController>();
            if (monster != null)
            {
                monster.GetDamage(9999, 9999);
            }
        }
    }

}
