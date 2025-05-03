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
    [SerializeField] bool _isTriggerHordeAttack = false;
    [SerializeField] IcecreamController _icecreamToChase;

    public MonsterCamp MonsterCamp { get => _monsterCamp; private set => _monsterCamp = value; }
    public bool IsTriggerHordeAttack { get => _isTriggerHordeAttack; private set => _isTriggerHordeAttack = value; }
    public IcecreamController IcecreamToChase { get => _icecreamToChase; private set => _icecreamToChase = value; }

    private void OnEnable()
    {
        _monsterCamp = _monsterCampSO.Data;
        foreach (var monster in _registeredMonster)
        {
            monster.RegisterCamp(this);
        }
    }

    private void Update()
    {
        CheckStopHordeAttack();
    }

    public void TriggerHordeAttack(IcecreamController chasingIcecream)
    {
        _isTriggerHordeAttack = true;
        IcecreamToChase = chasingIcecream;
    }

    public void CheckStopHordeAttack()
    {
        if (!IsTriggerHordeAttack) return;
        if (Vector3.Distance(IcecreamToChase.transform.position, transform.position) > MonsterCamp.Range)
        {
            StopHordeAttack();
        }
    }

    public void StopHordeAttack()
    {
        _isTriggerHordeAttack = false;
    }


}
