using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookIndicatorRenderer : MonoBehaviour {

    private bool show;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        spriteRenderer.color = new Color(1, 1, 1, show ? (Mathf.Sin(6 * Time.time) / 4) + .75f : 0);
    }

    public void Display(Vector2 target) {
        show = true;
        transform.position = target;
    }

    public void Remove() { show = false; }
}
