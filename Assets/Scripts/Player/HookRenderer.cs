using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRenderer : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    private Animator animator;
    private Vector2 target;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void Attach(Vector2 target) {
        this.target = target;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        animator.SetTrigger("Launch");
        StartCoroutine(Lengthen());
    }

    public void Dettach() {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    IEnumerator Lengthen() {
        float t = 0;
        Vector2 pos = player.transform.position;
        while (t < .1) {
            float nextScale = Mathf.Lerp(1, (Vector2.Distance(player.transform.position, target)), t / .1f);
            transform.localScale = new Vector3(nextScale, 1, 1);

            t += Time.deltaTime;
            yield return 0;
        }

        transform.localScale = new Vector3(Vector2.Distance(player.transform.position, target) + .5f, 1, 1);
        yield break;
    }

    // Update is called once per frame
    void Update() {
        transform.right = (target - (Vector2) player.transform.position).normalized;   
    }


}
