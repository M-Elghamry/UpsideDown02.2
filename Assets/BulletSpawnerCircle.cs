using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerCircle : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;
    public float time;
    public float timer;
    void Start()
    {
        timer = time;
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1));
        if (timer <= 0)
        {
            if (Random.Range(-1f, 2f) > 0)
            {
                Instantiate(bullet1, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bullet2, transform.position, Quaternion.identity);
            }

            timer = time;
        }
        timer -= .01f;
        time -= .0001f;
    }
}
