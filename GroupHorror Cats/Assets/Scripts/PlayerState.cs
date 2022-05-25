using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private Oxygen oxygen;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float underwaterDamage = 5f;

    public float health = 100f;

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private HarpoonGun harpoonGun;

    [SerializeField]
    private Texture[] crackTextures = new Texture[3];
    
    [SerializeField]
    private RawImage cracks;
    private bool isGrabbed = false;
    [SerializeField]
    private GameObject grabbedEnemy;

    public AudioSource source;
    public AudioClip pickup;

    public GameObject Player;
    Vector3 Lastpos;

    void Start()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.name+" AND "+col.collider.tag);
        if (col.collider.tag == "Enemy") //Loops while player is in trigger tagged "Underwater"
        {
            health -= col.gameObject.GetComponent<EnemyBehaviour>().attackDamage;
            if(!isGrabbed && col.gameObject.GetComponent<EnemyBehaviour>().IsSmall)
            {
                isGrabbed = true;
                Destroy(col.gameObject);
                grabbedEnemy.SetActive(true);
            }
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Ladder")
        {
            Debug.Log("HIT HIT HIT");
            Lastpos = Player.transform.position;
            if (Player.GetComponent<PlayerMovement>().move != new Vector3(0, 0, 0))
            {
                Debug.Log("LADDER LADDER LADDER");
                //Player.GetComponent<CharacterController>().Move(new Vector3(0, 50, 0) * Time.deltaTime);
                Player.GetComponent<PlayerMovement>().velocity.y = 4f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WinArea")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("WinScene");
        }
        if(other.tag == "Harpoon")
        {
            source.clip = pickup;
            source.Play();
            harpoonGun.harpoonCounter++;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if(isGrabbed)
        {

        }

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("DeathScene");
        }

        health = Mathf.Clamp(health, 0, maxHealth);
        if (oxygen.oxygenCounter <= 0 && health > 0)
        {
            health -= Time.deltaTime * underwaterDamage;
        }
        slider.value = health;

        if(health >= 75)cracks.texture = null;
        if(health < 75 && health >= 50)cracks.texture = crackTextures[0];
        if(health < 50 && health >= 25)cracks.texture = crackTextures[1];
        if(health < 25)cracks.texture = crackTextures[2];


        
    }
}
