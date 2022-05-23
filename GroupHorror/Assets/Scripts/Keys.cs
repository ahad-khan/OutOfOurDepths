using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    //public int KeyNum = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().PickUpKey();
            Destroy(gameObject);
        }
    }
}