using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private Text textHolder;
    [SerializeField] private Image bar;
    [SerializeField] private Gradient gradient;
    
    public void SetTime(float time)
    {
        string text = time.ToString("F2");
        if(time < 10f) text = "0" + text;
        textHolder.text = text;

        float ratio = time / 10f; // + menfou + palu + L
        Color color = gradient.Evaluate(ratio);

        textHolder.color = color;
        bar.color = color;
        bar.fillAmount = ratio;
    }
}
