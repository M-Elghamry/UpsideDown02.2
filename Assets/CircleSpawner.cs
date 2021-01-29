using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public float spawnTimer;
    void Start()
    {

    }

    void FixedUpdate()
    {
        spawnTimer -= .1f * Time.deltaTime;

        if (spawnTimer <= 0)
        {

        }
    }
}
