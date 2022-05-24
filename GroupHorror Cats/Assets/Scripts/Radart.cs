using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radart : MonoBehaviour
{
    //private Transform sweepTransform;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private float radarDistance = 50f;

    //[SerializeField]
    //private Transform pfRadarPing;
    //[SerializeField]
    private Transform mast;
    private List<Collider> collidedList;
    //[SerializeField]
   // private LayerMask layerMask;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        collidedList = new List<Collider>();
        mast = transform.Find("Sweep");
    }
    

    void Update()
    {
        float lastRotation = (mast.eulerAngles.y % 360) - 180;
        mast.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        //mast.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        float rotation = (transform.eulerAngles.y % 360) - 180;

        if(lastRotation < 0 && rotation >= 0) collidedList.Clear();

        //Physics.Raycast(transform.position, new Vector3(Mathf.Cos(mast.eulerAngles.z * (Mathf.PI/180f)), Mathf.Sin(mast.eulerAngles.z * (Mathf.PI/180f))));

        //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * radarDistance, out hit)          transform.TransformDirection(Vector3.down)

        //Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(mast.eulerAngles.z * (Mathf.PI/180f)), Mathf.Sin(mast.eulerAngles.z * (Mathf.PI/180f))) * radarDistance, Color.yellow);
        //Debug.DrawRay(mast.transform.position, transform.TransformDirection(Vector3.up) * radarDistance, Color.yellow);


        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(Mathf.Cos(mast.eulerAngles.z * (Mathf.PI/180f)), Mathf.Sin(mast.eulerAngles.z * (Mathf.PI/180f))), radarDistance);
        if (Physics.Raycast(mast.transform.position, transform.TransformDirection(Vector3.up) * radarDistance, out hit))
        {
          if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                print("hit");
                if (!collidedList.Contains(hit.collider))
                {
                    collidedList.Add(hit.collider);
                    
                    //Instantiate(pfRadarPing, hit.point, Quaternion.identity);
                }

            }  
        }
        
    }
}
