using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private TimerController timer;

	// Use this for initialization
	void Start () {
        GameObject timeController = GameObject.FindGameObjectWithTag("Timer");

        if (timeController != null)
        {
            timer = timeController.GetComponent<TimerController>();
        }
        else
        {
            Debug.Log("Can not find TimerController for " + gameObject.name + " of instance " + GetInstanceID());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // TODO: Start Level
    // TODO: Control Menu
    // TODO: Transactions
    // TODO: Transitions
    // TODO: Time Left
    // TODO: Game Over

    /// <summary>
    /// Handles a game over event. 
    /// </summary>
    public void GameOver ()
    {

    }
}
