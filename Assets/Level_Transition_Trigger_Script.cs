using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Transition_Trigger_Script : MonoBehaviour
{
    
    [SerializeField] public string levelName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            StartCoroutine("FadeToBlack");
        }
    }

    private void loadLevel() 
    {
        Debug.Log("Loading level: " + this.levelName);
        new PlayerData().populateDataLevelChange(this.levelName);
        SceneManager.LoadScene(this.levelName, LoadSceneMode.Single);
        return;
    }

    IEnumerator FadeToBlack() 
    {
        GameObject fadeToBlackBox = GameObject.Find("Level_Transition");
        if (fadeToBlackBox) {
            fadeToBlackBox.SetActive(true);
            for (float alphaValue = 0f; alphaValue < 1.2f; alphaValue = alphaValue + 0.1f) 
            {
                SpriteRenderer r = fadeToBlackBox.GetComponent<SpriteRenderer>();
                Color newColor = r.color;
                newColor.a = alphaValue;
                newColor.r = 0;
                newColor.g = 0;
                newColor.b = 0;
                r.color = newColor;
                yield return new WaitForSeconds(.05f);
            }
            loadLevel();
        }
    }

}
