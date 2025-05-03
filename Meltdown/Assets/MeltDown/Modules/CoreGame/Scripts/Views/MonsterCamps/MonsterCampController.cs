using MeltDown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCampController : MonoBehaviour
{
    [Header("Init Data")]
    [SerializeField] MonsterCampSO _monsterCampSO;
    [SerializeField] List<MonsterController> _registeredMonster = new();

    [Header("Runtime Data")]
    [SerializeField] MonsterCamp _monsterCamp;

    public MonsterCamp MonsterCamp { get => _monsterCamp; private set => _monsterCamp = value; }

    private void OnEnable()
    {
        _monsterCamp = _monsterCampSO.Data;
        foreach (var monster in _registeredMonster)
        {
            monster.RegisterCamp(this);
        }
    }
}
