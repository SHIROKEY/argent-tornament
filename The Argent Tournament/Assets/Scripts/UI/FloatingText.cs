using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FloatingText : MonoBehaviour
    {
        public float TickTime;
        public float LifeTime;
        public float VerticalDelta;

        private float CurrentTime;
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            StartFly();
        }

        public void StartFly()
        {
            StartCoroutine(Shift());
        }

        private IEnumerator Shift()
        {
            if (CurrentTime <= LifeTime)
            {
                _rectTransform.anchoredPosition = _rectTransform.anchoredPosition + new Vector2(0, VerticalDelta);
                yield return new WaitForSeconds(TickTime);
                CurrentTime += TickTime;
                StartCoroutine(Shift());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
