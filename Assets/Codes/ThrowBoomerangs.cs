using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBoomerangs : MonoBehaviour
{
    public List<GameObject> boomerangPrefabs = new List<GameObject>();

    public List<GameObject> onHand = new List<GameObject>();

    public GameObject timeManager;

    public GameObject UIList;

    void Start()
    {
        UIList = GameObject.Find("ListUI");
        timeManager = GameObject.FindGameObjectWithTag("TimeManager");
        for (int i = 0; i < boomerangPrefabs.Count; i++)
            Return(i);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }

    void Throw()
    {
        if (onHand.Count == 0)
            return;

        //Frame Lag
        timeManager.GetComponent<TimeStopperScript>().MiniHitStop();

        GameObject prefab = onHand[0];
        onHand.RemoveAt(0);
        GameObject projectile = Instantiate(prefab, transform.position, transform.rotation);
        //projectile.GetComponentInChildren<Boomerang>().prefabIndex = boomerangPrefabs.IndexOf(prefab);
        
        Boomerang[] array = projectile.GetComponentsInChildren<Boomerang>();
        for(int i = 0; i < array.Length; i++)
        {
            array[i].prefabIndex = boomerangPrefabs.IndexOf(prefab);
        }
        GameObject.Destroy(UIList.transform.GetChild(0).gameObject);

    }

    public void Return(int prefabIndex)
    {
        if (onHand.Contains(boomerangPrefabs[prefabIndex]))
            return;
        onHand.Add(boomerangPrefabs[prefabIndex]);
        Image im = new GameObject().AddComponent<Image>();
        im.sprite = boomerangPrefabs[prefabIndex].GetComponentInChildren<SpriteRenderer>().sprite;
        im.rectTransform.sizeDelta = new Vector2(50,50);
        im.name = boomerangPrefabs[prefabIndex].name;
        im.transform.SetParent(UIList.transform);
    }
}
