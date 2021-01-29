using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            //take damage
            return;
        }

        Debug.Log("Hit");

        if (collider.GetComponent<Spiral>() != null)
        {
            collider.GetComponent<Spiral>().ReturnToPlayer();
        }
        if (collider.GetComponent<CircleBoomerang>() != null)
        {
            collider.GetComponent<CircleBoomerang>().ReturnToPlayer();
        }

        if (collider.GetComponent<Boomerang>() != null)
        {
            collider.GetComponent<Boomerang>().ReturnToPlayer();
        }

    }
}
