using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1.0f)] private float time;
    [SerializeField] private float fullDayLength;
    [SerializeField] private float startTime = 0.4f;
    [SerializeField] private Vector3 noon;
    private float timeRate;


    [Header("Sun")]
    [SerializeField] private Light sun;
    [SerializeField] private Gradient sunColor;
    [SerializeField] private AnimationCurve sunIntensity;
    
    [Header("Moon")]
    [SerializeField] private Light moon;
    [SerializeField] private Gradient moonColor;
    [SerializeField] private AnimationCurve moonIntensity;


    [Header("Other Lighting")]
    [SerializeField] private AnimationCurve lightingIntensityMultiplier;
    [SerializeField] private AnimationCurve reflectionsIntensityMultiplier;

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    private void Update()
    {
        //increment time
        time += timeRate * Time.deltaTime;

        if(time >= 1f)
        {
            time = 0f;
        }

        //light rotation
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        //light intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);  

        // change colors
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);  


        // enable/disable sun
        if(sun.intensity == 0f && sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(false);
        }else if (sun.intensity > 0f && !sun.gameObject.activeInHierarchy)
        {
            sun.gameObject.SetActive(true);
        }
        
        // enable/disable moon
        if(moon.intensity == 0f && moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(false);
        }else if (moon.intensity > 0f && !moon.gameObject.activeInHierarchy)
        {
            moon.gameObject.SetActive(true);
        }


        // lighting and reflections intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
    }
}
