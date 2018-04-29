using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.UI;
using Assets.Scripts.Management;
using Assets.Scripts.Abstract;

namespace Assets.Scripts.Management
{
    public class ElementManager : MonoBehaviour
    {
        public GameObject PointerPrefab;
        public GameObject TrailPrefab;
        public GameObject FloatingTextPrefab;

        public Transform EnemyLayer { get; set; }
        public StaminaBar StaminaBar { get; set; }
        public EnemyHealth EnemyHealthBar { get; set; }

        public RectTransform SliceControllerTransform { get; set; }
        public EnemyManager EnemyManager { get; set; }

        public Vector2 PointerPositionAmendment { get; set; }

        public float KillingScore { get; set; }

        private int _loadProgress;

        public int LoadProgress
        {
            private get
            {
                return _loadProgress;
            }
            set
            {
                _loadProgress += value;
                if (_loadProgress == 6)
                {
                    StartGame();
                }
            }
        }

        private void OnPostRender()
        {
            Debug.Log("Scene is rendered!!!");
        }

        private void Awake()
        {
            Debug.Log(this.GetType() + " loaded");
        }

        public void SpawnNextEnemy()
        {
            EnemyManager.SpawnNextEnemy(0);
        }

        public void StartGame()
        {
            SpawnNextEnemy();
        }
    }
}
