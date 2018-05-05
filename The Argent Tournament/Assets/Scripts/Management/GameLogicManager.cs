using Assets.Scripts.Logic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Management
{
    public class GameLogicManager: MonoBehaviour
    {
        public float KillingScore { get; set; }

        public int CurrentPlayerLevel = 0;

        public float DamagePerStaminaPoint = 0;
        public float StaminaConsumePerLengthPoint = 0;

        public float DamageIncrease = 0;
        public float StaminaIncrease = 0;

        private ElementManager _elementManager;
        private bool _isGameOver = false;
        public GameRecord record;

        private void GameStart()
        {
            StartCoroutine(StartGameCoroutine());
        }

        public void RandomLevelUp()
        {
            var random = Random.Range(0, 5);
            switch (random)
            {
                case 1:
                    _elementManager.StaminaBar.MaxAmount += StaminaIncrease;
                    break;
                case 2:
                    DamagePerStaminaPoint += DamageIncrease;
                    break;
                default:
                    break;
            }
        }

        private IEnumerator StartGameCoroutine()
        {
            _elementManager.EffectManager.MainTimer.MaxSeconds = 12;
            _elementManager.EffectManager.MainTimer.StartTimer();
            yield return new WaitForSeconds(12);
            SetActive("IntroSource",false);
            SetActive("IntroLabel", false);
            SetActive("BGMSource", true);
            _elementManager.EffectManager.MainTimer.StopTimer();
            _elementManager.EffectManager.MainTimer.MaxSeconds = 25;
            _elementManager.EnemyManager.SpawnNextEnemy(0);
            _elementManager.EffectManager.MainTimer.IsIntro = false;
            _elementManager.EffectManager.MainTimer.StartTimer();
        }

        private void SetActive(string name, bool state)
        {
            var element = _elementManager.transform.Find(name);
            element.gameObject.SetActive(state);
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        public void GameEnd()
        {
            _isGameOver = true;
            _elementManager.SliceManager.gameObject.SetActive(false);
            SetActive("BGMSource", false);
            var menu = _elementManager.transform.Find("GameOverMenu");
            var killerName = _elementManager.EnemyManager.CurrentEnemyName;
            menu.gameObject.SetActive(true);
            menu.transform.Find("KillerLabel").GetComponent<Text>().text = killerName;
            menu.transform.Find("ScoreAmount").GetComponent<Text>().text = ((int)KillingScore).ToString();
            record = new GameRecord()
            {
                EnemyLevel = _elementManager.EnemyManager.CurrentEnemyLevel,
                KillerName = killerName,
                StaminaAmount = _elementManager.StaminaBar.MaxAmount,
                DamagePerStaminaPoint = DamagePerStaminaPoint,
                Score = KillingScore
            };
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
