using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Class Variables
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private static bool gameIsPaused;
    [SerializeField] private AudioListener audioListener;
    public bool externalPause = false;

    // Update is called once per frame
   private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7) || externalPause) { // Might be different for other controllers, on my switch pro controller this is the '+' key, 'Home' key doesn't get detected so this is the second best option
            gameIsPaused = !gameIsPaused;
            if (gameIsPaused || externalPause) //loop to compare the bool game is paused
            {
                GamePaused(); 
            }
            else{
                ResumeGame();
            }
            if (externalPause)
                externalPause = false;
        }
    }

    //function for when the game is called to be paused
    public void GamePaused(){
        Time.timeScale = 0;   //time scale set to zero to 'freeze' the game
        pauseMenuUI.SetActive(true); //pauseMenuUI become visible to the player
        AudioListener.pause = true;
    }

    //function for when the game is to be resumed from the pause menu
    public void ResumeGame(){
        Time.timeScale = 1;  //Time is resumed
        pauseMenuUI.SetActive(false); //pauseMenuUI is made non visible
        AudioListener.pause = false;
        setPaused(false);
    }

    //setter
    public void setPaused(bool p) {
        gameIsPaused = p;
    }
    public bool getPaused() {
        return gameIsPaused;
    }

    public void setExternalPause(bool b) {
        this.externalPause = b;
    }
}
