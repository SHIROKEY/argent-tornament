using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Management
{
    public class GameLogicManager: MonoBehaviour
    {
        public float KillingScore { get; set; }
        public float DamagePerStaminaPoint = 0;
        public float StaminaConsumePerLengthPoint = 0;

        private ElementManager _elementManager;

        private void Awake()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            _elementManager.EnemyManager.LinkToGameLogic(this);
            _elementManager.StaminaBar.LinkToGameLogic(this);
            _elementManager.EnemyHealthBar.LinkToGameLogic(this);
            _elementManager.SliceManager.LinkToGameLogic(this);
        }

        private void Start()
        {
            _elementManager.EnemyManager.SpawnNextEnemy(0);
        }

        public void RefreshHealthBar(float newAmount, string newName)
        {
            _elementManager.EnemyHealthBar.Refresh(newAmount, newName);
        }

        public Transform GetEnemyLayer()
        {
            return _elementManager.EnemyLayer;
        }

        public void StopStaminaRecovery()
        {
            _elementManager.StaminaBar.StopRecovery();
        }

        public float UseStamina(float amount)
        {
            return _elementManager.StaminaBar.ConsumeStamina(amount);
        }

        public void StartStaminaRecovery()
        {
            _elementManager.StaminaBar.StartRecovery();
        }

        public void CreateFloatingText(string text, Vector2 point, Color color)
        {
            var floatingText = Instantiate(_elementManager.FloatingTextPrefab, _elementManager.EnemyLayer);
            var container = floatingText.GetComponent<Text>();
            container.text = text;
            container.color = color;
            floatingText.GetComponent<RectTransform>().anchoredPosition = point;
        }

        public bool IsOutOfHP(float amount)
        {
            return _elementManager.EnemyHealthBar.IsOutOfHP(amount);
        }

        public void OnCurrentEnemyDeath()
        {
            _elementManager.EnemyManager.SpawnNextEnemy(0);
        }

        public RectTransform CreatePointer()
        {
            return Instantiate(_elementManager.PointerPrefab, _elementManager.EnemyLayer).GetComponent<RectTransform>();
        }

        public float GetCurrentStaminaAmount()
        {
            return _elementManager.StaminaBar.GetCurrentAmount();
        }

        public void CreateTrail(RectTransform pointerRectTransform)
        {
            Instantiate(_elementManager.TrailPrefab, pointerRectTransform);
        }
    }
}
