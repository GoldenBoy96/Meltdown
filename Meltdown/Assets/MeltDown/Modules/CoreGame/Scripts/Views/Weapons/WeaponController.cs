using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace MeltDown
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] WeaponSO _weaponSO;

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
                }
                CooldownAttack();
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

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Debug.Log("OnTriggerEnter2D");
        //    collision.gameObject.TryGetComponent<MonsterController>(out var monster);
        //    if (monster != null)
        //    {
        //        Debug.Log(monster);
        //        _inRangeMonsterController.Add(monster);
        //    }
        //}
        //private void OnCollisionExit(Collision collision)
        //{
        //    Debug.Log("OnTriggerEnter2D");
        //    collision.gameObject.TryGetComponent<MonsterController>(out var monster);
        //    if (monster != null)
        //    {
        //        _inRangeMonsterController.Remove(monster);
        //    }
        //}
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