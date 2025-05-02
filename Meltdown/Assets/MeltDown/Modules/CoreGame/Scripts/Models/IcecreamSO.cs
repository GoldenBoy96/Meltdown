using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [CreateAssetMenu(fileName = "IcecreamSO", menuName = "ScriptableObjects/IcecreamSO", order = 0)]
    [Serializable]
    public class IcecreamSO : ScriptableObject
    {
        [SerializeField] Icecream _data;

        public Icecream Data { get => ((ICloneable<Icecream>)_data).CloneSelf(); }
    }
}