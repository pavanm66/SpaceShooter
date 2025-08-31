using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform player;          // Assign your player here
    public float parallaxFactor = 0.5f;  // Smaller = slower movement (further back)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distanceX = player.position.x * parallaxFactor;
        transform.position = new Vector3(startPos.x + distanceX, startPos.y, startPos.z);
    }
}
