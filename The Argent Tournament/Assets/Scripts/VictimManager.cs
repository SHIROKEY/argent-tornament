using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class VictimManager : MonoBehaviour
    {
        public GameObject[] Victims;

        private List<EnemyLevel> _enemyLevelsMap;
        private ElementManager _elementManager;

        private void Start()
        {
            _elementManager = FindObjectOfType<ElementManager>();
            _enemyLevelsMap = new List<EnemyLevel>
            {
                new EnemyLevel(0,0) 
            };
        }

        public GameObject GetNextEnemy()
        {
            var enemyNumber = Random.Range(0, Victims.Length);
            var enemy = Instantiate(Victims[enemyNumber], _elementManager.EnemyLayer);
            return enemy;
        }
    }
}
