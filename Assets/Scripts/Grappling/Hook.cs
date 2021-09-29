using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    [SerializeField] float hookForce =25f;
    Grapple grapple;
    Rigidbody rigidBody;
    LineRenderer lineRenderer;
   
   public void Initialize(Grapple grapple, Transform ShootTransform){

       transform.forward = ShootTransform.forward;
       this.grapple = grapple;
       rigidBody = GetComponent<Rigidbody>();
       lineRenderer = GetComponent<LineRenderer>();
       rigidBody.AddForce(transform.forward * hookForce, ForceMode.Impulse);




   }//Initialize



    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[]{
            transform.position,
            grapple.transform.position
        };


        lineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other) {
        if((LayerMask.GetMask("Grapple") & 1 << other.gameObject.layer) > 0){
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;

            grapple.StartPull();
        }
    }
}
