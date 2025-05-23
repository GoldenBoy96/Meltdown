using OurUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    [Serializable]
    public class Monster : ICloneable<Monster>, IEquatable<Monster>
    {
        [SerializeField] Guid _id;
        [SerializeField] string _monsterName;
        [SerializeField] string _description;
        [SerializeField] float _maxHp;
        [SerializeField] float _hp;
        [SerializeField] float _atk;
        [SerializeField] float _def;
        [SerializeField] float _spe;
        [SerializeField] float _maxDectetionRange;
        [SerializeField] float _minDectetionRange;
        [SerializeField] float _attackRange;
        [SerializeField] float _attackPower; //TODO: Move this to MonsterSkillSO
        [SerializeField] float _attackCooldown; 

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty) _id = Guid.NewGuid();
                return _id;
            }
            set => _id = value;
        }
        public string MonsterName { get => _monsterName; set => _monsterName = value; }
        public string Description { get => _description; set => _description = value; }
        public float MaxHp { get => _maxHp; set => _maxHp = value; }
        public float Hp { get => _hp; set => _hp = value; }
        public float Atk { get => _atk; set => _atk = value; }
        public float Def { get => _def; set => _def = value; }
        public float Spe { get => _spe; set => _spe = value; }
        public float MaxDectetionRange { get => _maxDectetionRange; set => _maxDectetionRange = value; }
        public float MinDectetionRange { get => _minDectetionRange; set => _minDectetionRange = value; }
        public float AttackRange { get => _attackRange; set => _attackRange = value; }
        public float AttackPower { get => _attackPower; set => _attackPower = value; }
        public float AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Monster);
        }

        public bool Equals(Monster other)
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
            return $"{{{nameof(MonsterName)}={MonsterName}, {nameof(Description)}={Description}, {nameof(MaxHp)}={MaxHp.ToString()}, {nameof(Hp)}={Hp.ToString()}, {nameof(Atk)}={Atk.ToString()}, {nameof(Def)}={Def.ToString()}, {nameof(Spe)}={Spe.ToString()}}}";
        }
    }
}