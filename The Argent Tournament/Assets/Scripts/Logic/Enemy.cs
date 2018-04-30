using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Assets.Scripts.Abstract;
using Assets.Scripts.Logic;
using Assets.Scripts.Management;

namespace Assets.Scripts
{
    class Enemy : DamageableObject
    {
        public string DisplayName = "";
        public float MaxHealth = 0;
        public float KillingPoints = 0;

        public float HealthPerLevel = 0;
        public float PointsPerLevel = 0;

        private Animator _animator;

        private bool _dead;

        private void Start()
        {
            LinkToGameLogic(FindObjectOfType<GameLogicManager>());
            _animator = GetComponent<Animator>();
        }

        public override void TakeDamage(Pointer pointer, Vector2 point)
        {
            if (!_dead)
            {
                _animator.Play("TakingDamage");
                var amount = Mathf.Round(pointer.GetDamage());
                pointer.DecreaseDamage(amount);
                GameLogicManager.CreateFloatingText(amount.ToString(), point, new Color(1,1,1));
                _dead = GameLogicManager.IsOutOfHP(amount);
                if (_dead)
                {
                    _animator.Play("Death");
                }
            }
        }

        public void OnDeath()
        {
            Destroy(gameObject);
            GameLogicManager.KillingScore += KillingPoints;
            GameLogicManager.OnCurrentEnemyDeath();
        }
    }
}
