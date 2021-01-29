using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spiral : Boomerang
{

    public float spiralSpeed = 1f;

    public Transform pivot;

    Transform parent;

    private bool returning = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        parent = transform.parent;
        //movePath.Add(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //spinning
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        if (Vector2.Distance(transform.position, pivot.transform.position) > 4f)
            returning = true;

        if (returning)
        {
            Vector2 dir = (Vector2)player.transform.position - (Vector2)transform.position;
            dir.Normalize();
            transform.position = (Vector2)transform.position + dir * flySpeed * Time.deltaTime;
            
            if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
            {
                player.GetComponent<ThrowBoomerangs>().Return(prefabIndex);
                GameObject.Destroy(transform.parent.gameObject);
            }
        }
        else {
            parent.transform.Rotate(0,0,spiralSpeed * Time.deltaTime);
            transform.position += parent.transform.up * 1f * Time.deltaTime;
        }
    }

    public new void ReturnToPlayer()
    {
        returning = true;
    }
}
