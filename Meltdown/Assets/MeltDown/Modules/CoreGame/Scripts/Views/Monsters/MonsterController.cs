using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MeltDown
{
    public class MonsterController : MonoBehaviour
    {
        [Header("Init Data")]
        [SerializeField] protected MonsterSO _monsterSO;

        [Header("Child Component")]
        [SerializeField] protected Rigidbody2D _rb;

        [Header("Runtime Data")]
        [SerializeField] protected Monster _monster;

        protected GameViewController _gameViewController;

        private void Start()
        {
            _monster = _monsterSO.Data;
        }
        public virtual void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;
        }

    }
}