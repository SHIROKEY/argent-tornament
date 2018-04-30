using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Name : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<Text>();
            Debug.Log(this.GetType() + " loaded");
        }

        public void SetName(string name)
        {
            _text.text = name;
        }
    }
}
