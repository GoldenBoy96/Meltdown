using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OurUtils;
using UnityEngine;

namespace MeltDown.Modules.CoreGame.Scripts.Views
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
        [SerializeField] List<Canvas> _canvasToSetCamera = new();
        [SerializeField] CameraFollow _cameraFollow;

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
        public Transform EndPoint { get => _endPoint; set => _endPoint = value; }

        private void Start()
        {
            Application.targetFrameRate = 60;
            _mainCam = Camera.main;
            _canvasToSetCamera = FindObjectsOfType<Canvas>().ToList();
            foreach (var canvas in _canvasToSetCamera)
            {
                canvas.worldCamera = _mainCam;
            }
            _cameraFollow = _player.gameObject.GetComponent<CameraFollow>();
            StartCoroutine(WaitThenPlayBgMusic());
        }

        IEnumerator WaitThenPlayBgMusic()
        {
            yield return new WaitForSeconds(0.1f);

            // AudioManager.Instance.PlayMusic("bg_music");
        }

        private void OnEnable()
        {
            Time.timeScale = 1f;
            // AudioManager.Instance.PlayMusic("bg_music");
        }
        private void OnDisable()
        {
            // AudioManager.Instance.StopMusic();

        }

        public void SetCameraFollower(Transform follower)
        {
            _cameraFollow.SetFollower(follower);
        }    
    }
}