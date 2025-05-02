using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [CreateAssetMenu(fileName = "MonsterSO", menuName = "ScriptableObjects/MonsterSO", order = 0)]
    [Serializable]
    public class MonsterSO : ScriptableObject
    {
        [SerializeField] Monster _data;

        public Monster Data { get => ((ICloneable<Monster>)_data).CloneSelf(); }
    }
}