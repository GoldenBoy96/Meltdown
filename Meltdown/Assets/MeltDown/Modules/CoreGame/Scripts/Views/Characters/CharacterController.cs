using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class CharacterController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] CharacterSO _characterSO;

        [Header("Runtime Data")]
        [SerializeField] Character _character;

        private GameViewController _gameViewController;
        public void Init(GameViewController gameViewController)
        {
            _character = _characterSO.Data;
            _gameViewController = gameViewController;
        }
    }
}