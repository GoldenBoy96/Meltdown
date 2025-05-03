using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float _weaponDistance = 0.5f; // Khoảng cách từ nhân vật
        [SerializeField] private Transform _weaponTransform;

        private GameViewController _gameViewController;

        public void Init(GameViewController gameViewController)
        {
            _weapon = _weaponSO.Data;
            _gameViewController = gameViewController;
        }

        public void HandleWeapon(Vector2 moveInput)
        {
            if (moveInput.magnitude > 0.1f)
            {
                // Xác định vị trí của vũ khí theo hướng joystick
                Vector2 offset = moveInput.normalized * _weaponDistance;
                _weaponTransform.localPosition = offset;

                // Xoay vũ khí theo hướng joystick
                float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
                _weaponTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}
