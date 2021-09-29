using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=QKhn2kl9_8I SETUP
//https://www.youtube.com/watch?v=oqidgRQAMB8 SHOOT
public class Turret : MonoBehaviour
{

    private Transform target;

    [Header("Range and Fire")]
    public float range = 45f;
    //shooting
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Player Setup")]

    public string PlayerTag = "Player";


    //part of the turret for rotation
    public Transform RotatingPart;

    //bullet
    public GameObject bulletPre;
    public Transform firePoint;




    // Start is called before the first frame update
    void Start()
    {
        //calls something less frequently
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget(){

        GameObject[] players= GameObject.FindGameObjectsWithTag(PlayerTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;


        //finds player and stores its position 
        foreach (GameObject player in players ){
        
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if(distanceToPlayer < shortestDistance ){
               
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }

        if(nearestPlayer != null && shortestDistance <= range){
            target = nearestPlayer.transform;
        }else{
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            return;
        }
        //rotate the turret
        Vector3 dir = target.position - transform.position;
        Quaternion lookAround = Quaternion.LookRotation(dir);
        Vector3 rotation = lookAround.eulerAngles;
        RotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);;

        //shooting
        if(fireCountDown <= 0f){
            Shoot();
            fireCountDown = 1f/fireRate;
        }

        fireCountDown -= Time.deltaTime;


    }

    void Shoot(){
        Debug.Log("Shoot");
        Instantiate(bulletPre, firePoint.position, firePoint.rotation);
    }

   private void OnDrawGizmosSelected() {

       Gizmos.color = Color.black;
       Gizmos.DrawWireSphere(transform.position, range);
   }
}
