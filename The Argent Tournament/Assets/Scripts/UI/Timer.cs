using Assets.Scripts.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Timer: MonoBehaviour
    {
        public int MaxSeconds = 0;

        public int CurrentSeconds { get; set; }

        private Text _text;
        private Coroutine _counting;

        private string _min;
        private string _sec;

        public bool _inProgress = false;

        public void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void Stop()
        {
            _inProgress = false;
            CurrentSeconds = 0;
            StopCoroutine(_counting);
        }

        public void Start()
        {
            _inProgress = true;
            CurrentSeconds = MaxSeconds;
            _counting = StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            yield return new WaitForSeconds(1);
            CurrentSeconds -= 1;
            _min = (CurrentSeconds / 60).ToString();
            _sec = (CurrentSeconds % 60).ToString();
            _text.text = _min + ":" + _sec;
            if (CurrentSeconds>0)
            {
                _counting = StartCoroutine(Tick());
            }
            else
            {
                _inProgress = false;
            }
        }
    }
}
