using Assets.Scripts.Management;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class GameManager: MonoBehaviour
    {
        public GameObject PointerPrefab;
        public GameObject TrailPrefab;
        public GameObject FloatingTextPrefab;

        public abstract Transform EnemyLayer { get; set; }
        public abstract StaminaBar StaminaBar { get; set; }
        public abstract EnemyHealth EnemyHealthBar { get; set; } 

        public abstract RectTransform SliceControllerTransform { get; set; }
        public abstract VictimManager VictimManager { get; set; }
}
}
