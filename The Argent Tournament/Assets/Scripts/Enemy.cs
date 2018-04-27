using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    class Enemy : DamageableObject
    {
        public float MaxHealth = 0;
        public string DisplayName = "";

        //private float _currentHealth;
        private Animator _animator;
        private ElementManager _elementManager;

        private void Start()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            _animator = GetComponentInParent<Animator>();
        }

        public override void OnDamageTaken(float amount)
        {

        }

        public override void TakeDamage(Pointer pointer, Vector2 point)
        {
            _animator.Play("TakingDamage");
            var amount = pointer.GetDamage();
            var floatingtext = Instantiate(_elementManager.FloatingTextPrefab, _elementManager.EnemyLayer);
            floatingtext.GetComponent<Text>().text = Math.Round(amount, 2).ToString();
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
            _elementManager.SpawnNextVictim();
        }
    }
}
