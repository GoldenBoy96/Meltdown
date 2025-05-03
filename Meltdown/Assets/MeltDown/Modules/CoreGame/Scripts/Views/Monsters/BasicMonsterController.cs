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

        private void Update()
        {
            ChaseIcecream();
        }
        public void DetectIcecream(IcecreamController chasingIcecream)
        {
            _chasingIcecream = chasingIcecream;
        }
        public void ChaseIcecream()
        {
            if (_chasingIcecream == null) return; 
            if (Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.AttackRange)
            {
                TryAttack();
            }

            if (Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.MinDectetionRange) return;
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, _chasingIcecream.transform.position, _monster.Spe * Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);
        }

        public override void TryAttack()
        {
            if (_chasingIcecream != null && Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.AttackRange && IsAttackable)
            {
                _chasingIcecream.GetDamage(_monster.Atk, _monster.AttackPower);
                CooldownAttack();
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("OnTriggerEnter2D");
            collision.TryGetComponent<IcecreamController>(out var icecream);
            if (icecream != null)
            {
                DetectIcecream(icecream);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("OnTriggerEnter2D");
            collision.TryGetComponent<IcecreamController>(out var icecream);
            if (icecream != null)
            {
                DetectIcecream(null);
            }
        }
    }
}