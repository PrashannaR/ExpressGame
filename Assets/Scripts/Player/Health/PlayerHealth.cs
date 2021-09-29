using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=gc0_Xna8Ofw
public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public Slider HealthSlider;

    public DamageHealth damageHealth;
    public float dValue;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        HealthSlider.value = currentHealth;
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.P)){
            //Destroy(gameObject);
            SendDamage(Random.Range(10,20));
        } 

       // SendDamage(damageHealth.damagePoints);

        if(currentHealth < 0){
            Destroy(gameObject);
        }
    }

    public void SendDamage(float damageValue){
        currentHealth -= damageValue;
        HealthSlider.value = currentHealth;
    }
}
