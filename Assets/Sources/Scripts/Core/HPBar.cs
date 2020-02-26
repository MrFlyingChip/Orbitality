using System;
using UnityEngine;

namespace Sources.Scripts.Core
{
    public class HPBar : MonoBehaviour
    {
        public Transform barValue;
        
        private float _minValue = 0;
        private float _maxValue = 1;
        private float _currentValue;

        private void Awake()
        {
            CalculateCurrentValue();
        }

        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                if ( _currentValue == value)
                {
                    return;
                }
                
                _currentValue = value;
                CalculateCurrentValue();
            }
        }
        
        public float MinValue
        {
            get => _minValue;
            set
            {
                if ( _minValue == value)
                {
                    return;
                }
                
                _minValue = value;
                CalculateCurrentValue();
            }
        }
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                if ( _maxValue == value)
                {
                    return;
                }
                
                _maxValue = value;
                CalculateCurrentValue();
            }
        }

        private void CalculateCurrentValue()
        {
            float currentValuePercent = _minValue + _currentValue / (_maxValue - _minValue);
            if (currentValuePercent < 0)
            {
                currentValuePercent = 0;
            } 
            else if (currentValuePercent > 1)
            {
                currentValuePercent = 1;
            }
            
            barValue.localScale = new Vector3(currentValuePercent, 1, 1);
        }
    }
}
