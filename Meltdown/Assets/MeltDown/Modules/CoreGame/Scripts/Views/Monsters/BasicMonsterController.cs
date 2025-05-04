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

        public override void Update()
        {
            base.Update();
            ChaseIcecream();
        }
        public void DetectIcecream(IcecreamController chasingIcecream)
        {
            _chasingIcecream = chasingIcecream;
            if (_chasingIcecream != null && _monsterCamp != null)
            {
                _monsterCamp.TriggerHordeAttack(chasingIcecream);
            }
        }
        public void ChaseIcecream()
        {
            if (_monsterCamp != null)
            {
                if (_monsterCamp.IsTriggerHordeAttack)
                {
                    _chasingIcecream = _monsterCamp.IcecreamToChase;
                }
            } 
            if (_chasingIcecream == null) return;
            //Debug.Log(Vector3.Distance(transform.position, _chasingIcecream.transform.position));
            if (Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.AttackRange)
            {
                //Debug.Log("TryAttack");
                TryAttack();
            }

            if (Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.MinDectetionRange) return;
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, _chasingIcecream.transform.position, _monster.Spe * Time.fixedDeltaTime);
            _rb.MovePosition(newPosition);
        }

        public override void TryAttack()
        {
            Debug.Log(_chasingIcecream + "\n"
                + Vector3.Distance(transform.position, _chasingIcecream.transform.position) + " | " + _monster.AttackRange
                + IsAttackable);
            if (_chasingIcecream != null && Vector3.Distance(transform.position, _chasingIcecream.transform.position) <= _monster.AttackRange && IsAttackable)
            {
                _chasingIcecream.GetDamage(_monster.Atk, _monster.AttackPower);
                CooldownAttack();
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Icecream"))
            {
                Debug.Log(collision);
                var icecream = collision.GetComponentInChildren<IcecreamController>();

                Debug.Log(collision + " | " + icecream);
                DetectIcecream(icecream);
            }

        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Icecream"))
            {
                DetectIcecream(null);
            }
        }
    }
}