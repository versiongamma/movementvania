using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupObject : MonoBehaviour {

    [SerializeField] private string powerupName, message; 
    [SerializeField] private PowerUps powerup; 

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            other.gameObject.GetComponent<PlayerEquipment>().GivePowerup(powerup);
            Destroy(this.gameObject);
        }
    }
}
