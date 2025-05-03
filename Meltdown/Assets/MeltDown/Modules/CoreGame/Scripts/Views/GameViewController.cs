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
        List<MonsterController> _monsterList = new();
    }
}