using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles rendering the indicator that shows where the player can swing from
public class HookIndicatorRenderer : MonoBehaviour {

    private bool show;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Simple update that occelates the alpha of the sprite sinusoidally, when the indicator should be shown
    void Update() {
        spriteRenderer.color = new Color(1, 1, 1, show ? (Mathf.Sin(6 * Time.time) / 4) + .75f : 0);
    }

    public void Display(Vector2 target) {
        show = true;
        transform.position = target;
    }

    public void Remove() { show = false; }
}
