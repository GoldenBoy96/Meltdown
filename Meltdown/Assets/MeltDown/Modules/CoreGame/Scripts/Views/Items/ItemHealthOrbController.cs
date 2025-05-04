using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeltDown
{
    public class ItemHealthOrbController : MonoBehaviour
    {
        [SerializeField] float _hpToHeal;

        bool _isUsed = false;

        public float HpToHeal { get => _hpToHeal; set => _hpToHeal = value; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent<CharacterController>(out var player);
            if (player != null && !_isUsed)
            {
                if (player.Icecream != null)
                {
                    player.Icecream.Heal(_hpToHeal);
                    _isUsed = true;
                    Destroy(gameObject);
                }
        }
        }
    }
}