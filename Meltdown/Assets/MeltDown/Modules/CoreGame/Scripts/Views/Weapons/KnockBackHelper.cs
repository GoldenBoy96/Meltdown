using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class KnockBackHelper : MonoBehaviour
    {
        public static void Knockback(Transform attacker, Rigidbody2D targetRg, float knockbackForce)
        {
            targetRg.totalForce = Vector3.zero;
            Vector3 direction = (targetRg.transform.position - attacker.transform.position).normalized;
            targetRg.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }
}