using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    private Transform target;
    public float range = 45f;

    public string PlayerTag = "Player";

     //part of the turret for rotation
    public Transform RotatingPart;

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

    }

    
   private void OnDrawGizmosSelected() {

       Gizmos.color = Color.black;
       Gizmos.DrawWireSphere(transform.position, range);
   }
}
