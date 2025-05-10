using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MeltDown.Modules.CoreGame.Scripts.Views;
using Template;
using UnityEngine;

namespace MeltDown
{
    public class SpecialMonsterController : BasicMonsterController
    {
        [SerializeField] CharacterController _player;

        public override void Start()
        {
            base.Start();
            _player = GameViewController.Instance.Player;
        }

        public override void FixedUpdate()
        {
            //base.Update();
            ChasePlayer();
            ReturnToCamp();
        }
        public override void ChaseIcecream()
        {
            //base.ChaseIcecream();
        }

        public override void DetectIcecream(IcecreamController chasingIcecream)
        {
            //base.DetectIcecream(chasingIcecream);
        }

        public void ChasePlayer()
        {
            if (_monsterCamp != null)
            {
                if (_monsterCamp.IsTriggerHordeAttack)
                {
                    if (Vector3.Distance(transform.position, _player.transform.position) <= _monster.MinDectetionRange) return;
                    Vector3 direction = (_player.transform.position - (Vector3)_rb.position).normalized;
                    Vector3 force = direction * _monster.Spe / 100;
                    _rb.AddForce(force, ForceMode2D.Impulse);
                }
            }
            else
            {
                var distance = Vector3.Distance(transform.position, _player.transform.position);
                if (distance <= _monster.MinDectetionRange
                    || distance > _monster.MaxDectetionRange) return;
                Vector3 direction = (_player.transform.position - (Vector3)_rb.position).normalized;
                Vector3 force = direction * _monster.Spe / 100;
                _rb.AddForce(force, ForceMode2D.Impulse);
            }
            //Debug.Log(Vector3.Distance(transform.position, _chasingIcecream.transform.position));
            if (Vector3.Distance(transform.position, _player.transform.position) <= _monster.AttackRange)
            {
                //Debug.Log("TryAttack");
                TryAttack();
            }

        }

        public override void TryAttack()
        {
            if (_player.Icecream != null)
            {
                if (Vector3.Distance(transform.position, _player.transform.position) <= _monster.AttackRange && IsAttackable)
                {
                    _player.Icecream.GetDamage(_monster.Atk, _monster.AttackPower);
                    CooldownAttack();
                    AudioManager.Instance.PlaySound("monster_attack");
                }
            } else
            {
                if (Vector3.Distance(transform.position, _player.transform.position) <= _monster.AttackRange && IsAttackable)
                {
                    StartCoroutine(AttackCoroutine());
                    AudioManager.Instance.PlaySound("monster_attack");
                }
            }
        }
        IEnumerator AttackCoroutine()
        {
            _isAttacking = true;
            KnockBackHelper.Knockback(transform, _player.GetComponent<Rigidbody2D>(), 5);
            CooldownAttack();

            Vector3 direction = (_player.transform.position - (Vector3)_rb.position).normalized;
            Vector3 force = -direction * _monster.Spe * 25 / 100;
            _rb.AddForce(force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
            AudioManager.Instance.PlaySound("monster_attack");
            _isAttacking = false;
        }

    }
}