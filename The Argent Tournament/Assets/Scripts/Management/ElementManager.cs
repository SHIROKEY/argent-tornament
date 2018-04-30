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
        public EnemyManager EnemyManager { get; set; }
        public SliceManager SliceManager { get; set; }


        private void Awake()
        {
            EnemyManager = GetComponentInChildren<EnemyManager>();
            EnemyLayer = EnemyManager.GetComponent<RectTransform>();
            StaminaBar = GetComponentInChildren<StaminaBar>();
            EnemyHealthBar = GetComponentInChildren<EnemyHealth>();
            SliceManager = GetComponentInChildren<SliceManager>();
        }
    }
}
