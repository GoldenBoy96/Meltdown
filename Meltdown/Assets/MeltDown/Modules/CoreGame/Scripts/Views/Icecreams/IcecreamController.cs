using System.Collections;
using System.Collections.Generic;
using MeltDown.Modules.CoreGame.Scripts.Views;
using Template;
using UnityEngine;

namespace MeltDown
{
    public class IcecreamController : MonoBehaviour, IGetDamageable
    {
        [Header("Prefab")]
        [SerializeField] AlertIconController _alertIconPrefab;

        [Header("Init Data")]
        [SerializeField] IcecreamSO _icecreamSO;

        [Header("Child Component")]
        [SerializeField] HealthBarController _healthBar;
        [SerializeField] HealthBarController _pickUpCircle;
        [SerializeField] Sprite _alertIconSprite;

        [Header("Runtime Data")]
        [SerializeField] Icecream _icecream;
        [SerializeField] bool _isPickingUp = false;
        [SerializeField] bool _enablePickUp = false;
        [SerializeField] float _currentPickUpTime;
        [SerializeField] bool _isMeltDown = true;

        [Header("Ui Lose")]
        [SerializeField] GameObject _loseGamePanel;

        private GameViewController _gameViewController;
        private CharacterController _holderCharacter;
        private CharacterController _pickUpCharacter;
        bool _isLose = false;

        public CharacterController HolderCharacter { get => _holderCharacter; private set => _holderCharacter = value; }

        private void OnEnable()
        {
            _icecream = _icecreamSO.Data;
            if (_holderCharacter == null) _enablePickUp = true;
            StartCoroutine(MeltDownCoroutine());
        }
        public void Init(CharacterController holderCharacter, GameViewController gameViewController)
        {
            _holderCharacter = holderCharacter;
            _gameViewController = gameViewController;
            if (_holderCharacter == null) _enablePickUp = true;
            else _enablePickUp = false;
            var alert = Instantiate(_alertIconPrefab.gameObject, GameViewController.Instance.AlertRect.transform);
            alert.GetComponent<AlertIconController>().Init(transform, GameViewController.Instance.AlertRect, alert.GetComponent<RectTransform>(), _alertIconSprite);
        }

        public void GetDamage(float atk, float power)
        {
            if (!_isLose)
            {
                if (_holderCharacter != null)
                {
                    _holderCharacter.DropIcecream();
                    _holderCharacter = null;
                }
                else
                {
                    _icecream.Hp -= IGetDamageable.CalculateTrueDamage(atk, power, _icecream.Def);
                    if (_icecream.Hp <= 0)
                    {
                        _icecream.Hp = 0;
                        Debug.Log("Lose Game");

                         _loseGamePanel.SetActive(true);
                        AudioManager.Instance.PlaySound("lose");
                        AudioManager.Instance.StopMusic();
                        _isLose = true;
                    }

                    if (_icecream.Hp > _icecream.MaxHp) _icecream.Hp = _icecream.MaxHp;
                }
            }
        }

        private void Update()
        {
            CheckPickUp();
            _healthBar.UpdateHpBar(_icecream.Hp, _icecream.MaxHp);
        }

        public void CheckPickUp()
        {
            if (!_enablePickUp) return;
            _pickUpCircle.gameObject.SetActive(true);
            if (_isPickingUp)
            {
                _currentPickUpTime += Time.deltaTime;
                if (_currentPickUpTime > _icecream.PickUpTime)
                {
                    _currentPickUpTime = 0;
                    _pickUpCharacter.PickUpIcecream(this);
                    _pickUpCircle.gameObject.SetActive(false);
                }
            }
            else
            {
                _currentPickUpTime -= Time.deltaTime;
                if (_currentPickUpTime < 0) _currentPickUpTime = 0;
            }
            _pickUpCircle.UpdateHpBar(_currentPickUpTime, _icecream.PickUpTime);
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

        public void Heal(float hpToHeal)
        {
            _icecream.Hp += hpToHeal;
            if (_icecream.Hp < 0)
            {
                _icecream.Hp = 0;
            }
            if (_icecream.Hp > _icecream.MaxHp) _icecream.Hp = _icecream.MaxHp;
        }

        IEnumerator MeltDownCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            if (_isMeltDown)
            {
                _icecream.Hp -= _icecream.MeltDownPerSecond / 10f;
                if (_icecream.Hp < 0)
                {
                    _icecream.Hp = 0;
                    Debug.Log("Lose Game");
                }
                if (_icecream.Hp > _icecream.MaxHp) _icecream.Hp = _icecream.MaxHp;
            }
            StartCoroutine(MeltDownCoroutine());
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
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