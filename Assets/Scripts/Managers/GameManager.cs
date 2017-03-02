using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private TheBrain brain;

    private int currLevelID;
    
    [SerializeField]
    private string resetLevel;

    [SerializeField]
    private bool DebugMode;

    private int numOfKeyEnemies;

    // Use this for initialization
    void Start()
    {
        try
        {
            // TODO: Discover a more efficient way to find the below game object
            brain = GameObject.Find("TheBrain").GetComponent<TheBrain>();
        }
        catch
        {
            Debug.Log("The Brain was not found for this object");
        }

        currLevelID = SceneManager.GetActiveScene().buildIndex;
        //brain.nextSceneIndex = SceneManager.GetSceneByName(resetLevel).buildIndex;

    }

    /// <summary>
    /// Loads the next level based off the given levelID
    /// </summary>
    /// <param name="levelID"></param>
    public void LevelStart(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    // TODO: Control Menu
    // TODO: Transactions

    public void NextLevel()
    {
        SceneManager.LoadScene(currLevelID + 1);
    }

    /// <summary>
    /// 
    /// </summary>
    public void FlipDebugMode ()
    {
        DebugMode = !DebugMode;
    }

    /// <summary>
    /// Handles a game over event. 
    /// </summary>
    public void GameOver ()
    {
        if (!DebugMode)
        {
            SceneManager.LoadScene(resetLevel);
        }
    }

    public void AddKeyEnemy ()
    {
        numOfKeyEnemies++;
    }

    public void RemoveKeyEnemy ()
    {
        numOfKeyEnemies--;
    }
}
