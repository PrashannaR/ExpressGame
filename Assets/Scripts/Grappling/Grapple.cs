using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    [SerializeField] float pullSpeed = 0.5f;
    [SerializeField] float stopDistance = 4f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;

    Hook hook;
    bool pulling;
    Rigidbody rigidBody;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        pulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        StopAllCoroutines();
        if(hook == null && Input.GetMouseButton(0)){
            pulling = false;
            hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Initialize(this, shootTransform);
            StartCoroutine(DestroyAfter());
        }else if(hook != null && Input.GetMouseButtonDown(1)){
            DestroyHook();
        }

        if(! pulling || hook == null) return;

        if(Vector3.Distance(transform.position, hook.transform.position) <= stopDistance){
            DestroyHook();
        }else{
            rigidBody.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
        }
    }



    public void StartPull(){
        pulling = true;

    }

    void DestroyHook(){
        if(hook == null) return;
        pulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }

    private IEnumerator DestroyAfter(){
        yield return new WaitForSeconds(8f);
        DestroyHook();
    }
         
}
