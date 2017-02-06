using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    
    /// <summary>
    /// All the timer control properties:
    ///     - Timer itself
    ///     - Display of the time
    /// </summary>
    private TimerController timer;


    // Use this for initialization
    void Start ()
    {
        timer = GetComponent<TimerController>();

        if (timer == null)
        {
            Debug.Log("Can not find TimerController for " + gameObject.name + " of instance " + GetInstanceID());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    /// <summary>
    /// Loads the next level based off the given levelID
    /// </summary>
    /// <param name="levelID"></param>
    public void LevelStart (int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    // TODO: Control Menu
    // TODO: Transactions


    /// <summary>
    /// Handles a game over event. 
    /// </summary>
    public void GameOver ()
    {
        timer.UpdateTimer((time) => timer.TimeLimit);
    }

    /// <summary>
    /// Takes damage and will perform necessary functions. 
    /// </summary>
    /// <param name="damage"></param>
    public void ReportDamage(float damage)
    {
        timer.UpdateTimer((time) => (time - damage));
    }
}
