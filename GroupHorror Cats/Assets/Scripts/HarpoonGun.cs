using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    public int harpoonCounter = 10;
    [SerializeField]
    private Transform shootStart;
    [SerializeField]
    private Harpoon harpoon;
    private bool canShoot = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (!Physics.Linecast(transform.parent.position, shootStart.position))
        {
            if (canShoot && harpoonCounter > 0 && Input.GetMouseButtonDown(0))
            {
                Instantiate(harpoon, shootStart.position, shootStart.rotation);
                harpoonCounter--;
            }
        }
          
    }
}
