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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pingDuration = 2f;
        timer = 0f;
        transform.Rotate(90, 0, 0);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        color.a = Mathf.Lerp(pingDuration, 0f, timer / pingDuration);
        spriteRenderer.color = color;
        if (timer >= pingDuration) Destroy(gameObject);
    }

}
