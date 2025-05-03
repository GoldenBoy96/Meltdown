using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] private CharacterSO _characterSO;

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _characterVisual;
        [SerializeField] private Transform _icecreamHolder;
        [SerializeField] private Joystick _joystick;

        [Header("Weapon Controller")]
        [SerializeField] private WeaponController _weaponController;

        [Header("Runtime Data")]
        [SerializeField] Character _character;
        [SerializeField] GameViewController _gameViewController;
        [SerializeField] IcecreamController _icecream;


        private Vector2 _moveInput;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _character = _characterSO.Data;

            if (_icecream == null) _icecream = _icecreamHolder.GetComponentInChildren<IcecreamController>();
            if (_icecream != null)
            {
                _icecream.transform.localPosition = Vector3.zero;
                _icecream.Init(this, _gameViewController);
            }
        }

        public void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;

            // Khởi tạo weapon controller
            _weaponController.Init(_gameViewController);
            if (_icecream == null) _icecream = _icecreamHolder.GetComponentInChildren<IcecreamController>();
            if (_icecream != null)
            {
                _icecream.transform.localPosition = Vector3.zero;
                _icecream.Init(this, _gameViewController);
            }
        }

        void Update()
        {
            HandleMovement();
            FlipCharacter();
            ControlWeapon();
        }

        private void HandleMovement()
        {
            _moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            _rb.velocity = _moveInput * _character.Spe;
        }

        private void FlipCharacter()
        {
            if (_moveInput.x > 0.1f)
            {
                _characterVisual.localScale = new Vector3(1, 1, 1);
            }
            else if (_moveInput.x < -0.1f)
            {
                _characterVisual.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void ControlWeapon()
        {
            _weaponController.HandleWeapon(_moveInput);
        }

        public void PickUpIcecream(IcecreamController icecream)
        {
            _icecream = icecream;
            _icecream.transform.SetParent(_icecreamHolder);
            _icecream.transform.localPosition = Vector3.zero;
            _icecream.PickUp(this);
        }

        [ContextMenu("DropIcecream")]
        public void DropIcecream()
        {
            _icecream.transform.parent = transform.parent;
            _icecream.DropDown();
            _icecream = null;
        }
    }
}
