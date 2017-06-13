using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

    [SerializeField]
    Text timeText; //Will assign our Time Text to this variable so we can modify the text it displays.

    bool isPaused; //Used to determine paused state

    public GameObject myplayer; //Player prefab

    void Start()
    {
        UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
        isPaused = false; //make sure isPaused is always false when our scene opens
    }

    void Update()
    {

        //If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            UnPause();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        isPaused = true;
        UIPanel.gameObject.SetActive(true); //turn on the pause menu
        Time.timeScale = 0f; //pause the game
        Movement.allow_move = false; //disable player movement
        myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = false; //disable camera tracker
    }

    public void UnPause()
    {
        isPaused = false;
        UIPanel.gameObject.SetActive(false); //turn off pause menu
        Time.timeScale = 1f; //resume game
        Movement.allow_move = true; //allow player movement
        myplayer.gameObject.GetComponent<Camera_Track_Mouse>().enabled = true; //allow camera tracker
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        isPaused = false;
        Time.timeScale = 1f; //resume game
        SceneManager.LoadScene(0);
    }
}