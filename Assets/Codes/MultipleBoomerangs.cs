using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBoomerangs : MonoBehaviour
{
    public int projectileAmount;
    public int returned = 0;

    public GameObject player;
    public int prefabIndex;

    void Update()
    {
        if (returned >= projectileAmount)
        {
            player.GetComponent<ThrowBoomerangs>().Return(prefabIndex);
            GameObject.Destroy(transform.gameObject);
        }
    }
}
