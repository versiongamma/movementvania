using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupObject : MonoBehaviour {

    enum Key {
        Jump,
        Dash,
    }

    [SerializeField] private GameObject canvas, cam;
    [SerializeField] public Text powerupNameUI, actionUI, keyUI, resultUI;
    [SerializeField] public string powerupName, action, result;  
    [SerializeField] private PowerUps powerup; 
    [SerializeField] private Key key;
    private KeyCode keyString;


    private AudioSource sound;

    void Start() {
        sound = GetComponent<AudioSource>();

        switch(key) {
            case Key.Jump:
                keyString = InputController.jump;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerEquipment>().GivePowerup(powerup);
            canvas.transform.position = (Vector2) cam.transform.position + new Vector2(0, -5);
            
            sound.Play();
            StartCoroutine(ShowTooltip(canvas));
        }
    }

    IEnumerator Fade(CanvasRenderer[] tooltips, float time, bool direction) {
        foreach (CanvasRenderer tooltip in tooltips) tooltip.SetAlpha(direction ? 0 : 1);
        float t = 0;

        while (t < time) {
            foreach (CanvasRenderer tooltip in tooltips) tooltip.SetAlpha(tooltip.GetAlpha() + (Time.unscaledDeltaTime / time) * (direction ? 1 : -1));
            t += Time.unscaledDeltaTime;
            yield return 0;
        }

        yield break;
    }

    IEnumerator ShowTooltip(GameObject canvas) {
            
            powerupNameUI.text = powerupName;
            actionUI.text = action;
            keyUI.text = "x";
            resultUI.text = result;

            canvas.SetActive(true);
            StartCoroutine(Fade(canvas.GetComponentsInChildren<CanvasRenderer>(), .15f, true));


            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(6);
            Time.timeScale = 1;

            GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0, 0);
            StartCoroutine(Fade(canvas.GetComponentsInChildren<CanvasRenderer>(), .1f, false));
            yield return new WaitForSecondsRealtime(.1f);
            
            Destroy(this.gameObject);
        }
        
}
