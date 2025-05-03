using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [CreateAssetMenu(fileName = "MonsterCampSO", menuName = "ScriptableObjects/MonsterCampSO", order = 0)]
    [Serializable]
    public class MonsterCampSO : ScriptableObject
    {
        [SerializeField] MonsterCamp _data;

        public MonsterCamp Data { get => ((ICloneable<MonsterCamp>)_data).CloneSelf(); }
    }
}