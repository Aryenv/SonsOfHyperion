using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCycle : MonoBehaviour
{
    public  float  actualSeconds;
    private float actualMinutes;
    public  int  actualDay;
    [Space]
    public float CycleDuration;
    [Space]
    public float DayDuration;
    public float NightDuration;

    public TimerStats myGeneralState;

    private void Update()
    {
        Timer();
    }
    private void Timer()
    {
        actualSeconds += Time.deltaTime;
        actualMinutes = actualSeconds / 60;
        //Duracion del ciclo
        if (actualSeconds >= CycleDuration)
        {
            actualSeconds = 0;
            actualDay++;
        }
        //Duracion del dia
        if (actualSeconds <= DayDuration)
        {
            myGeneralState = TimerStats.Day;
        }

        //Duracion de la noche
        else
        {
            myGeneralState = TimerStats.Night;
        }
    }
}
