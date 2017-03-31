using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

    private TheBrain brain;

    private int currLevelID;

    public AudioMixerSnapshot ispaused;
    public AudioMixerSnapshot unpaused;

    [SerializeField]
    private string resetLevel;

    [SerializeField]
    private string nextLevel;

    [SerializeField]
    private string MainMenuTag;

    [SerializeField]
    private bool DebugMode;

    [SerializeField]
    private GameObject pauseMenu;

    private bool paused = false;

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
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        //brain.nextSceneIndex = SceneManager.GetSceneByName(resetLevel).buildIndex;
    }

    void Update()
    {
        if (pauseMenu != null && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button9)))
        {
            pauseControl(!paused);
        }
    }

    /// <summary>
    /// Determine if key was pressed to intiate pause control and will set the state of pause menu to give value if key was pressed. 
    /// </summary>
    /// <param name="state"></param>
    private void pauseControl(bool state)
    {
        pauseMenu.SetActive(state);
        if (state)
            PauseGame();
        else
            UnPauseGame();
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
        SceneManager.LoadScene(nextLevel);
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
        brain.resetTime();
    }

    public int GetNumOfKeyEnemiesAlive()
    {
        return numOfKeyEnemies;
    }

    public void AddKeyEnemy ()
    {
        numOfKeyEnemies++;
    }

    public void RemoveKeyEnemy ()
    {
        numOfKeyEnemies--;
    }

    /// <summary>
    /// Handles a LevelFinished Event
    /// </summary>
    public void LevelFinished()
    {
        NextLevel();
    }

    public void PauseGame ()
    {
        paused = true;
        Time.timeScale = 0;
        Lowpass();

    }

    public void UnPauseGame()
    {
        paused = false;
        Time.timeScale = 1;
        Lowpass();
    }

    void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            ispaused.TransitionTo(.01f);
        }

        else
        {
            unpaused.TransitionTo(.01f);
        }
    }

    public void GoToMain()
    {
        UnPauseGame();
        SceneManager.LoadScene(MainMenuTag);    
    }

    public void KillGame()
    {
        // TODO: Do we need to wait for any threads, coroutines or processes to finish. 
        Application.Quit();
    }
}