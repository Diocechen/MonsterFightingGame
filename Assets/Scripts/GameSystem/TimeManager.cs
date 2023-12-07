using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private float slowDownFactor = 0.4f;
    private float slownDownLength = 10f;

    public bool SetTimeScaleNormal = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (SetTimeScaleNormal) 
        {
            Time.timeScale += (1f / slownDownLength) * Time.unscaledDeltaTime;
            if (Time.timeScale >= 1)
            {
                SetTimeScaleNormal = false;
                Time.timeScale = 1f;
            }
        }
        //print(Time.timeScale);
    }

    public void SlowMotion()
    {
        Time.timeScale = slowDownFactor;
    }
}
