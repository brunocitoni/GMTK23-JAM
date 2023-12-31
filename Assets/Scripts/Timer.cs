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
        int timerInt = (int)timer;

        if (timerInt == 0)
        {
            if (this.gameObject.name == "UI Canvas Variant")
            {
                timerText.text = "GO!";
            }
            else
            {
                timerText.text = timerInt.ToString();
            }
        }
        else
        {
            timerText.text = timerInt.ToString();
        }
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
    }
}
