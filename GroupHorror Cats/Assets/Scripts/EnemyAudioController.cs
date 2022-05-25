using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAudioController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip Idle1;
    public AudioClip Idle2;

    float timer = 5f;
    int WhichClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }

        if(timer < 0)
        {
            WhichClip = Random.Range(1, 3);
            if(WhichClip == 1)
            {
                source.clip = Idle1;
            }
            else if (WhichClip == 2)
            {
                source.clip = Idle2;
            }
            source.Play();
            timer = 10f;
        }
    }
}
