using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHealth : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public float damagePoints = 10f;

    private void OnTriggerEnter(Collider other) {
       // playerHealth.currentHealth -= damagePoints * Time.deltaTime;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if(playerHealth == null){
            return;
        }

       // playerHealth.currentHealth -= damagePoints * Time.deltaTime;
       playerHealth.SendDamage(damagePoints);

    }
}
