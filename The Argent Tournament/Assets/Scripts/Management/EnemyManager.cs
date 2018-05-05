using UnityEngine;
using System.Collections;
using Assets.Scripts.Enum;
using Assets.Scripts.Abstract;

namespace Assets.Scripts.Management
{
    public class EnemyManager : StorableElement
    {
        public Difficulty EnemyDifficulty = Difficulty.Dark_Souls;

        public int SecondsPerLevel = 0;

        public int CurrentEnemyLevel = 0;

        public string CurrentEnemyName = "";

        public GameObject[] Enemies;

        public int BonusSeconds
        {
            get
            {
                return GetCurrentLevel() * SecondsPerLevel;
            }
        }

        
        private int _remainingDelay = 0;

        private bool _waiting;
        private int _enemySpawnDelay = 0;

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
            var tmp = CurrentEnemyLevel;
            CurrentEnemyLevel = GetCurrentLevel();
            if (CurrentEnemyLevel > tmp)
            {
                while (tmp<=CurrentEnemyLevel)
                {
                    GameLogicManager.RandomLevelUp();
                    tmp++;
                }
            }
            var enemy = Instantiate(Enemies[enemyNumber], GameLogicManager.GetEnemyLayer());
            CurrentEnemyName = enemy.GetComponent<Enemy>().DisplayName;
            LevelUp(enemy.GetComponent<Enemy>());
            return enemy;
        } 

        private int GetCurrentLevel()
        {
            var level = CurrentEnemyLevel;
            var levelBound = (int)EnemyDifficulty * Mathf.Exp(CurrentEnemyLevel);
            while (levelBound< GameLogicManager.KillingScore)
            {
                level++;
                levelBound  = (int)EnemyDifficulty * Mathf.Exp(level);
            }
            return level;
        }

        private void LevelUp(Enemy enemy)
        {
            enemy.MaxHealth += enemy.HealthPerLevel * CurrentEnemyLevel;
            enemy.KillingPoints += enemy.PointsPerLevel * CurrentEnemyLevel;
            enemy.DisplayName = enemy.DisplayName + " (lvl-"+(CurrentEnemyLevel + 1)+")";
        }

        public void SpawnNextEnemy(int secDelay)
        {
            _enemySpawnDelay = secDelay;
            StartCoroutine(WaitForSecondBeforeSpawn());
        }

        private void Spawn()
        {
            Enemy enemy;
            if (CurrentEnemyLevel < 1)
            {
                enemy = GetNextEnemy(0, 0).GetComponent<Enemy>();
            }
            else
            {
                enemy = GetNextEnemy().GetComponent<Enemy>();
                
            }
            GameLogicManager.RefreshHealthBar(enemy.MaxHealth, enemy.DisplayName);
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
