using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeltDown
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] Slider _slider;
        
        public void UpdateHpBar(float hp, float maxHp)
        {
            _slider.value = hp / maxHp;
        }
    }
}