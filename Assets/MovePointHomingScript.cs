using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointHomingScript : MonoBehaviour
{
    public double homingtimer;
    public float thrust;
    private void Start()
    {
        //GetComponent<Rigidbody2D>().velocity = Vector2.up;
    }
    void Update()
    {
        homingtimer -=  1 * Time.deltaTime;

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (homingtimer <= 0)
        {
            ThrustAtMouse();
        }
    }

    public void ThrustAtMouse()
    {



        GetComponent<Rigidbody2D>().AddForce(Vector3.up * thrust);
        homingtimer += 1;
    }
}
