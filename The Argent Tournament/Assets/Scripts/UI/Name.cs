﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Name : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<Text>();
        }

        public void SetName(string name)
        {
            _text.text = name;
        }
    }
}
