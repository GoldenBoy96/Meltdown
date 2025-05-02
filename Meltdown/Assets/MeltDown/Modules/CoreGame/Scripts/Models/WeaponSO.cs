using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO", order = 0)]
    [Serializable]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] Weapon _data;

        public Weapon Data { get => ((ICloneable<Weapon>)_data).CloneSelf(); }
    }
}