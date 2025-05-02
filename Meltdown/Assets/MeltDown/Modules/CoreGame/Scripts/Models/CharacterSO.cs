using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/CharacterSO", order = 0)]
    [Serializable]
    public class CharacterSO : ScriptableObject
    {
        [SerializeField] Character _data;

        public Character Data { get => ((ICloneable<Character>)_data).CloneSelf(); }
    }
}