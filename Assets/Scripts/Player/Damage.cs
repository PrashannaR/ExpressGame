using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//testing for rebase 

//https://www.youtube.com/watch?v=TbVuM9lL-EQ
public class Damage : MonoBehaviour
{

public float DamagePoints = 50f;
    private void OnTriggerEnter(Collider other) {
        
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();

        if(healthSystem == null){
            return;
        }

        healthSystem.HealthPoints -= DamagePoints * Time.deltaTime;
    }

}
