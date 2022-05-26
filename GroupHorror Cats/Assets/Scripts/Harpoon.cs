using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Harpoon : MonoBehaviour
{
    Rigidbody body;
    private Camera camera;
    [SerializeField]
    private float speed = 100;
    private float depth = 0.3f;
    RaycastHit hit;
    private Vector3 endPoint;
    //Vector3 target;
    Ray ray;

    AudioSource source;
    public AudioClip InEnemy;


    void Start()
    {
        source = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out hit)) endPoint = hit.point;
        body = GetComponent<Rigidbody>();
        body.velocity = (endPoint - transform.position).normalized * 10;
        body.AddForce(transform.forward * -1 * speed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            source.clip = InEnemy;
            if(col.gameObject.GetComponent<EnemyBehaviour>().IsSmall)
            {
                Destroy(col.gameObject);
            }else transform.parent = col.transform;
        }else if (col.gameObject.tag != "Player")
        {
            //col.transform.Translate(depth * -Vector2.right); 
            body.isKinematic = true;
        }
        source.Play();
    }
    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            body.isKinematic = false;
        }
    }
}
