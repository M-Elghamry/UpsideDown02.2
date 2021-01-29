using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Vector2 moveVelocity;
    public Rigidbody2D rb;

    public float slideSpeed;
    private float initialSlideSpeed;

    public float slideSpeedCurve;

    public Vector3 slideDir;

    private State state;
    public enum State
    {
        Normal,
        DashMoving,
    }
    void Start()
    {
        state = State.Normal;
        initialSlideSpeed = slideSpeed;
    }

    void Update()
    {


        moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                HandleDash();
                break;
            case State.DashMoving:
                HandleDashMove();
                break;
        }
    }



    public void HandleMovement()
    {
        rb.MovePosition(transform.position + new Vector3(moveVelocity.x * speed * Time.deltaTime, moveVelocity.y * speed * Time.fixedDeltaTime));
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    public void HandleDash()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Dash");
            state = State.DashMoving;
            slideDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            initialSlideSpeed += .001f;
            slideSpeed = initialSlideSpeed;
        }
    }
    private void HandleDashMove()
    {
        transform.position += slideDir * slideSpeed * Time.deltaTime;

        slideSpeed -= slideSpeed * slideSpeedCurve * Time.deltaTime;
        if (slideSpeed < 5f)
        {
            state = State.Normal;
        }
    }
}
