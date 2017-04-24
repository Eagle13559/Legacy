using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour
{

    /// <summary>
    /// Controls the total limit of time
    /// </summary>
    public float TimeLimit { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Text TimerText;

    /// <summary>
    /// Represents the image that will show the players curent time
    /// </summary>
    [SerializeField]
    private Image TimerBar;

    /// <summary>
    /// Represents the tip that will go on the timer;
    /// </summary>
  //  [SerializeField]
// private Image TimerTip;

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

    private bool paused;

    public TimerController(float TotalTime, Sprite incense, Image refToTimerBar, Text refToTimerText)
    {
        this.TimerBar = refToTimerBar;
        this.TimerText = refToTimerText;

        TimeLimit = TotalTime * 60;
        CurrTime = TimeLimit;

        if (incense != null)
        {
            TimerBar.sprite = incense;
        }

        TimerFill = new FillControl(TimerBar);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Intialize(float TotalTime, Sprite incense)
    {
        TimeLimit = TotalTime * 60;
        CurrTime = TimeLimit;

        if (incense != null)
        {
            TimerBar.sprite = incense;
        }

        TimerFill = new FillControl(TimerBar);
    }

    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {
        while (true)
        {
           ReduceTimer(Time.deltaTime);
           
            //TimerTip.rectTransform.offsetMax = new Vector2(TimerTip.rectTransform.offsetMax.x - Time.deltaTime, TimerTip.rectTransform.offsetMax.y);
           // Vector3 tipMoveVector = new Vector3(TimerTip.rectTransform.position.x - (Time.deltaTime), TimerTip.rectTransform.position.y, 0);
           // TimerTip.rectTransform.transform.position = tipMoveVector;

            if (CurrTime <= 0)
           {
              player.Die();
           }
           
            TimerDisplay();

            yield return new WaitForEndOfFrame();
        }
        
    }


    public void StartTimer ()
    {
        StartCoroutine(UpdateTimer());
    }


    /// <summary>
    /// Displays all the appropriate timer display items. 
    /// </summary>
    public void TimerDisplay()
    {
        if (!float.IsPositiveInfinity(TimeLimit))
        {
            TimerFill.ChangeBarFill(CurrTime / TimeLimit);
        }     
        else
            TimerFill.ChangeBarFill(1);

        TimerText.text = ToString();
    }

    /// <summary>
    /// Changes the current sprite being used for the timer to the sprite given. 
    /// </summary>
    /// <param name="newSprite"></param>
    public void ChangeTimerBarSprite (Sprite newSprite)
    {
        TimerBar.sprite = newSprite;
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
    /// Reduces the current timer appropriately based off the damage argument.
    /// </summary>
    /// <param name="damage"> Damage recieved </param>
    /// <returns></returns> 
    public void IncreaseTimer(float heals)
    {
        if (CurrTime + heals < TimeLimit)
        {
            CurrTime += (heals);
        }
        else
        {
            CurrTime = TimeLimit;
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
    /// Will pause the current timer until it is restarted. 
    /// </summary>
    private void PauseTimer()
    {
        paused = true;
    }

    /// <summary>
    /// Will unpause the current timer.
    /// </summary>
    private void UnPauseTimer ()
    {
        paused = false;
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
