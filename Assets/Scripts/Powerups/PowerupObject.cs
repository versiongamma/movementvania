using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the powerup object in the level that the player picks up to aquire new powerups

// Represents the keys that respond to a specified powerup, to be displayed on the popup 
public enum Key {
    Jump,
    Dash,
    Swing,
}

public class PowerupObject : MonoBehaviour {

    [SerializeField] private GameObject canvas, cam;
    [SerializeField] private PowerUps powerup; 

    private AudioSource sound;

    void Start() {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {

            other.gameObject.GetComponent<PlayerEquipment>().GivePowerup(powerup);
            canvas.transform.position = (Vector2) cam.transform.position + new Vector2(0, 0);
            
            sound.Play();
            StartCoroutine(ShowTooltip(canvas));
        }
    }

    // Takes a given CanvasRenderer [tooltips] and fades it's opacity either in or out based on the [direction] (true = in, false = out), over a given [time]
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
