using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupObject : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField] private string powerupName, message; 
    [SerializeField] private PowerUps powerup; 

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerEquipment>().GivePowerup(powerup);
            Destroy(this.gameObject);
        }
    }
}
