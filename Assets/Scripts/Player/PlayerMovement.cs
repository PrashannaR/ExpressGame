using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public float speed = 3f;

    //gravity
    Vector3 velocity;
    public float gravity = -9.81f;
    
    //check the ground 
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    bool isGrounded;

    //jump
    public float jumpHeight = 300f;
    public float Verticalvelocity;

    //animation
    public string Walking = "Walking";
    Animator animator;

    //camera 
    private Camera playerCamera;

    //hook shot
    private State state;
    [SerializeField] private Transform debugHitPoint;
    private Vector3 hookShotPosition;
    
    private enum State{
        Normal,
        HookShotFly
    }





    private void Awake() {
        playerCamera = transform.Find("Camera").GetComponent<Camera>();

        //hook
        state = State.Normal;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

    }//start



    // Update is called once per frame
    void Update()
    {
        switch(state){
            default:
            case State.Normal:
        
        //check ground
         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        //character movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        if(move != Vector3.zero){
             
             controller.Move(move * speed * Time.deltaTime);
             walk();
           
        }else{

            idle();
        }

        //hook
        HandleHookShotStart();
        break;


        //hook shot player fly
        case State.HookShotFly:
        HandleHookShotMovement();
        break;


        }//switch

    }//update
    
    //gravity and character jump
    private void FixedUpdate() {
        JumpPlayer();
        
    }//fixedUpdate 

    void JumpPlayer(){
         //jump https://www.youtube.com/watch?v=miMCu5796KM
         if(controller.isGrounded){

            Verticalvelocity = gravity * Time.deltaTime;
            if(Input.GetButtonDown("Jump")){
               Verticalvelocity = jumpHeight;
            }
        }
            else{
                Verticalvelocity += gravity * Time.deltaTime; 
            }
            //gravity
            velocity = new Vector3(0f, Verticalvelocity, 0f);
            controller.Move(velocity * Time.deltaTime);
    }

    //animation
    public void walk(){
        animator.SetFloat("Speed", 1f);
    }

    public void idle(){
        animator.SetFloat("Speed", 0f);
    }
   

    //hook shot
    private void HandleHookShotStart(){

        if(Input.GetKeyDown(KeyCode.E)){
         if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit)){

             //hit target
             debugHitPoint.position = raycastHit.point;
             hookShotPosition = raycastHit.point;
             state = State.HookShotFly;
         }
        }

    }//handle hook shot start

    private void HandleHookShotMovement(){

        Vector3 hookShotDirection = (hookShotPosition - transform.position).normalized;

        float hookShotSpeed = 5f;
        //move character
        controller.Move(hookShotDirection * hookShotSpeed * Time.deltaTime);

        //reset after reaching
        float reachedHookShotDistance = 1f;
        if(Vector3.Distance(transform.position, hookShotPosition) < reachedHookShotDistance){
           
           // state = State.Normal;
           state = State.HookShotFly;


        }
    }//HandleHookShotMovement








}
