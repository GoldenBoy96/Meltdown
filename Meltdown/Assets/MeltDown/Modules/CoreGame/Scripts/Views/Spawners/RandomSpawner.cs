using System.Collections;
using System.Collections.Generic;
using MeltDown.Modules.CoreGame.Scripts.Views;
using UnityEngine;

namespace MeltDown
{
    public class RandomSpawner : MonoBehaviour
    {
        [SerializeField] MonsterController _monsterPrefab;
        [SerializeField] float _distanceFromPlayer;
        [SerializeField] float _timeBetweenSpawn;

        private Coroutine _spawnCoroutine;
        private GameViewController _gameViewController;
        public void Init(GameViewController gameViewController)
        {
            _gameViewController = gameViewController;
            StartSpawn();
        }
        public void StartSpawn()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }
}