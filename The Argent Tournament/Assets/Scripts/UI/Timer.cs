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
    public class Timer: StorableElement
    {
        public int MaxSeconds = 0;
        public bool IsIntro = true;

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

        public void StopTimer()
        {
            _inProgress = false;
            CurrentSeconds = 0;
            StopCoroutine(_counting);
        }

        public void StartTimer()
        {
            _inProgress = true;
            CurrentSeconds = MaxSeconds;
            RenderTime();
            _counting = StartCoroutine(Tick());
        }

        private void RenderTime()
        {
            _min = (CurrentSeconds / 60).ToString();
            _sec = (CurrentSeconds % 60).ToString();
            _text.text = _min + ":" + _sec;
        }

        public void IncreaseTime(int amount)
        {
            CurrentSeconds += amount;
            if (CurrentSeconds>MaxSeconds)
            {
                CurrentSeconds = MaxSeconds;
            }
            RenderTime();
        }

        private IEnumerator Tick()
        {
            yield return new WaitForSeconds(1);
            CurrentSeconds -= 1;
            RenderTime();
            if (CurrentSeconds > 0)
            {
                _counting = StartCoroutine(Tick());
            }
            else
            {
                _inProgress = false;
                if (!IsIntro)
                {
                    GameLogicManager.GameEnd();
                }
            }
        }
    }
}
