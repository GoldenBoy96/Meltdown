using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] WeaponSO _weaponSO;

        [Header("Runtime Data")]
        [SerializeField] Weapon _weapon;

        private GameViewController _gameViewController;
        public void Init(GameViewController gameViewController)
        {
            _weapon = _weaponSO.Data;
            _gameViewController = gameViewController;
        }
    }
}