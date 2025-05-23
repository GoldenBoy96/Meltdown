using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using MeltDown.Modules.CoreGame.Scripts.Views;
using UnityEngine;

namespace MeltDown
{
    public class MonsterController : MonoBehaviour, IGetDamageable
    {
        [Header("Init Data")]
        [SerializeField] protected MonsterSO _monsterSO;

        [Header("Child Component")]
        [SerializeField] protected Rigidbody2D _rb;
        [SerializeField] protected AlertIconController _alertIcon;
        [SerializeField] protected RectTransform _alert;
        [SerializeField] HealthBarController _healthBar;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] Sprite _alertIconSprite;

        [Header("Runtime Data")]
        [SerializeField] protected Monster _monster;
        [SerializeField] protected bool _isAttackable = true;
        [SerializeField] protected MonsterCampController _monsterCamp;
        [SerializeField] protected Vector3 _originalPosition;

        protected GameViewController _gameViewController;

        public bool IsAttackable { get => _isAttackable; private set => _isAttackable = value; }

        public virtual void Start()
        {
            _monster = _monsterSO.Data;
            Init(GameViewController.Instance);
        }
        public virtual void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;
            _alert = Instantiate(_gameViewController.AlertIconPrefab, _gameViewController.AlertRect.transform);
            _alert.GetComponent<AlertIconController>().Init(transform, _gameViewController.AlertRect, _alert, _alertIconSprite);
        }

        public void Update()
        {
            _healthBar.UpdateHpBar(_monster.Hp, _monster.MaxHp);
        }

        public virtual void FixedUpdate()
        {
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

        public void ReturnToCamp()
        {
            if (_monsterCamp == null) return;
            if (!_monsterCamp.IsTriggerHordeAttack)
            {
                if (Vector3.Distance(transform.transform.position, _originalPosition) > 0.1f)
                {
                    _rb.totalForce = Vector3.zero;
                    Vector3 direction = (_originalPosition - transform.position).normalized;
                    Vector3 force = direction * _monster.Spe / 100;
                    _rb.AddForce(force, ForceMode2D.Impulse);
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

        private Coroutine _deadCoroutine;
        public void GetDamage(float atk, float power)
        {
            _monster.Hp -= IGetDamageable.CalculateTrueDamage(atk, power, _monster.Def);
            if (_monster.Hp <= 0)
            {
                _monster.Hp = 0;
                Destroy(_alert.gameObject);
                if (_deadCoroutine == null) StartCoroutine(DeadCoroutine());
            }
            if (_monster.Hp > _monster.MaxHp) _monster.Hp = _monster.MaxHp;
        }

        IEnumerator DeadCoroutine()
        {
            _spriteRenderer.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
        }
    }
}