using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoucherDeSoleil : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]
    public GameObject sun;
    [HideInInspector]
    public Light sunLight;

    public bool RotationOuPas = true;

    [Range(0, 24)]
    public float timeOfDay = 18;

    [HideInInspector]
    public Color sunLightColor;

    public float secondsPerMinute = 60;
    [HideInInspector]
    public float secondsPerHour;
    [HideInInspector]
    public float secondsPerDay;

    public Color earlyMorningColor = Color.yellow;
    public Color lateMorningColor = Color.yellow;
    public Color afterNoonColor = Color.white;
    public Color earlyEveningColor = Color.yellow;
    public Color lateEveningColor = Color.yellow;
    public Color MiddlelateEveningColor = Color.yellow;
    public Color EndlateEveningColor = Color.yellow;
    public Color nightTimeColor = Color.blue;

    public float timeMultiplier = 10;
    public Gradient sunGradient;

    void Start()
    {
        sun = gameObject;
        sunLight = gameObject.GetComponent<Light>();
        sunLightColor = sunLight.color;

        secondsPerHour = secondsPerMinute * 60;
        secondsPerDay = secondsPerHour * 24;
    }


    void Awake()
    {
        SetupGradient();
    }

    void SetupGradient()
    {
        GradientColorKey[] colorKey = new GradientColorKey[8];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[8];

        // the times are normalized
        // 0 is the start, 1.0 is the end

        colorKey[0].color = earlyMorningColor;
        colorKey[0].time = 0.2f;
        colorKey[1].color = lateMorningColor;
        colorKey[1].time = 0.4f;
        colorKey[2].color = afterNoonColor;
        colorKey[2].time = 0.6f;
        colorKey[3].color = earlyEveningColor;
        colorKey[3].time = 0.8f;
        colorKey[4].color = lateEveningColor;
        colorKey[4].time = 0.9f;
        colorKey[5].color = MiddlelateEveningColor;
        colorKey[5].time = 0.93f;
        colorKey[6].color = EndlateEveningColor;
        colorKey[6].time = 0.95f;
        colorKey[7].color = nightTimeColor;
        colorKey[7].time = 0.97f;

        for (int i = 0; i < alphaKey.Length; ++i)
            alphaKey[i].time = 1.0f;

        sunGradient.SetKeys(colorKey, alphaKey);
    }


    // Update is called once per frame
    void Update()
    {
        SunUpdate();

        timeOfDay += (Time.deltaTime / secondsPerDay) * timeMultiplier;

        if (timeOfDay >= 24)
        {
            secondsPerMinute = -60;
        }
    }

    public void SunUpdate()
    {
        if (RotationOuPas == true)
        sun.transform.localRotation = Quaternion.Euler(((timeOfDay / 24) * 360f) -180, 90, 0);
        // Your setting the localRotatoin from -90 to +270 so we need to normalize that value
        // from 0.0 to 1.0;   I assume timeOfDay =0 (angle = -90) is our starting night time)

        sunLight.color = sunGradient.Evaluate(timeOfDay / 24f);
    }
}
