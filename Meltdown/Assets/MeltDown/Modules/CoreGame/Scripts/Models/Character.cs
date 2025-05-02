using OurUtils;
using System;
using UnityEngine;

namespace MeltDown
{
    [Serializable]
    public class Character : ICloneable<Character>
    {
        [SerializeField] Guid _id;
        [SerializeField] string _characterName;
        [SerializeField] string _description;
        [SerializeField] float _maxHp;
        [SerializeField] float _hp;
        [SerializeField] float _atk;
        [SerializeField] float _def;
        [SerializeField] float _spe;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty) _id = Guid.NewGuid();
                return _id;
            }
            set => _id = value;
        }
        public string CharacterName { get => _characterName; set => _characterName = value; }
        public string Description { get => _description; set => _description = value; }
        public float MaxHp { get => _maxHp; set => _maxHp = value; }
        public float Hp { get => _hp; set => _hp = value; }
        public float Atk { get => _atk; set => _atk = value; }
        public float Def { get => _def; set => _def = value; }
        public float Spe { get => _spe; set => _spe = value; }

        public override string ToString()
        {
            return $"{{{nameof(CharacterName)}={CharacterName}, {nameof(Description)}={Description}, {nameof(MaxHp)}={MaxHp.ToString()}, {nameof(Hp)}={Hp.ToString()}, {nameof(Atk)}={Atk.ToString()}, {nameof(Def)}={Def.ToString()}, {nameof(Spe)}={Spe.ToString()}}}";
        }
    }
}