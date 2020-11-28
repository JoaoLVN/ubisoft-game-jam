using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimerText : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();    
    }

    private void Update()
    {
        int seconds = Mathf.CeilToInt(GameManager.TimeLeft);
        _text.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }
}
