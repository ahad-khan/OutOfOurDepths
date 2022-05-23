using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField]
    private float speed = 15f;
    [SerializeField]
    private float airSpeed = 20f;
    [SerializeField]
    private float underwaterSpeed = 15f;
    [SerializeField]
    private float depthsSpeed = 10f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHight = 3f;
    private KeyCode crouch = KeyCode.C;
    private GameObject hitbox;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;





    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }
        if(Input.GetKeyDown("f") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "depths")
        {
            speed = depthsSpeed;
        }
        if (col.transform.tag == "air")
        {
            speed = airSpeed;
        }
        
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "depths" || col.transform.tag == "air")
        {
            speed = underwaterSpeed;
        }
        
        
    }



}
