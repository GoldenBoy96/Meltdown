using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [Serializable]
    public class Icecream : ICloneable<Icecream>, IEquatable<Icecream>
    {
        [SerializeField] Guid _id;
        [SerializeField] string _icecreamName;
        [SerializeField] string _description;
        [SerializeField] float _maxHp;
        [SerializeField] float _hp;
        [SerializeField] float _def;
        [SerializeField] float _meltDownPerSecond;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty) _id = Guid.NewGuid();
                return _id;
            }
            set => _id = value;
        }
        public string IcecreamName { get => _icecreamName; set => _icecreamName = value; }
        public string Description { get => _description; set => _description = value; }
        public float MaxHp { get => _maxHp; set => _maxHp = value; }
        public float Hp { get => _hp; set => _hp = value; }
        public float Def { get => _def; set => _def = value; }
        public float MeltDownPerSecond { get => _meltDownPerSecond; set => _meltDownPerSecond = value; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Icecream);
        }

        public bool Equals(Icecream other)
        {
            return other is not null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(IcecreamName)}={IcecreamName}, {nameof(Description)}={Description}, {nameof(MaxHp)}={MaxHp.ToString()}, {nameof(Hp)}={Hp.ToString()}, {nameof(Def)}={Def.ToString()}, {nameof(MeltDownPerSecond)}={MeltDownPerSecond.ToString()}}}";
        }
    }
}