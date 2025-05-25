using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class MonsterExecutionArea : MonoBehaviour
    {
        [SerializeField] private GameObject particleEffect;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                var monster = collision.GetComponentInParent<MonsterController>();
                if (monster != null)
                {
                    monster.GetDamage(9999, 9999);
                    if (particleEffect != null)
                    {
                        Instantiate(particleEffect, monster.transform.position, Quaternion.identity);
                    }
                }
            }
        }

    }
}