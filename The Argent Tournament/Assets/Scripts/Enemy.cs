using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
        private ElementManager _elementManager;

        private void Start()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            _animator = GetComponentInParent<Animator>();
        }

        public override void TakeDamage(Pointer pointer, Vector2 point)
        {
            _animator.Play("TakingDamage");
            var amount = Mathf.Round(pointer.GetDamage());
            var floatingtext = Instantiate(_elementManager.FloatingTextPrefab, _elementManager.EnemyLayer);
            floatingtext.GetComponent<Text>().text = amount.ToString();
            floatingtext.GetComponent<RectTransform>().anchoredPosition = point - _elementManager.PointerPositionAmendment;
            pointer.DecreaseDamage(amount);
            if (_elementManager.HealthBar.TryToKill(amount))
            {
                _animator.Play("Death");
            }
        }

        public void OnDeath()
        {
            Destroy(gameObject);
            _elementManager.KillingScore += KillingPoints;
            _elementManager.SpawnNextVictim();
        }
    }
}
