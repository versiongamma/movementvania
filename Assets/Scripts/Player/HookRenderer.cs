using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles rendering of the string thing that attaches the player to the point they are swinging from
public class HookRenderer : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    private Animator animator;
    private Vector2 target;

    void Start() {
        animator = GetComponent<Animator>();
    }

    // Attach the hook to a given [target]
    public void Attach(Vector2 target) {
        this.target = target;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        animator.SetTrigger("Launch");
        StartCoroutine(Lengthen());
    }

    // Dettachs the hook and stops rendering
    public void Dettach() {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    // Lengths the hook over 100ms from a length of 0, to a length that is the distance between the 
    // player and the target of their swing
    IEnumerator Lengthen() {
        float t = 0;
        Vector2 pos = player.transform.position;
        while (t < .1) {
            float nextScale = Mathf.Lerp(1, (Vector2.Distance(player.transform.position, target)), t / .1f);
            transform.localScale = new Vector3(nextScale, 1, 1);

            t += Time.deltaTime;
            yield return 0;
        }

        // Make sure the hook is at the correct length before exiting, as it can exit the above while loop without the Lerp quite reaching maximum,
        // especially at low frame rates
        transform.localScale = new Vector3(Vector2.Distance(player.transform.position, target) + .5f, 1, 1); 
        yield break;
    }

    // Always rotate the hook to whatever the current target is. That way, we never have to worry about it's rotation. Inefficent, sure, but 
    // if we need to start optimising for performance I think we might actually need better computers.
    void Update() {
        transform.right = (target - (Vector2) player.transform.position).normalized;   
    }


}
