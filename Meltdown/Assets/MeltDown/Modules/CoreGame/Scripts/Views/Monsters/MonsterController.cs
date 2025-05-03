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