using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MeltDown
{
    public class BasicMonsterController : MonsterController
    {
        [Header("Child Component")]
        [SerializeField] Collider2D _detectionArea;

        [Header("Runtime Data")]
        [SerializeField] IcecreamController _chasingIcecream;


        public void DetectIcecream(IcecreamController chasingIcecream)
        {
            _chasingIcecream = chasingIcecream;
            ChaseIcecream();
        }
        public void ChaseIcecream()
        {
            if (_chasingIcecream == null) return;
            if (Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.MinDectetionRange) return;
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, _chasingIcecream.transform.position, _monster.Spe * Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);

            Debug.Log(Vector3.Distance(transform.position, _chasingIcecream.transform.position) + " | " + _monster.AttackRange);
            if (Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.AttackRange)
            {
                Attack();
            }
        }

        public void Attack()
        {
            if (_chasingIcecream != null && Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.AttackRange)
            {
                _chasingIcecream.GetDamage(_monster.Atk, _monster.AttackPower);
            }
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("OnTriggerEnter2D");
            collision.TryGetComponent<IcecreamController>(out var icecream);
            Debug.Log(icecream);
            if (icecream != null)
            {
                DetectIcecream(icecream);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            collision.TryGetComponent<IcecreamController>(out var icecream);
            if (icecream != null)
            {
                DetectIcecream(null);
            }
        }
    }
}