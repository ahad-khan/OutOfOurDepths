using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HarpoonGun : MonoBehaviour
{
    public int harpoonCounter = 10;
    [SerializeField]
    private Transform shootStart;
    [SerializeField]
    private Harpoon harpoon;
    private bool canShoot = true;

    public AudioClip ShootSound;
    public AudioSource audioSource;

    float timer = 2f;

    //public AudioClip InEnemy;
    void Start()
    {
        
    }

    void Update()
    {
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
        if (!Physics.Linecast(transform.parent.position, shootStart.position))
        {
            if (canShoot && harpoonCounter > 0 && Input.GetMouseButtonDown(0) && timer < 0)
            {
                audioSource.clip = ShootSound;
                audioSource.Play();
                Instantiate(harpoon, shootStart.position, shootStart.rotation);
                harpoonCounter--;
                timer = 2f;
            }
        }
          
    }
}
