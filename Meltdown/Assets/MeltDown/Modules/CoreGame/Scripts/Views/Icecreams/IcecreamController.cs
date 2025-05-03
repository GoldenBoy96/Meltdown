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

        private GameViewController _gameViewController;

        private void OnEnable()
        {
            _icecream = _icecreamSO.Data;
        }
        public void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;
        }

        public void GetDamage(float atk, float power)
        {
            Debug.Log(IGetDamageable.CalculateTrueDamage(atk, power, _icecream.Def));
            _icecream.Hp -= IGetDamageable.CalculateTrueDamage(atk, power, _icecream.Def);
            if (_icecream.Hp < 0) _icecream.Hp = 0;
            if (_icecream.Hp > _icecream.MaxHp) _icecream.Hp = _icecream.MaxHp;
        }

    }
}