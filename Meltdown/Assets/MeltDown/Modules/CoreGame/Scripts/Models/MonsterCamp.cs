using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [Serializable]
    public class MonsterCamp : ICloneable<MonsterCamp>
    {
        [SerializeField] float _range;

        public float Range { get => _range; set => _range = value; }


    }
}