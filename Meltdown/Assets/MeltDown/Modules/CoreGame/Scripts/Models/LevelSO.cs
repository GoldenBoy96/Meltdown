using OurUtils;
using System;
using UnityEngine;

namespace MeltDown
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/LevelSO", order = 0)]
    [Serializable]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] Level _data;

        public Level Data { get => ((ICloneable<Level>)_data).CloneSelf(); }
    }
}