﻿using System;
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

        public int CurrentPlayerLevel = 0;

        public float DamagePerStaminaPoint = 0;
        public float StaminaConsumePerLengthPoint = 0;

        private ElementManager _elementManager;
        private bool _isGameOver = false;

        private void GameStart()
        {
            _elementManager.EnemyManager.SpawnNextEnemy(0);
            _elementManager.EffectManager.MainTimer.StartTimer();
        }

        public void GameEnd()
        {
            Debug.Log("Game over");
            _isGameOver = true;
            _elementManager.SliceManager.gameObject.SetActive(false);
            var menu = _elementManager.transform.Find("GameOverMenu");
            var levelPoints = (_elementManager.EnemyManager.CurrentEnemyLevel - CurrentPlayerLevel);
            levelPoints = levelPoints > 0 ? levelPoints : 0;
            var killerName = _elementManager.EnemyManager.CurrentEnemyName;
            menu.gameObject.SetActive(true);
            menu.transform.Find("KillerLabel").GetComponent<Text>().text = killerName;
            menu.transform.Find("LevelPointsAmount").GetComponent<Text>().text = levelPoints.ToString();
            menu.transform.Find("ScoreAmount").GetComponent<Text>().text = ((int)KillingScore).ToString();
        }

        private void Awake()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            _elementManager.EnemyManager.LinkToGameLogic(this);
            _elementManager.StaminaBar.LinkToGameLogic(this);
            _elementManager.EnemyHealthBar.LinkToGameLogic(this);
            _elementManager.SliceManager.LinkToGameLogic(this);
            _elementManager.EffectManager.LinkToGameLogic(this);
            _elementManager.EffectManager.MainTimer.LinkToGameLogic(this);
        }

        private void Start()
        {
            GameStart();
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
            if (!_isGameOver)
            {
                _elementManager.EnemyManager.SpawnNextEnemy(0);
                _elementManager.EffectManager.MainTimer.IncreaseTime(_elementManager.EnemyManager.BonusSeconds);
            }
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
