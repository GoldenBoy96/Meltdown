using OurUtils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [Serializable]
    public class GameLog : ICloneable<GameLog>
    {
        [SerializeField] Level _level;
        [SerializeField] List<Turn> _turns;

        public Level Level { get => _level; set => _level = value; }
        public List<Turn> Turns { get => _turns; set => _turns = value; }

        public override string ToString()
        {
            return $"{{{nameof(Level)}={Level}, {nameof(Turns)}={Turns}}}";
        }
    }
}