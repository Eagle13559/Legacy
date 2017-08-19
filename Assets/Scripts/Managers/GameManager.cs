using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;
using System.Text.RegularExpressions;

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

    [SerializeField]
    private GameObject pauseHolder;

    [SerializeField]
    private GameObject helpHolder;

    [SerializeField]
    private AudioSource[] soundSystem;

    private bool paused = false;

    private bool _inShop;

    [SerializeField]
    private Text CrowCount;
    private int numOfKeyEnemies;
    private GameObject _closeDoors;
    float _closeDoorTime = 1.2f;
    float _closeDoorTimer = 0f;
    bool _transitioning = false;
    bool _restarting = false;

    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("TheBrain") != null)
        {
            brain = GameObject.Find("TheBrain").GetComponent<TheBrain>();
        }
        else
        {
            Debug.Log("The Brain was not found for this object");
            brain = new TheBrain();

            brain.Time = float.PositiveInfinity;
            brain.PlayersMoney = 100;
            brain.InitalizePlayerItemCount();
            brain.InitalizePlayerIncenseCount();
            brain.currIncense = TheBrain.IncenseTypes.Base;
        }

        currLevelID = SceneManager.GetActiveScene().buildIndex;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        //brain.nextSceneIndex = SceneManager.GetSceneByName(resetLevel).buildIndex;
        _closeDoors = GameObject.Find("CloseDoors");
        _closeDoors.SetActive(false);

        if (!Regex.IsMatch(SceneManager.GetActiveScene().name, "Shop"))
        {
            _inShop = false;
        }
        else
        {
            _inShop = true;
        }

        foreach (AudioSource sound in soundSystem) {
            sound.Play();
        }

        //nextLevel = SceneManager.GetSceneByBuildIndex(brain.nextSceneIndex).name;
    }

    void Update()
    {
        if (pauseMenu != null && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button9)))
        {
            pauseControl(!paused);
        }
        if (_transitioning)
        {
            _closeDoorTimer += Time.deltaTime;
            if (_closeDoorTimer >= _closeDoorTime)
            {
                if (!_restarting)
                {
                    if (!_inShop)
                        SceneManager.LoadScene("Shop");
                    else
                        SceneManager.LoadScene(brain.nextSceneIndex);
                }   
                else
                    SceneManager.LoadScene(resetLevel);
            }
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
        //SceneManager.LoadScene(nextLevel);
        if (!_inShop)
        {
            if (nextLevel.Equals("MainMenu"))
            {
                GameOver();
                return;
            }
            else
            {
                brain.nextSceneIndex = nextLevel;
            }
           
        }
        _transitioning = true;
        _closeDoors.SetActive(true);
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

       _transitioning = true;
       _restarting = true;
       _closeDoors.SetActive(true);
        //SceneManager.LoadScene(resetLevel);
    }

    public int GetNumOfKeyEnemiesAlive()
    {
        return numOfKeyEnemies;
    }

    public void AddKeyEnemy ()
    {
        numOfKeyEnemies++;
        CrowCount.text = GetNumOfKeyEnemiesAlive().ToString();
    }

    public void RemoveKeyEnemy ()
    {
        numOfKeyEnemies--;
        CrowCount.text = GetNumOfKeyEnemiesAlive().ToString();
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

        //makes sure that the help holder is closed and pause menu is open when closing the overall menu
        //so that when reopening pause it opens to the pause menu and not the help menu
        pauseHolder.SetActive(true);
        helpHolder.SetActive(false);
        Time.timeScale = 1;
        Lowpass();
    }

    // alters sound while game is paused
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

    public void ResetGame()
    {
        brain.resetBrain();
    }
}