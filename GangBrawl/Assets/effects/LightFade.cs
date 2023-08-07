using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D glow;
    public int steps = 10;
    public float time;
    public float intensityMax = 5;
    public float intensityMin = 0.1f;
    IEnumerator fade()
    {
        float stepSize = time / steps;
        for(int i=0; i<steps; i++)
        {
            yield return new WaitForSeconds(stepSize);
            glow.intensity = Mathf.Lerp(intensityMax,intensityMin,((float)i)/steps);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fade());
    }

    // Update is called once per frame

}
