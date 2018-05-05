using Assets.Scripts.Abstract;
using Assets.Scripts.Management;
using System;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Pointer: StorableElement, IDisposable
    {
        public float StaminaConsumePerLengthPoint { get; set; }
        public float DamagePerStaminaPoint { get; set; }

        private float _currentDamage = 0;

        private void Awake()
        {
            LinkToGameLogic(FindObjectOfType<GameLogicManager>());
            DamagePerStaminaPoint = GameLogicManager.DamagePerStaminaPoint;
            StaminaConsumePerLengthPoint = GameLogicManager.StaminaConsumePerLengthPoint;
        }

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
            var enemy = collision.gameObject.GetComponent<DamageableObject>();
            if (enemy != null)
            {
                enemy.TakeDamage(this, GetComponent<RectTransform>().anchoredPosition);
            }
            if (enemy.GetComponent<Enemy>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
