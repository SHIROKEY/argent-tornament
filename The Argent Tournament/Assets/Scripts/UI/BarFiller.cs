using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI
{
    public class BarFiller: MonoBehaviour
    {
        private Image _indicator;

        private void Awake()
        {
            Debug.Log(this.GetType() + " loaded");
        }

        public void InitializeIndication()
        {
            _indicator = GetComponent<Image>();
        }

        public void RenderIndication(float amount)
        {
            _indicator.fillAmount = amount;
        }
    }
}
