using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Vector3 velocity;
    bool isGrounded;


    ////////////////////////
    Collider[] HitObjects;
    public Transform Punch;
    public float PunchRange = 1.0f;
    public LayerMask Enemies;
    public LayerMask AirObject;
    public GameObject harpoonGun;

    public GameObject FlashLight;
    Light light;

    float timer = 1;
    int FlickerNum = 2;
    int count = 0;
    float FlickerTimer = 0.1f;
    bool LightState = true;

    float InteractTimer = 3.0f;
    bool interacting = false;
    public bool harpoonActive = false;

    public Vector3 move;
    public GameObject DOOR;

    public bool IsConsole = false;
    public Slider InteractSlider;
    ////////////////////////

    private void Start()
    {
        light = FlashLight.GetComponent<Light>();
        InteractSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }
        if(Input.GetKeyDown("f") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }

        ///////////////////////////////////////////////////
        if (!harpoonActive && Input.GetMouseButtonDown(0))
        {
            HitObjects = Physics.OverlapSphere(Punch.position, PunchRange, Enemies);
            foreach (var hit in HitObjects)
            {
                hit.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }
        if (Input.GetMouseButton(1))
        {


            HitObjects = Physics.OverlapSphere(Punch.position, PunchRange, AirObject);
            foreach (var hit in HitObjects)
            {
                if (hit.gameObject.tag == "Console") { interacting = true; print("wooo"); IsConsole = true; break;}
                if (hit.gameObject.layer == LayerMask.NameToLayer("AirRefill")) { interacting = true; print("waaa"); break; }
                
            }


            if (interacting == true)
            {
                InteractSlider.gameObject.SetActive(true);
                if (InteractTimer > 0)
                {
                    InteractTimer = InteractTimer - Time.deltaTime;
                    InteractSlider.value = InteractTimer;
                }
                if (InteractTimer <= 0)
                {
                    if (IsConsole == false)
                    {
                        GetComponent<Oxygen>().ChangeOxygenValue(100);
                    }
                    else if (GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().KeyCount == 4)
                    {
                        print("yay");
                        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().KeyCount = 0;
                        DOOR.SetActive(false);
                    }

                    InteractSlider.gameObject.SetActive(false);
                    interacting = false;
                    IsConsole = false;
                }

            }
            else
            {
                InteractTimer = 3;
                InteractSlider.gameObject.SetActive(false);
                interacting = false;
                IsConsole = false;
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            InteractTimer = 3;
            InteractSlider.gameObject.SetActive(false);
            interacting = false;
            IsConsole = false;
        }
        if (Input.GetKeyDown("e"))
        {
            if (light.enabled == true)
            {
                light.enabled = false;
                LightState = false;
            }
            else
            {
                light.enabled = true;
                LightState = true;
            }
        }

        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }

        if (LightState == true && timer < 0)
        {
            if (FlickerTimer > 0)
            {
                FlickerTimer = FlickerTimer - Time.deltaTime;
            }
            if (count < FlickerNum && FlickerTimer < 0)
            {
                count++;
                light.intensity = 0.5f;
                if (light.enabled == true)
                {
                    light.enabled = false;
                }
                else
                {
                    light.enabled = true;
                }
                FlickerTimer = 0.1f;
            }
            if (count == FlickerNum && FlickerTimer < 0)
            {
                if (LightState == true && light.enabled == false)
                {
                    light.enabled = true;
                }
                light.intensity = 2;
                count = 0;
                FlickerNum = Random.Range(5, 10);
                timer = Random.Range(5f, 20f);
            }
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f || Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if(harpoonActive)
            {
                harpoonActive = false;
                harpoonGun.SetActive(false);
            }else
            {
                harpoonActive = true;
                harpoonGun.SetActive(true);
            }
        }
 {
     //wheel goes up
 }


        ///////////////////////////////////////////////////

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
