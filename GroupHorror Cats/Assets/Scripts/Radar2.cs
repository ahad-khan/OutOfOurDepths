using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar2 : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float radarDistance = 50f;

    [SerializeField]
    private Transform ping;
    [SerializeField]
    private Transform mast;
    private List<Collider> collidedList;

    //private LayerMask layerMask;


    void Start()
    {
        collidedList = new List<Collider>();
    }
    RaycastHit hit;
    //Collider lastHit = null;

    void Update()
    {
        float lastRotation = (mast.eulerAngles.y % 360) - 180;
        mast.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        //transform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        float rotation = (transform.eulerAngles.y % 360) - 180;


        if(lastRotation <0 && rotation >= 0) collidedList.Clear();
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(Mathf.Cos(mast.eulerAngles.z * (Mathf.PI/180f)), Mathf.Sin(mast.eulerAngles.z * (Mathf.PI/180f))), radarDistance);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * radarDistance, Color.yellow);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, radarDistance))
        {
            if (hit.collider.tag == "Enemy" && !collidedList.Contains(hit.collider))
            {
                collidedList.Add(hit.collider);
                print("hit");
                Instantiate(ping, hit.point, Quaternion.identity);
            }

        }
    }
}
