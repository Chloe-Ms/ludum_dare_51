using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 1;
    [SerializeField] private Light2D globalLight;

    public void LerpLight(float target) {StartCoroutine(FadeLightCo(target));}
    private IEnumerator FadeLightCo(float target)
    {
        float t = 0;
        float origin = globalLight.intensity;
        while (t < 1) 
        {
            t += lerpSpeed * Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(origin, target, t);
            yield return null;
        }
        globalLight.intensity = target;
    }
}
