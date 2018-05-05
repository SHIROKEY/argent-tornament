using UnityEngine;
using Assets.Scripts.UI;

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
        public EnemyManager EnemyManager { get; set; }
        public SliceManager SliceManager { get; set; }
        public EffectManager EffectManager { get; set; }


        private void Awake()
        {
            EffectManager = FindObjectOfType<EffectManager>();
            EnemyManager = GetComponentInChildren<EnemyManager>();
            EnemyLayer = EnemyManager.GetComponent<RectTransform>();
            StaminaBar = GetComponentInChildren<StaminaBar>();
            EnemyHealthBar = GetComponentInChildren<EnemyHealth>();
            SliceManager = GetComponentInChildren<SliceManager>();
            
        }
    }
}
