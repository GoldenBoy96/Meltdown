using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

namespace MeltDown
{
    public class MonsterController : MonoBehaviour, IGetDamageable
    {
        [Header("Init Data")]
        [SerializeField] protected MonsterSO _monsterSO;

        [Header("Child Component")]
        [SerializeField] protected Rigidbody2D _rb;

        [Header("Runtime Data")]
        [SerializeField] protected Monster _monster;
        [SerializeField] protected bool _isAttackable = true;
        [SerializeField] protected MonsterCampController _monsterCamp;
        [SerializeField] protected Vector3 _originalPosition;
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

        public virtual void Update()
        {
            CheckReturnToCamp();
            ReturnToCamp();
        }

        public void RegisterCamp(MonsterCampController monsterCamp)
        {
            _monsterCamp = monsterCamp;
            if (_monsterCamp != null)
            {
                if (Vector3.Distance(transform.position, _monsterCamp.transform.position) > _monsterCamp.MonsterCamp.Range)
                {
                    // Direction from camp to monster
                    Vector3 direction = (transform.position - _monsterCamp.transform.position).normalized;

                    // New position exactly Range units from the camp
                    transform.position = _monsterCamp.transform.position + direction * _monsterCamp.MonsterCamp.Range;
                }
                _originalPosition = transform.position;
            }
        }

        public void CheckReturnToCamp()
        {
            if (_monsterCamp == null) return;
            if (Vector3.Distance(transform.transform.position, _monsterCamp.transform.position) > _monsterCamp.MonsterCamp.Range)
            {
                _isReturnToCamp = true;
            }
        }
        public void ReturnToCamp()
        {
            if (_monsterCamp == null) return;
            if (_isReturnToCamp)
            {
                transform.position = Vector3.MoveTowards(transform.position, _originalPosition, _monster.Spe * Time.deltaTime);
                if (Vector3.Distance(transform.transform.position, _originalPosition) <= 0.1f)
                {
                    _isReturnToCamp = false;
                }
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

        public void GetDamage(float atk, float power)
        {
            _monster.Hp -= IGetDamageable.CalculateTrueDamage(atk, power, _monster.Def);
            if (_monster.Hp <= 0)
            {
                _monster.Hp = 0;
                Destroy(gameObject);
            }
            if (_monster.Hp > _monster.MaxHp) _monster.Hp = _monster.MaxHp;
        }
    }
}