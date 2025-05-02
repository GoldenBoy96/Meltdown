using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class IcecreamController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] IcecreamSO _icecreamSO;

        [Header("Runtime Data")]
        [SerializeField] Icecream _icecream;

        private GameViewController _gameViewController;
        public void Init(GameViewController gameViewController)
        {
            _icecream = _icecreamSO.Data;
            _gameViewController = gameViewController;
        }
    }
}