using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    public bool GetFlipped;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetFlipped)
        {
            GetComponent<Transform>().Rotate(new Vector2(0, 180));
            GetFlipped = false;
        }
    }


}
