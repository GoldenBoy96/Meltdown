using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class MonsterController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] MonsterSO _monsterSO;

        [Header("Runtime Data")]
        [SerializeField] Monster _monster;

        private GameViewController _gameViewController;
        public void Init(GameViewController gameViewController)
        {
            _monster = _monsterSO.Data;
            _gameViewController = gameViewController;
        }
    }
}