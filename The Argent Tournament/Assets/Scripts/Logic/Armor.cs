﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Abstract;
using Assets.Scripts.Management;

namespace Assets.Scripts.Logic
{
    public class Armor : DamageableObject
    {
        private ElementManager _elementManager;

        public float AbsorbtionPercent = 0;

        public void Awake()
        {
            Debug.Log(this.GetType() + " loaded");
        }

        private void Start()
        {
            _elementManager = FindObjectOfType<ElementManager>();
        }

        public override void TakeDamage(Pointer pointer, Vector2 point)
        {
            var decreased = Mathf.Round(AbsorbtionPercent * pointer.GetDamage() / 100);
            pointer.DecreaseDamage(decreased);
            var floatingtext = Instantiate(_elementManager.FloatingTextPrefab, _elementManager.EnemyLayer);
            var textContainer = floatingtext.GetComponent<Text>();
            textContainer.text = decreased.ToString();
            textContainer.color = new Color(0, 1, 0);
            floatingtext.GetComponent<RectTransform>().anchoredPosition = (point - _elementManager.PointerPositionAmendment) - new Vector2(300,0);
        }
    }
}
