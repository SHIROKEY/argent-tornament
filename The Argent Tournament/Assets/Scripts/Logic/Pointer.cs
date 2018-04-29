using Assets.Scripts.Abstract;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Logic
{
    public class Pointer: MonoBehaviour, IDisposable
    {
        public float DamagePerStaminaPoint;
        public float StaminaConsumePerLengthPoint;

        private float _currentDamage = 0;

        public void IncreaseDamage(float staminaPoints)
        {
            _currentDamage += DamagePerStaminaPoint * staminaPoints;
        }

        public void DecreaseDamage(float amount)
        {
            if (amount < _currentDamage)
            {
                _currentDamage -= amount;
            }
            else
            {
                _currentDamage = 0;
            }
            
        }

        public float GetDamage()
        {
            return _currentDamage;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var victim = collision.gameObject.GetComponent<DamageableObject>();
            if (victim != null)
            {
                victim.TakeDamage(this, GetComponent<RectTransform>().anchoredPosition);
            }
        }
    }
}
