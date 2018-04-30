using Assets.Scripts.Abstract;
using Assets.Scripts.Management;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StaminaBar : Bar
    {
        public float TickTime = 0;
        public float RecoverStaminaPerTick = 0;

        public bool IsRecovering { get; set; }

        private Coroutine _currentAction;

        public void Awake()
        {
            InitializeIndication();
            Increase(MaxAmount);
        }

        private IEnumerator RecoverStamina()
        {
            if (GetCurrentAmount() < MaxAmount)
            {
                Increase(RecoverStaminaPerTick);
                yield return new WaitForSeconds(TickTime);
                _currentAction = StartCoroutine(RecoverStamina());
            }
            else
            {
                StopRecovery();
                IsRecovering = false;
            }
        }

        public void StopRecovery()
        {
            if (_currentAction != null)
            {
                StopCoroutine(_currentAction);
            }
            IsRecovering = false;
        }

        public void StartRecovery()
        {
            if (!IsRecovering)
            {
                IsRecovering = true;
                _currentAction = StartCoroutine(RecoverStamina());
            }
        }

        public float ConsumeStamina(float amount)
        {
            return Decrease(amount);
        }

        protected override float Decrease(float amount)
        {
            float _consumedStamina;
            if (_currentAmount > amount)
            {
                _consumedStamina = amount;
                _currentAmount -= amount;
            }
            else
            {
                _consumedStamina = _currentAmount;
                _currentAmount = 0;
            }
            base._indicator.RenderIndication(_currentAmount / MaxAmount);
            return _consumedStamina;
        }
    }
}
