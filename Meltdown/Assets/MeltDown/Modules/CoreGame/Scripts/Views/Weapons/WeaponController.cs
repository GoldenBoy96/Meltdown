using UnityEngine;

namespace MeltDown
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] protected WeaponSO _weaponSO;

        [Header("Runtime Data")]
        [SerializeField] protected Weapon _weapon;

        [Header("Weapon Settings")]
        [SerializeField] private float _weaponDistance = 0.5f;
        [SerializeField] private Transform _weaponTransform;

        private GameViewController _gameViewController;

        public void Init(GameViewController gameViewController)
        {
            _weapon = _weaponSO.Data;
            _gameViewController = gameViewController;
        }

        public void HandleWeapon(Vector2 moveInput, bool isFacingRight)
        {
            if (moveInput.magnitude > 0.1f)
            {
                Vector2 direction = moveInput.normalized;

                // Flip theo hướng nhìn của nhân vật
                if (!isFacingRight)
                {
                    direction.x *= -1;
                }

                Vector2 offset = direction * _weaponDistance;
                _weaponTransform.rotation = Quaternion.Euler(0f, 0f, 0f);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _weaponTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}
