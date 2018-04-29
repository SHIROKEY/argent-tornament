using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Assets.Scripts.Enum;
using Assets.Scripts.Logic;

namespace Assets.Scripts.Management
{
    public class VictimManager : MonoBehaviour
    {
        public int EnemySpawnDelay = 0;
        public float[] _levelingGaps = new float[0];
        public Difficulty EnemyDifficulty = Difficulty.Dark_Souls;

        public GameObject[] Victims;

        private ElementManager _elementManager;

        private int _currentEnemyLevel = 0;
        private int _remainingDelay = 0;

        private bool _waiting;


        private void Start()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            Debug.Log("Victim Manager");
            for (int i=0;i<_levelingGaps.Length;i++)
            {
                _levelingGaps[i] *= (int)EnemyDifficulty;
            }
        }

        public void CreateFirstEnemy()
        {
            var victim = Instantiate(Victims[0], _elementManager.EnemyLayer).GetComponent<Enemy>();
            _elementManager.HealthBar.Refresh(victim.MaxHealth, victim.DisplayName + " (lvl-1)");
        }

        public GameObject GetNextEnemy()
        {
            _currentEnemyLevel = GetCurrentLevel();
            var enemyNumber = Random.Range(0, Victims.Length);
            var enemy = Instantiate(Victims[enemyNumber], _elementManager.EnemyLayer);
            LevelUp(enemy.GetComponent<Enemy>());
            return enemy;
        }

        private int GetCurrentLevel()
        {
            var previousLevels = _levelingGaps.Where(x => x <= _elementManager.KillingScore).ToArray();
            var level = _currentEnemyLevel < previousLevels.Length ? previousLevels.Length : _currentEnemyLevel;
            return level;
        }

        private void LevelUp(Enemy enemy)
        {
            enemy.MaxHealth += enemy.HealthPerLevel * _currentEnemyLevel;
            enemy.KillingPoints += enemy.PointsPerLevel * _currentEnemyLevel;
            enemy.DisplayName = enemy.DisplayName + " (lvl-"+(_currentEnemyLevel + 1)+")";
        }

        public void SpawnNextEnemy()
        {
            StartCoroutine(WaitForSecondBeforeSpawn());
        }

        private void Spawn()
        {
            var victim = GetNextEnemy().GetComponent<Enemy>();
            _elementManager.HealthBar.Refresh(victim.MaxHealth, victim.DisplayName);
        }

        private IEnumerator WaitForSecondBeforeSpawn()
        {
            if (!_waiting)
            {
                _remainingDelay = EnemySpawnDelay;
                _waiting = true;
            }
            if (_remainingDelay > 0)
            {
                yield return new WaitForSeconds(1);
                _remainingDelay -= 1;
                StartCoroutine(WaitForSecondBeforeSpawn());
            }
            else
            {
                _waiting = false;
                Spawn();
            }
        }
    }
}
