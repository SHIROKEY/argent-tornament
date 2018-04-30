using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Assets.Scripts.Enum;
using Assets.Scripts.Logic;
using Assets.Scripts.Abstract;

namespace Assets.Scripts.Management
{
    public class EnemyManager : MonoBehaviour, IRegistrable
    {
        public Difficulty EnemyDifficulty = Difficulty.Dark_Souls;

        public GameObject[] Enemies;

        private ElementManager _elementManager;

        private int _currentEnemyLevel = 0;
        private int _remainingDelay = 0;

        private bool _waiting;
        private int _enemySpawnDelay = 0;

        public void Awake()
        {
            Debug.Log(this.GetType() + " loaded");
        }

        public GameObject GetNextEnemy()
        {
            return CreateEnemy(Random.Range(0, Enemies.Length));
        }

        public GameObject GetNextEnemy(int min, int max)
        {
            return CreateEnemy(Random.Range(min, max));
        }

        private GameObject CreateEnemy(int enemyNumber)
        {
            _currentEnemyLevel = GetCurrentLevel();
            var enemy = Instantiate(Enemies[enemyNumber], _elementManager.EnemyLayer);
            LevelUp(enemy.GetComponent<Enemy>());
            return enemy;
        } 

        private int GetCurrentLevel()
        {
            var level = _currentEnemyLevel;
            //var levelBound = (int)EnemyDifficulty * Mathf.Exp(_currentEnemyLevel);
            //while (levelBound<_elementManager.KillingScore)
            //{
            //    level++;
            //    levelBound  = (int)EnemyDifficulty * Mathf.Exp(level);
            //}
            return level;
        }

        private void LevelUp(Enemy enemy)
        {
            enemy.MaxHealth += enemy.HealthPerLevel * _currentEnemyLevel;
            enemy.KillingPoints += enemy.PointsPerLevel * _currentEnemyLevel;
            enemy.DisplayName = enemy.DisplayName + " (lvl-"+(_currentEnemyLevel + 1)+")";
        }

        public void SpawnNextEnemy(int secDelay)
        {
            _enemySpawnDelay = secDelay;
            StartCoroutine(WaitForSecondBeforeSpawn());
        }

        private void Spawn()
        {
            Enemy enemy;
            if (_currentEnemyLevel < 1)
            {
                enemy = GetNextEnemy(0, 0).GetComponent<Enemy>();
            }
            else
            {
                enemy = GetNextEnemy().GetComponent<Enemy>();
                
            }
            _elementManager.EnemyHealthBar.Refresh(enemy.MaxHealth, enemy.DisplayName);
        }

        private IEnumerator WaitForSecondBeforeSpawn()
        {
            if (!_waiting)
            {
                _remainingDelay = _enemySpawnDelay;
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
