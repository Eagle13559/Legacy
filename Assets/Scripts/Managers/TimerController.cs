using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour
{

    /// <summary>
    /// Controls the total limit of time
    /// </summary>
    [SerializeField]
    private float TimeLimit;

    /// <summary>
    /// Represents the current time of this timerController
    /// </summary>
    private float currTime;


    /// <summary>
    /// Deals with the Brain
    /// </summary>
    private GameManager Brain;

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
    private FillControl TimerFill;

    // Use this for initialization
    void Start()
    {
        Brain = GetComponent<GameManager>();

        if (Brain == null)
        {
            Debug.Log("Can not find GameManager for " + gameObject.name + " of instance " + GetInstanceID());
        }

        TimeLimit = TimeLimit * 60;
        currTime = TimeLimit;

        TimerFill = new FillControl(TimerBar);
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= Time.deltaTime;

        if (currTime < 0)
        {
            EndOfTimer();
        }

        TimerDisplay();
    }

    /// <summary>
    /// Displays all the appropriate timer display items. 
    /// </summary>
    private void TimerDisplay()
    {
        TimerFill.ChangeBarFill(currTime/TimeLimit);

        TimerText.text = ToString();
    }

    /// <summary>
    /// Reduces the current timer appropriately based off the damage argument.
    /// </summary>
    /// <param name="damage"> Damage recieved </param>
    /// <returns></returns> 
    public void ReduceTimer(float damage)
    {
        currTime -= (damage);
    }

    /// <summary>
    /// When the timer ends, this method will be called and deal with the fallout
    /// </summary>
    private void EndOfTimer()
    {
        // TODO: Determine if it needs to be a coroutine. 
       ResetTimer();

       Brain.GameOver(); 
    }

    /// <summary>
    /// Resets the timer to its' original value. 
    /// </summary>
    private void ResetTimer()
    {
        currTime = TimeLimit;
    }

    /// <summary>
    /// Display the current time
    /// </summary>
    public override string ToString()
    {
        double minutes = currTime / 60; //Divide the guiTime by sixty to get the minutes.
        double seconds = currTime % 60;//Use the euclidean division for the seconds

        return string.Format("{0:00} : {1:00}", Math.Floor(minutes), Math.Floor(seconds));
    }
}
