using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Template;
using UnityEngine;

namespace MeltDown
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] WeaponSO _weaponSO;
        [SerializeField] float _pickUpTime;


        [Header("ChildComponent")]
        [SerializeField] SpriteRenderer _sprite;
        [SerializeField] GameObject _slashEffect;
        [SerializeField] HealthBarController _pickUpCircle;

        [Header("Runtime Data")]
        [SerializeField] Weapon _weapon;
        [SerializeField] HashSet<MonsterController> _inRangeMonsterController = new();
        [SerializeField] bool _isAttackable = true;
        [SerializeField] bool _isPickingUp = false;
        [SerializeField] bool _enablePickUp = false;
        [SerializeField] float _currentPickUpTime;

        private GameViewController _gameViewController;
        private CharacterController _holderCharacter;
        private CharacterController _pickUpCharacter;

        private void OnEnable()
        {
            if (_holderCharacter == null)
            {
                _enablePickUp = true;
            }
        }

        private void Start()
        {
            Init(GameViewController.Instance);
        }
        public void Init(GameViewController gameViewController)
        {
            _weapon = _weaponSO.Data;
            _gameViewController = gameViewController;
        }
        public void Init(CharacterController characterController, GameViewController gameViewController)
        {
            _weapon = _weaponSO.Data;
            _gameViewController = gameViewController;
            _holderCharacter = characterController;
        }

        private void Update()
        {
            TryAttack();
            CheckPickUp();
        }

        public void TryAttack()
        {
            if (_isAttackable && _inRangeMonsterController.Count > 0)
            {
                foreach (var monster in _inRangeMonsterController.ToList())
                {
                    monster.GetDamage(_holderCharacter.Character.Atk, _weapon.Power);
                    KnockBackHelper.Knockback(GameViewController.Instance.Player.transform, monster.GetComponent<Rigidbody2D>(), _weapon.KnockBackForce);
                }

                AudioManager.Instance.PlaySound("player_attack");
                CooldownAttack();
                StartCoroutine(AttackEffectCoroutine());
            }
        }

        public void CooldownAttack()
        {
            StartCoroutine(CooldownAttackCoroutine());
        }

        IEnumerator CooldownAttackCoroutine()
        {
            _isAttackable = false;
            yield return new WaitForSeconds(_weapon.Cooldown);
            _isAttackable = true;
        }

        IEnumerator AttackEffectCoroutine()
        {
            var originRotation = _sprite.transform.localRotation;
            var originScale = _sprite.transform.localScale;
            _slashEffect.SetActive(true);
            _sprite.transform.DOLocalRotate(originRotation.eulerAngles + new Vector3(0, 0, 60), 0.2f);
            _sprite.transform.DOScale(originScale * 1.5f, 0.2f);
            yield return new WaitForSeconds(0.2f);
            _sprite.transform.DOLocalRotate(originRotation.eulerAngles + new Vector3(0, 0, 0), 0.1f);
            _sprite.transform.DOScale(originScale, 0.1f);
            _slashEffect.SetActive(false);
        }


        public void CheckPickUp()
        {
            if (!_enablePickUp) return;
            _pickUpCircle.gameObject.SetActive(true);
            if (_isPickingUp)
            {
                _currentPickUpTime += Time.deltaTime;
                if (_currentPickUpTime > _pickUpTime)
                {
                    _currentPickUpTime = 0;
                    _pickUpCharacter.PickUpWeapon(this);
                    _pickUpCircle.gameObject.SetActive(false);
                }
            }
            else
            {
                _currentPickUpTime -= Time.deltaTime;
                if (_currentPickUpTime < 0) _currentPickUpTime = 0;
            }
            _pickUpCircle.UpdateHpBar(_currentPickUpTime, _pickUpTime);
        }

        public void PickUp(CharacterController holderCharacter)
        {
            _enablePickUp = false;
            _holderCharacter = holderCharacter;
            transform.DOLocalRotate(new Vector3(0, 0, -90), 0.1f);
        }

        public void DropDown()
        {
            transform.SetParent(_holderCharacter.transform.parent);
            _holderCharacter = null;
            _enablePickUp = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent<CharacterController>(out var player);
            if (player != null)
            {
                _pickUpCharacter = player;
                _isPickingUp = true;
            }
            else if (collision.CompareTag("Monster"))
            {
                var monster = collision.gameObject.GetComponentInParent<MonsterController>();
                if (monster != null)
                {
                    _inRangeMonsterController.Add(monster);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            collision.TryGetComponent<CharacterController>(out var player);
            if (player != null)
            {
                _isPickingUp = false;
            }
            else if (collision.CompareTag("Monster"))
            {
                var monster = collision.gameObject.GetComponentInParent<MonsterController>();
                if (monster != null)
                {
                    _inRangeMonsterController.Remove(monster);
                }
            }
        }
    }
}