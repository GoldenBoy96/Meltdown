using OurUtils;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class GameViewController : SingletonMono<GameViewController>
    {
        [Header("Prefab")]
        [SerializeField] MonsterController _monsterPrefab;
        [SerializeField] RectTransform _alertIconPrefab;

        [Header("Child Component")]
        [SerializeField] Transform _monsterHolder;
        [SerializeField] Transform _startPoint;
        [SerializeField] Transform _endPoint;
        [SerializeField] RectTransform _alertRect;
        [SerializeField] Camera _mainCam;

        [Header("Runtime Component")]
        [SerializeField] CharacterController _player;
        [SerializeField] RandomSpawner _randomSpawner;
        List<MonsterController> _monsterList = new();

        public CharacterController Player { get => _player; set => _player = value; }
        public RandomSpawner RandomSpawner { get => _randomSpawner; set => _randomSpawner = value; }
        public List<MonsterController> MonsterList { get => _monsterList; set => _monsterList = value; }
        public RectTransform AlertIconPrefab { get => _alertIconPrefab; set => _alertIconPrefab = value; }
        public RectTransform AlertRect { get => _alertRect; set => _alertRect = value; }
        public Camera MainCam { get => _mainCam; set => _mainCam = value; }

        private void Start()
        {
            _mainCam = Camera.main;
        }
    }
}