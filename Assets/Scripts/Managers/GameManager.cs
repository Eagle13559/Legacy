using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {

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
        Debug.Log("Sorry");
    }
}
