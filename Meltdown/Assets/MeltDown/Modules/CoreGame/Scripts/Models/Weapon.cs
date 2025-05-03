using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [Serializable]
    public class Weapon : ICloneable<Weapon>, IEquatable<Weapon>
    {
        [SerializeField] Guid _id;
        [SerializeField] string _weaponName;
        [SerializeField] string _description;
        [SerializeField] float _power;
        [SerializeField] float _cooldown;
        [SerializeField] float _range;
        [SerializeField] float _angle;
        [SerializeField] float _direction;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty) _id = Guid.NewGuid();
                return _id;
            }
            set => _id = value;
        }
        public string WeaponName { get => _weaponName; set => _weaponName = value; }
        public string Description { get => _description; set => _description = value; }
        public float Power { get => _power; set => _power = value; }
        public float Cooldown { get => _cooldown; set => _cooldown = value; }
        public float Range { get => _range; set => _range = value; }
        public float Angle { get => _angle; set => _angle = value; }
        public float Direction { get => _direction; set => _direction = value; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Weapon);
        }

        public bool Equals(Weapon other)
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
            return $"{{{nameof(WeaponName)}={WeaponName}, {nameof(Description)}={Description}, {nameof(Power)}={Power.ToString()}, {nameof(Cooldown)}={Cooldown.ToString()}, {nameof(Range)}={Range.ToString()}, {nameof(Angle)}={Angle.ToString()}, {nameof(Direction)}={Direction.ToString()}}}";
        }
    }
}