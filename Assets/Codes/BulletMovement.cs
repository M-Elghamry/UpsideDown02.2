using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BulletMovement : MonoBehaviour
{
    public float Force;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Force * Time.deltaTime * (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - gameObject.GetComponent<Transform>().position).normalized, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player" && collision.collider.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "EnemyBoundaries")
        {
            Destroy(gameObject);
        }
    }
}
