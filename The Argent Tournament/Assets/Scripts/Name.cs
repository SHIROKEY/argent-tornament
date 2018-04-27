using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    public void SetName(string name)
    {
        _text.text = name;
    }
}
