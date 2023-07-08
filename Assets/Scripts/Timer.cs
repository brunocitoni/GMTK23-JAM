using System;
using TMPro;
using UnityEngine;

/*Implements a countdown timer*/
public class Timer : MonoBehaviour
{

    public TMP_Text timerText;
    private float timeDuration;
    private float timer;
    private bool isCounting = false;
    public bool display = false;

    // events
    public delegate void OnTimerElapsed();
    public event OnTimerElapsed TimerElapsed;

    public void SetDuration(float duration)
    {
        if (!isCounting)
        {
            timeDuration = duration;
        }
        else
        {
            Debug.Log("Timer needs to be stopped before it can be set again");
        }
    }

    public void ResumeTimer()
    {
        isCounting = true;
    }

    public void RestartTimer()
    {
        timer = timeDuration;
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
        timer = 0;
    }

    public void PauseTimer()
    {
        isCounting = false;
    }

    public float GetTimeLeft()
    {
        return timer;
    }

    public void OnTimerFinish()
    {
        TimerElapsed?.Invoke();
    }

    public void FormatAndDisplayTimer()
    {
        // Convert timer to TimeSpan
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

        // Format the timer as seconds
        string formattedTime = string.Format("{0:00}", timeSpan.Seconds);

        // Update the timer text
        timerText.text = formattedTime;
    }

    private void Update()
    {
        if (isCounting)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (display)
                {
                    FormatAndDisplayTimer();
                }
            }
            else
            {
                StopTimer();
                OnTimerFinish();
            }
        }

        Debug.Log(timer);
    }
}
