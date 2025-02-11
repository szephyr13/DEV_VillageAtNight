using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject introScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject youLost;
    [SerializeField] private GameObject youWon;
    [SerializeField] private GameObject sourceSFX;
    [SerializeField] private GameObject sourceBGM;
    [SerializeField] private TextManager introManager;
    [SerializeField] private TextManager endManager;
    [SerializeField] private TextMeshProUGUI screenMessage;

    private int gameMode;

    public int GameMode { get => gameMode; set => gameMode = value; }

    void Start()
    {
        Time.timeScale = 0f;
        gameMode = 0;
        //0 - main menu + options
        //1 - intro
        //2 - game
        //3 - ending
        introManager = introScreen.GetComponent<TextManager>();
        
    }

    private void Update()
    {
        UIManagement();
    }

    //manages the input reading depending on the game mode
    private void UIManagement()
    {
        if (gameMode == 0) //MAIN MENU
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (options.activeSelf == true)
                {
                    BackToMenu();
                }
            }
        }
        else if (gameMode == 1) //INTRO SCREEN
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResetGame();
            }
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                introManager.DisplayNextSentence();
            }
        }
        else if (gameMode == 2) //GAME
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.activeSelf == true)
                {
                    ExitPause();
                }
                else if (pauseMenu.activeSelf == false)
                {
                    PauseMenu();
                }
            }
        }
        else if (gameMode == 3) //END SCREEN
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResetGame();
            }
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                endManager.DisplayNextSentence();
            }
        }
    }

    //shows text in the ui (for recollected items)
    public void showUIText(string text)
    {
        StartCoroutine(textInterface(text));
    }

    //coroutine complementing showUItext, making the text disappear
    private IEnumerator textInterface(string text)
    {
        screenMessage.text = text;
        yield return new WaitForSeconds(5f);
        screenMessage.text = "";
    }


    //resets frequency filter, game mode and scene
    public void ResetGame()
    {
        AudioManager.instance.PlaySFX("UISelect");
        AudioManager.instance.PlayBGM("Menu");
        if (sourceBGM.GetComponent<AudioLowPassFilter>().cutoffFrequency != 5007.7f)
        {
            sourceBGM.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5007.7f;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameMode = 0;
        Time.timeScale = 0f;
    }

    //lowpass filter, sets menu active and stops time
    public void PauseMenu()
    {
        AudioManager.instance.PlaySFX("UISelect");
        sourceBGM.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000.0f;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    //stops lowpass, sets pause menu inactive, restores time flow
    public void ExitPause()
    {
        AudioManager.instance.PlaySFX("UISelect");
        sourceBGM.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5007.7f;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //plays us sound, hides menu, uhnpauses, BGM.
    public void IntroFromMenu()
    {
        AudioManager.instance.PlaySFX("UISelect");
        AudioManager.instance.StopMusic();
        mainMenu.SetActive(false);
        introScreen.SetActive(true);
        gameMode = 1;
        introManager.StartDialogue();
    }

    //sets next screens depending on the text mode it comes from
    public void NextScreen()
    {
        if (gameMode == 1)
        {
            GameFromIntro();
        }
        if (gameMode == 3)
        {
            ResetGame();
        }
    }

    //starts game from the intro screen
    private void GameFromIntro()
    {
        AudioManager.instance.PlayBGM("Game");
        introScreen.SetActive(false);
        gameMode = 2;
        Time.timeScale = 1f;
    }


    //ui sound, hides menu, shows options
    public void Options()
    {
        AudioManager.instance.PlaySFX("UISelect");
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    //ui sound, hides options, shows menu
    public void BackToMenu()
    {
        AudioManager.instance.PlaySFX("UISelect");
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    //opens the losing screen, stoping music and pausing time
    public void YouLost()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySFX("DiedSFX");
        youLost.SetActive(true);
        Time.timeScale = 0f;
    }

    //shows the ending text, stopping time and music
    public void YouWon()
    {
        AudioManager.instance.StopMusic();
        youWon.SetActive(true);
        gameMode = 3;
        endManager.StartDialogue();
        Time.timeScale = 0f;
    }

    //both attached to slide. finds audiosource if null and sets volume by slide
    public void SetVolumeBGM(float volume)
    {
        if (sourceBGM == null)
        {
            sourceBGM = GameObject.Find("BGM");
        }
        sourceBGM.GetComponent<AudioSource>().volume = volume;
    }

    public void SetVolumeSFX(float volume)
    {
        if (sourceSFX == null)
        {
            sourceBGM = GameObject.Find("SFX");
        }
        sourceSFX.GetComponent<AudioSource>().volume = volume;
    }
}
