using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerCircle : MonoBehaviour
{
    public GameObject bullet;
    public float time;
    private float timer;
    void Start()
    {
        timer = time;
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1));
        if (timer <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timer = time;
        }
        timer -= .01f;
    }
}
