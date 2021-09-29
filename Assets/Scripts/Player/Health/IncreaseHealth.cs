using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
   public PlayerHealth playerHealth;
   public float healPoints = 10f;

   private void OnTriggerEnter(Collider other) {
       PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

       if(playerHealth == null){
           return;

       }

       playerHealth.SendHeal(healPoints);
   }
}
