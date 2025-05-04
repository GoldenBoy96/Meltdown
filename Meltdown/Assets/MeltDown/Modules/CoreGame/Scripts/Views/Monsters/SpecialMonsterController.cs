using System.Collections;
using System.Collections.Generic;
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

        public override void Update()
        {
            //base.Update();
            ChasePlayer();
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
                    Vector2 newPosition = Vector2.MoveTowards(_rb.position, _player.transform.position, _monster.Spe * Time.fixedDeltaTime);
                    _rb.MovePosition(newPosition);
                }
            }
            else
            {
                var distance = Vector3.Distance(transform.position, _player.transform.position);
                if (distance <= _monster.MinDectetionRange
                    || distance > _monster.MaxDectetionRange) return;
                Vector2 newPosition = Vector2.MoveTowards(_rb.position, _player.transform.position, _monster.Spe * Time.fixedDeltaTime);
                _rb.MovePosition(newPosition);
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
                }
            }
        }

    }
}