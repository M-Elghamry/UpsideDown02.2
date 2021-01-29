using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float rotationSpeed = 1000f;
    public float flySpeed = 5f;

    public List<Transform> movePath = new List<Transform>();
    public GameObject player;

    public int prefabIndex;

    private int nextPoint = 0;

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
        transform.Rotate(new Vector3(0,0, rotationSpeed) * Time.deltaTime);

        //movement
        Vector2 dir = (Vector2)movePath[nextPoint].position - (Vector2)transform.position;
        dir.Normalize();
        transform.position = (Vector2)transform.position + dir * flySpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, movePath[nextPoint].position) <= 0.5f)
        {
            if (nextPoint == movePath.Count - 1)
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
            else
                nextPoint++;
        }
    }

    public void ReturnToPlayer()
    {
        nextPoint = movePath.Count - 1;
    }

}
