using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class IcecreamController : MonoBehaviour, IGetDamageable
    {
        [Header("Init Data")]
        [SerializeField] IcecreamSO _icecreamSO;

        [Header("Runtime Data")]
        [SerializeField] Icecream _icecream;
        [SerializeField] bool _isPickingUp = false;
        [SerializeField] bool _enablePickUp = false;
        [SerializeField] float _currentPickUpTime;

        private GameViewController _gameViewController;
        private CharacterController _holderCharacter;
        private CharacterController _pickUpCharacter;

        private void OnEnable()
        {
            _icecream = _icecreamSO.Data;
            if (_holderCharacter == null) _enablePickUp = true;
        }
        public void Init(CharacterController holderCharacter, GameViewController gameViewController)
        {
            _holderCharacter = holderCharacter;
            _gameViewController = gameViewController;
            if (_holderCharacter == null) _enablePickUp = true;
            else _enablePickUp = false;
        }

        public void GetDamage(float atk, float power)
        {
            if (_holderCharacter != null)
            {
                _holderCharacter.DropIcecream();
                _holderCharacter = null;
            }
            else
            {
                _icecream.Hp -= IGetDamageable.CalculateTrueDamage(atk, power, _icecream.Def);
                if (_icecream.Hp < 0) _icecream.Hp = 0;
                if (_icecream.Hp > _icecream.MaxHp) _icecream.Hp = _icecream.MaxHp;
            }
        }

        private void Update()
        {
            CheckPickUp();
        }

        public void CheckPickUp()
        {
            if (!_enablePickUp) return;
            if (_isPickingUp)
            {
                _currentPickUpTime += Time.deltaTime;
                if (_currentPickUpTime > _icecream.PickUpTime)
                {
                    _currentPickUpTime = 0;
                    _pickUpCharacter.PickUpIcecream(this);
                }
            }
            else
            {
                _currentPickUpTime -= Time.deltaTime;
                if (_currentPickUpTime < 0) _currentPickUpTime = 0;
            }
        }

        public void PickUp(CharacterController holderCharacter)
        {
            _enablePickUp = false;
            _holderCharacter = holderCharacter;
        }

        public void DropDown()
        {
            _enablePickUp = true;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("OnTriggerEnter2D");
            collision.TryGetComponent<CharacterController>(out var player);
            if (player != null)
            {
                _pickUpCharacter = player;
                _isPickingUp = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("OnTriggerExit2D");
            collision.TryGetComponent<CharacterController>(out var player);
            if (player != null)
            {
                _isPickingUp = false;
            }
        }

    }
}