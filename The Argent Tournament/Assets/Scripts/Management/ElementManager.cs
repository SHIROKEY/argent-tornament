using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.UI;
using Assets.Scripts.Management;
using Assets.Scripts.Abstract;

namespace Assets.Scripts.Management
{
    public class ElementManager : GameManager
    {
        public override Transform EnemyLayer { get; set; }
        public override StaminaBar StaminaBar { get; set; }
        public override EnemyHealth EnemyHealthBar { get; set; }
        public override RectTransform SliceControllerTransform { get; set; }
        public override VictimManager VictimManager { get; set; }

        public Vector2 PointerPositionAmendment { get; set; }

        public float KillingScore { get; set; }

        private void Awake()
        {
            Debug.Log(this.GetType() + " loaded");
        }

        public void SpawnNextVictim()
        {
            VictimManager.SpawnNextEnemy();
        }

    }
}
