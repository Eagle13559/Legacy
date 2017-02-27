using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour
{

    /// <summary>
    /// Controls the total limit of time
    /// </summary>
    private float TimeLimit;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Text TimerText;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Image TimerBar;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private PlayerController player;

    /// <summary>
    /// Represents the current time of this timerController
    /// </summary>
    public float CurrTime;

    /// <summary>
    /// 
    /// </summary>
    private FillControl TimerFill;

    /// <summary>
    /// 
    /// </summary>
    void Start () {
        // Use this for initialization
    }

    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            ReduceTimer(Time.deltaTime);

            if (CurrTime <= 0)
            {
                player.Die();
            }

            TimerDisplay();

            yield return new WaitForEndOfFrame();
        }
        
    }


    public void StartTimer (float TotalTime)
    {
        TimeLimit = TotalTime * 60;
        CurrTime = TimeLimit;

        TimerFill = new FillControl(TimerBar);

        StartCoroutine(UpdateTimer());
    }


    /// <summary>
    /// Displays all the appropriate timer display items. 
    /// </summary>
    private void TimerDisplay()
    {
        if (!float.IsPositiveInfinity(TimeLimit))
            TimerFill.ChangeBarFill(CurrTime / TimeLimit);
        else
            TimerFill.ChangeBarFill(1);

        TimerText.text = ToString();
    }

    /// <summary>
    /// Reduces the current timer appropriately based off the damage argument.
    /// </summary>
    /// <param name="damage"> Damage recieved </param>
    /// <returns></returns> 
    public void ReduceTimer(float damage)
    {
        if (CurrTime - damage > 0)
        {
            CurrTime -= (damage);
        }
        else
        {
            CurrTime = 0;
        }
    }

    /// <summary>
    /// Resets the timer to its' original value. 
    /// </summary>
    private void ResetTimer()
    {
        CurrTime = TimeLimit;
    }

    /// <summary>
    /// Display the current time
    /// </summary>
    public override string ToString()
    {
        double minutes = CurrTime / 60; //Divide the guiTime by sixty to get the minutes.
        double seconds = CurrTime % 60;//Use the euclidean division for the seconds

        return string.Format("{0:00} : {1:00}", Math.Floor(minutes), Math.Floor(seconds));
    }
}
