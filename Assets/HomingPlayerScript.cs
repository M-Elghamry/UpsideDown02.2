using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPlayerScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float moveSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        Vector2 movingVector = transform.up;
        moveSpeed = GetComponent<RotateToMouse>().distance.magnitude;
        moveSpeed = Mathf.Min(moveSpeed, 10f);
        moveSpeed = 0.01f + moveSpeed / 1.5f;
        myRigidbody.velocity += (movingVector * moveSpeed);
    }
}

