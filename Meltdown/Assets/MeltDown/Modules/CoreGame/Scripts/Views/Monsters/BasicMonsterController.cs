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
        [SerializeField] protected IcecreamController _chasingIcecream;

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            ChaseIcecream();
        }
        public virtual void DetectIcecream(IcecreamController chasingIcecream)
        {
            _chasingIcecream = chasingIcecream;
            if (_chasingIcecream != null && _monsterCamp != null)
            {
                _monsterCamp.TriggerHordeAttack(chasingIcecream);
            }
        }
        public virtual void ChaseIcecream()
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
            Vector3 direction = (_chasingIcecream.transform.position - (Vector3)_rb.position).normalized;
            Vector3 force = direction * _monster.Spe / 100;
            _rb.AddForce(force, ForceMode2D.Impulse);
        }

        public override void TryAttack()
        {
            //Debug.Log(_chasingIcecream + "\n"
            //    + Vector3.Distance(transform.position, _chasingIcecream.transform.position) + " | " + _monster.AttackRange
            //    + IsAttackable);
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