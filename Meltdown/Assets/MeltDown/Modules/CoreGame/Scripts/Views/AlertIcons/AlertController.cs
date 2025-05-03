using MeltDown;
using OurUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterController = MeltDown.CharacterController;

public class AlertController : MonoBehaviour
{

    [SerializeField] AlertIconController _alertIconPrefab;
    [SerializeField] CharacterController _player;
    [SerializeField] Camera _camera;
    [SerializeField] List<MonsterController> _monsterList = new();
    [SerializeField] EndPointController _endpoint;

    [SerializeField] Dictionary<AlertIconController, MonsterController> _alertMonsterIconDictionary = new();

    private void Start()
    {
        _player = FindAnyObjectByType<CharacterController>();
        _camera = Camera.current;
        _monsterList = FindObjectsOfType<MonsterController>().ToList();
        _endpoint = FindAnyObjectByType<EndPointController>();
        foreach (var monster in _monsterList)
        {
            AlertIconController alertIcon = ObjectPooling.Instance.SpawnObject(_alertIconPrefab.gameObject, transform, Vector3.zero, Quaternion.identity).GetComponent<AlertIconController>();
            _alertMonsterIconDictionary.Add(alertIcon, monster);
        }
    }

    public void UpdateAlertIcon()
    {
        foreach (var test in _alertMonsterIconDictionary)
        {

        }
    }

}
