using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject BulletPrefab;
    private bool InPlayerRange = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BulletDelaySpawner");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BulletDelaySpawner()
    {
        while(true)
        {
            if (InPlayerRange)
            {

                Instantiate(BulletPrefab, (Vector2)GetComponent<Transform>().position, Quaternion.identity);
                yield return new WaitForSeconds(3);
            }
            else
            {
                yield return null;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            InPlayerRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InPlayerRange = false;
        }
    }
}
