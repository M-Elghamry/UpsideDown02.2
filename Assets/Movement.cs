using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Movement2d Movement2d;

    void Start()
    {
       
    }

    void Update()
    {
        Movement2d.MoveHorizontally(Input.GetAxis("Horizontal"));
        if(Input.GetButton("Jump"))
        {
            Movement2d.Jump();
        }
        if (Input.GetButton("Crouch"))
        {
            Movement2d.Crouch();
        }
    }

}
