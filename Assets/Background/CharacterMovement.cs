using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//for shoot mechanism : https://www.youtube.com/watch?v=mkErt53EEFY


//https://www.youtube.com/watch?v=59No0ybIoxg
public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float movementSpeed = 69f;
    public float gravity = 9.81f;
    public float jumpSpeed = 2f;
    public float storeJump;
 

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 moveChar = new Vector3(horizontal, 0, vertical);

        if(characterController.isGrounded){
            
            if(Input.GetButtonDown("Jump")){
            storeJump = jumpSpeed;
            }
        }


        storeJump -= gravity * Time.deltaTime;

        moveChar.y = storeJump;
       
       characterController.Move(moveChar * movementSpeed * Time.deltaTime);
    }
}
