using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class Bar : StorableElement
    {
        public float MaxAmount;
        protected float _currentAmount;

        protected BarFiller _indicator;

        public float GetCurrentAmount()
        {
            return _currentAmount;
        }

        protected void Increase(float amount)
        {
            if (amount <= MaxAmount - _currentAmount)
            {
                _currentAmount += amount;
            }
            else
            {
                _currentAmount = MaxAmount;
            }
            _indicator.RenderIndication(_currentAmount / MaxAmount);
        }

        protected virtual float Decrease(float amount)
        {
            if (_currentAmount > amount)
            {
                _currentAmount -= amount;
            }
            else
            {
                _currentAmount = 0;
            }
            _indicator.RenderIndication(_currentAmount / MaxAmount);
            return _currentAmount;
        }

        protected void InitializeIndication()
        {
            _indicator = GetComponentInChildren<BarFiller>();
            _indicator.InitializeIndication();
        }
    }
}
