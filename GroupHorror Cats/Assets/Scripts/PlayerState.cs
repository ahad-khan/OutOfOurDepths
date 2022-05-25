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

    public GameObject Player;
    Vector3 Lastpos;

    [SerializeField]
    private int maxHealth = 100;

    HarpoonGun harpoonGun;
    public GameObject gun;

    public AudioSource source;
    public AudioClip pickup;

    void Start()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        harpoonGun = gun.GetComponent<HarpoonGun>();
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.name+" AND "+col.collider.tag);
        if (col.collider.tag == "Enemy") //Loops while player is in trigger tagged "Underwater"
        {
            health -= col.gameObject.GetComponent<EnemyBehaviour>().attackDamage;
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
        if (other.tag == "Harpoon")
        {
            source.clip = pickup;
            source.Play();
            harpoonGun.harpoonCounter++;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (oxygen.oxygenCounter <= 0 && health > 0)
        {
            health -= Time.deltaTime * underwaterDamage;
        }
        slider.value = health;

        if(health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("DeathScene");
        }
    }
}
