using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Abstract;
using Assets.Scripts.Management;

namespace Assets.Scripts.UI
{
    public class EnemyHealth : Bar, IRegistrable
    {
        private Name _enemyName;
        private ElementManager _elementManager;

        public void Awake()
        {
            _enemyName = GetComponentInChildren<Name>();
            InitializeIndication();
            _elementManager = FindObjectOfType<ElementManager>();
            _elementManager.EnemyHealthBar = this;
            _elementManager.LoadProgress = 1;
            Debug.Log(this.GetType() + " loaded");
        }

        public void Refresh(float maxAmount, string newName)
        {
            this.MaxAmount = maxAmount;
            _enemyName.SetName(newName);
            Increase(MaxAmount);
        }

        public bool IsOutOfHP(float amount)
        {
            var remainingHP = Decrease(amount);
            if (remainingHP <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}