using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleBoomerang : Boomerang
{

    public Transform movePoint;

    public Transform pivot;

    private bool spinning = false;
    private bool returning = false;

    private bool var = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movePath.Add(player.transform);

        if (transform.parent.GetComponent<MultipleBoomerangs>())
        {
            transform.parent.GetComponent<MultipleBoomerangs>().player = player;
            transform.parent.GetComponent<MultipleBoomerangs>().prefabIndex = prefabIndex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //spinning
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        //movement
        if (spinning)
        {
            transform.parent.Rotate(0, 0, 100f * Time.deltaTime);
            if (transform.parent.rotation.z > .2f)
            {
                var = true;
            }
            if (var && transform.parent.rotation.z <= 0f && transform.parent.rotation.z >= -10f)
            {
                spinning = false;
                returning = true;
            }
        }
        else if (returning)
        {
            Vector2 dir = (Vector2)player.transform.position - (Vector2)transform.position;
            dir.Normalize();
            transform.position = (Vector2)transform.position + dir * flySpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
            {

                if (transform.parent.GetComponent<MultipleBoomerangs>())
                {
                    transform.parent.GetComponent<MultipleBoomerangs>().returned++;
                    GameObject.Destroy(transform.gameObject);
                }
                else
                {
                    player.GetComponent<ThrowBoomerangs>().Return(prefabIndex);
                    GameObject.Destroy(transform.parent.gameObject);
                }


            }
        }
        else
        {

            Vector2 dir1 = (Vector2)movePoint.position - (Vector2)transform.position;
            dir1.Normalize();
            transform.position = (Vector2)transform.position + dir1 * flySpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, movePoint.position) <= 0.5f)
            {
                spinning = true;
            }
        }
    }

    public new void ReturnToPlayer()
    {
        spinning = false;
        returning = true;
    }
}
