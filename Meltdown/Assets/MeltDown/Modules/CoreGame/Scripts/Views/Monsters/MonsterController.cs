using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MeltDown
{
    public class MonsterController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] protected MonsterSO _monsterSO;

        [Header("Child Component")]
        [SerializeField] protected Rigidbody2D _rb;

        [Header("Runtime Data")]
        [SerializeField] protected Monster _monster;
        [SerializeField] protected bool _isAttackable = true;
        [SerializeField] protected MonsterCampController _monsterCamp;
        [SerializeField] protected bool _isReturnToCamp = false;

        protected GameViewController _gameViewController;

        public bool IsAttackable { get => _isAttackable; private set => _isAttackable = value; }

        private void Start()
        {
            _monster = _monsterSO.Data;
        }
        public virtual void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;
        }

        public void RegisterCamp(MonsterCampController monsterCamp)
        {
            _monsterCamp = monsterCamp;
            if (Vector3.Distance(transform.position, _monsterCamp.transform.position) > _monsterCamp.MonsterCamp.Range)
            {
                // Direction from camp to monster
                Vector3 direction = (transform.position - _monsterCamp.transform.position).normalized;

                // New position exactly Range units from the camp
                transform.position = _monsterCamp.transform.position + direction * _monsterCamp.MonsterCamp.Range;
            }
        }
        public virtual void TryAttack()
        {
        }

        public virtual void CooldownAttack()
        {
            StartCoroutine(CooldownAttackCoroutine());
        }
        protected IEnumerator CooldownAttackCoroutine()
        {
            _isAttackable = false;
            yield return new WaitForSeconds(_monster.AttackCooldown);
            _isAttackable = true;
        }
    }
}