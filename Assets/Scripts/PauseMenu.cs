using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool gameIsPaused;

    // Update is called once per frame
   private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) { // Might be different for other controllers, on my switch pro controller this is the '+' key, 'Home' key doesn't get detected so this is the second best option
            gameIsPaused = !gameIsPaused;

            if (gameIsPaused)
            {
                GamePaused();
            }
            else{
                ResumeGame();
            }
        }
    }

    public void GamePaused(){
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }

    public void setPaused(bool p) {
        this.gameIsPaused = p;
    }

}
