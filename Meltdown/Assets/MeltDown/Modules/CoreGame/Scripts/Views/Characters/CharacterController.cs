using Template;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MeltDown
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] private CharacterSO _characterSO;

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private WeaponController _weapon;
        [SerializeField] private Transform _characterVisual;
        [SerializeField] private Transform _icecreamHolder;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Animator _animator;

        [Header("Weapon Controller")]
        [SerializeField] private WeaponController _weaponController;

        [Header("Weapon Parameters")]
        [SerializeField] private float _weaponDistance = 0.1f;

        [Header("Runtime Data")]
        [SerializeField] Character _character;
        [SerializeField] GameViewController _gameViewController;
        [SerializeField] IcecreamController _icecream;
        //[SerializeField] IcecreamController _weapon;


        private Vector2 _moveInput;
        private bool _isFacingRight = true;

        public Character Character { get => _character; private set => _character = value; }
        public IcecreamController Icecream { get => _icecream; private set => _icecream = value; }

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            Character = _characterSO.Data;

            if (_icecream == null) _icecream = _icecreamHolder.GetComponentInChildren<IcecreamController>();
            if (_icecream != null)
            {
                _icecream.transform.localPosition = Vector3.zero;
                _icecream.Init(this, _gameViewController);
            }

            Init(GameViewController.Instance);
        }

        public void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;

            // Khởi tạo weapon controller
            //_weaponController.Init(this, _gameViewController);
            if (_icecream == null) _icecream = _icecreamHolder.GetComponentInChildren<IcecreamController>();
            if (_icecream != null)
            {
                _icecream.transform.localPosition = Vector3.zero;
                _icecream.Init(this, _gameViewController);
            }
            AnimatorHelper.PlayAnimation(_animator, CoreGameConstants.Idle);
        }

        void FixedUpdate()
        {
            HandleMovement();
            HandleWeaponMovement();
            FlipCharacter();
        }

        // Xử lý di chuyển nhân vật
        private void HandleMovement()
        {
            _rb.totalForce = Vector3.zero;
            _moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical).normalized;
            _rb.AddForce(_moveInput * Character.Spe / 100, ForceMode2D.Impulse);
            if (_moveInput != Vector2.zero)
            {
                AnimatorHelper.PlayAnimation(_animator, CoreGameConstants.Run);
            }
            else
            {
                AnimatorHelper.PlayAnimation(_animator, CoreGameConstants.Idle);
            }
        }

        private void HandleWeaponMovement()
        {
            if (_moveInput.magnitude > 0.1f)
            {
                // Tính toán vị trí của vũ khí dựa vào hướng joystick
                Vector2 weaponOffset = _moveInput.normalized * _weaponDistance;
                _weaponHolder.localPosition = weaponOffset;

                // Xoay vũ khí về hướng của joystick
                float angle = Mathf.Atan2(weaponOffset.y, weaponOffset.x) * Mathf.Rad2Deg;
                _weaponHolder.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }

        private void FlipCharacter()
        {
            if (_moveInput.x > 0.1f)
            {
                // Flip sang phải
                _characterVisual.localScale = new Vector3(1, 1, 1);
                //_weapon.localScale = new Vector3(0.3f, 0.3f, 1);
            }
            else if (_moveInput.x < -0.1f)
            {
                // Flip sang trái
                _characterVisual.localScale = new Vector3(-1, 1, 1);
                //_weapon.localScale = new Vector3(-0.3f, 0.3f, 1);
            }
        }




        public void PickUpIcecream(IcecreamController icecream)
        {
            _icecream = icecream;
            _icecream.transform.SetParent(_icecreamHolder);
            _icecream.transform.localPosition = Vector3.zero;
            _icecream.PickUp(this);
        }

        public void PickUpWeapon(WeaponController weaponController)
        {
            if (_weapon != null)
            {
                _weapon.DropDown();
            }
            _weapon = weaponController;
            _weapon.transform.SetParent(_weaponHolder);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.PickUp(this);
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
