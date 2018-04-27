using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class VictimManager : MonoBehaviour
    {
        public float[] _levelingGaps = new float[0];
        public Difficulty EnemyDifficulty = Difficulty.Dark_Souls;

        public GameObject[] Victims;

        private ElementManager _elementManager;

        private int _currentEnemyLevel = 0;

        private void Start()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            for (int i=0;i<_levelingGaps.Length;i++)
            {
                _levelingGaps[i] *= (int)EnemyDifficulty;
            }
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
            var previousLevels = _levelingGaps.Where(x => x < _elementManager.KillingScore).ToArray();
            var level = _currentEnemyLevel < previousLevels.Length ? previousLevels.Length : _currentEnemyLevel;
            return level;
        }

        private void LevelUp(Enemy enemy)
        {
            enemy.MaxHealth += enemy.HealthPerLevel * _currentEnemyLevel;
            enemy.DisplayName = enemy.DisplayName + (_currentEnemyLevel + 1);
        }
    }
}
