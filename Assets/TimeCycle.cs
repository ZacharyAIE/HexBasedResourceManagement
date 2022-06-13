using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceManagement;
using DG.Tweening;

public enum DayState
{
    DAY,
    NIGHT
}

public class TimeCycle : MonoBehaviour
{
    public Light sunlight;

    public int secondsUntilDay = 60;
    public int secondsUntilNight = 80;
    public float daySpeed = 2;
    int currentTime = 0;
    public DayState dayState;

    private void Start()
    {
        TickSystem.Instance.OnTick.AddListener(() => { /*CheckTime();*/ currentTime++; });

        dayState = DayState.DAY;
    }

    private void Update()
    {
        sunlight.gameObject.transform.Rotate(new Vector3(1,0,0), Time.deltaTime * daySpeed);
        if(sunlight.gameObject.transform.rotation.eulerAngles.x < 0 || sunlight.gameObject.transform.rotation.eulerAngles.x > 180)
        {
            sunlight.enabled = false;
            dayState = DayState.NIGHT;
        }
        else
        {
            sunlight.enabled = true;
            dayState = DayState.DAY;
        }
    }

    void CheckTime() 
    {
        if(currentTime > secondsUntilDay && dayState == DayState.NIGHT)
        {
            dayState = DayState.DAY;
            SetLight(DayState.DAY);
            currentTime = 0;
        }
        else if(currentTime > secondsUntilNight && dayState == DayState.DAY)
        {
            dayState = DayState.NIGHT;
            SetLight(DayState.NIGHT);
            currentTime = 0;
        }
    }

    void SetLight(DayState ds)
    {
        if (ds == DayState.NIGHT)
            sunlight.intensity = Mathf.Lerp(sunlight.intensity, 0, 40000);
        else if (ds == DayState.DAY)
            sunlight.intensity = Mathf.Lerp(sunlight.intensity, 1.77f, 49999);
    }
}
