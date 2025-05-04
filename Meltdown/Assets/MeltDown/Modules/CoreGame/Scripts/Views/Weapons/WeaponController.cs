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


        [Header("ChildComponent")]
        [SerializeField] SpriteRenderer _sprite;
        [SerializeField] GameObject _slashEffect;

        [Header("Runtime Data")]
        [SerializeField] Weapon _weapon;
        [SerializeField] HashSet<MonsterController> _inRangeMonsterController = new();
        [SerializeField] bool _isAttackable = true;

        private GameViewController _gameViewController;
        private CharacterController _characterController;
        public void Init(CharacterController characterController, GameViewController gameViewController)
        {
            _weapon = _weaponSO.Data;
            _gameViewController = gameViewController;
            _characterController = characterController;
        }

        private void Update()
        {
            TryAttack();
        }

        public void TryAttack()
        {
            if(_isAttackable && _inRangeMonsterController.Count > 0)
            {
                foreach (var monster in _inRangeMonsterController.ToList())
                {
                    monster.GetDamage(_characterController.Character.Atk, _weapon.Power);
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
            _sprite.transform.DOScale(originScale* 1.5f, 0.2f);
            yield return new WaitForSeconds(0.2f);
            _sprite.transform.DOLocalRotate(originRotation.eulerAngles + new Vector3(0, 0, 0), 0.1f);
            _sprite.transform.DOScale(originScale, 0.1f);
            _slashEffect.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
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
            if (collision.CompareTag("Monster"))
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