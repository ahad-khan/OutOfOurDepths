using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void OnCollisionEnter(Collider col)
    {
        if (col.tag == "Enemy") //Loops while player is in trigger tagged "Underwater"
        {
            health -= Time.deltaTime * underwaterDamage;
        }
    }
 
    void Update()
    {
        if (oxygen.oxygenCounter <= 0 && health > 0)
        {
            health -= Time.deltaTime * underwaterDamage;
        }
        slider.value = health;
    }
}
