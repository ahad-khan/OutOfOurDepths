using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSXExample_CameraMovement : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(transform.right.x, 0, transform.right.z) * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    }
}
