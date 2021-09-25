using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider slider;
    public float StartHealth = 100f;

    public float HealthPoints{
        get{
            return _HealthPoints;
        }
        set{
            _HealthPoints = Mathf.Clamp(value, 0f, 100f);

            if(_HealthPoints <= 0f){
                //Dead
                Die();
            }
        }
    }
    [SerializeField]
    private float _HealthPoints = 100f;


    // Start is called before the first frame update
    void Start()
    {
        HealthPoints = StartHealth;
    }//start

    void Die(){
       Destroy(gameObject);
    }

    public void SetHealth(float HealthPoints){
        slider.value = HealthPoints;
    }
  
}
