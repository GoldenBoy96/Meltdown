using OurUtils;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class GameViewController : SingletonMono<GameViewController>
    {
        [Header("Prefab")]
        [SerializeField] MonsterController _monsterPrefab;

        [Header("Child Component")]
        [SerializeField] Transform _monsterHolder;

        [Header("Runtime Component")]
        [SerializeField] CharacterController _player;
        [SerializeField] RandomSpawner _randomSpawner;
        List<MonsterController> _monsterList = new();

        public CharacterController Player { get => _player; set => _player = value; }
        public RandomSpawner RandomSpawner { get => _randomSpawner; set => _randomSpawner = value; }
        public List<MonsterController> MonsterList { get => _monsterList; set => _monsterList = value; }
    }
}