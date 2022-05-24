using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private float timer;
    [SerializeField]
    private float pingDuration;
    [SerializeField]
    private Color color;

    [SerializeField]
    private float radarDistance = 42f;
    [SerializeField]
    private Transform radar;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pingDuration = 2f;
        timer = 0f;
        transform.Rotate(90, 0, 0);
        radar = GameObject.FindWithTag ("Player").transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        color.a = Mathf.Lerp(pingDuration, 0f, timer / pingDuration);
        spriteRenderer.color = color;
        if (Vector3.Distance(transform.position, radar.position) > radarDistance || timer >= pingDuration) Destroy(gameObject);
    }

}
