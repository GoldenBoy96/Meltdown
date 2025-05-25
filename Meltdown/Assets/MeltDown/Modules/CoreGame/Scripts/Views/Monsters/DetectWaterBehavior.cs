using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MeltDown
{
    public class DetectWaterBehavior : MonoBehaviour
    {
        [SerializeField] MonsterController _ownMonster;

        private void Start()
        {
            _ownMonster = GetComponentInParent<MonsterController>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<MonsterExecutionArea>(out var monsterExecutionArea))
            {
                Debug.Log("DetectWaterBehavior " + _ownMonster);
            }
        }
    }
}