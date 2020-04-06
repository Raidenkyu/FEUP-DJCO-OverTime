using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity = 12f;
    public float jumpHeight = 0.5f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    public CharacterController player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);

        if(isGrounded && velocity.y<0){
            velocity.y = -2f;
        }

        float moveX = (Convert.ToInt32(Input.GetKey(KeyCode.D))-Convert.ToInt32(Input.GetKey(KeyCode.A))) * sensitivity * Time.deltaTime;
        float moveZ = (Convert.ToInt32(Input.GetKey(KeyCode.W))-Convert.ToInt32(Input.GetKey(KeyCode.S))) * sensitivity * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward*moveZ;

        player.Move(move);
        
        if(Input.GetKey(KeyCode.P) && isGrounded){
     
            velocity.y = Mathf.Sqrt(jumpHeight)*-2f*gravity;
        }

        velocity.y+= 1/2*gravity*Time.deltaTime;

        player.Move(velocity*Time.deltaTime);

       

    }
}
