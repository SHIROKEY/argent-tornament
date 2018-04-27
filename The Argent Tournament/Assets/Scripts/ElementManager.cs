using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class ElementManager : MonoBehaviour
    {
        public Transform EnemyLayer;
        public StaminaBar StaminaBar;
        public EnemyHealthBar HealthBar;
        public GameObject PointerPrefab;
        public GameObject TrailPrefab;
        public GameObject FloatingTextPrefab;

        public Vector2 PointerPositionAmendment { get; private set; }

        public float KillingScore { get; set; }

        private RectTransform _sliceControllerTransform;
        private VictimManager _victimManager;

        private void Start()
        {
            _sliceControllerTransform = GetComponentInChildren<SliceController>().GetComponent<RectTransform>();
            PointerPositionAmendment = _sliceControllerTransform.anchoredPosition;
            _victimManager = EnemyLayer.GetComponent<VictimManager>();
        }

        public void SpawnNextVictim()
        {
            var victim = _victimManager.GetNextEnemy().GetComponent<Enemy>();
            HealthBar.Refresh(victim.MaxHealth, victim.DisplayName);
        }
    }
}
