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
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WinArea")
        {
            SceneManager.LoadScene("WinScene");
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
