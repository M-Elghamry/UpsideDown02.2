using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementrandomizer : MonoBehaviour
{
    void Update()
    {
        if (Random.Range(-1, 1) <= 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Random.Range(.015f, .005f) * ((GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position).normalized + new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0)), ForceMode2D.Impulse);
        }
    }
}
