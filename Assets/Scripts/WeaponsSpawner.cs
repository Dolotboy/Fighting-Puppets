using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSpawner : MonoBehaviour
{
    [SerializeField] private bool isRandom;

    [SerializeField] private List<GameObject> weaponsObjects;

    [SerializeField] private int weaponsNbrInList;

    [SerializeField] private GameObject weaponsObject;


    void Start()
    {
        if (isRandom)
        {
            LoadRandomWeapons();
        }
        else
        {
            LoadWeapons();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadWeapons()
    {
        weaponsObject = weaponsObjects[weaponsNbrInList];

        GameObject newPrefab = Instantiate(weaponsObject, transform, true);
        newPrefab.transform.localPosition = new Vector3(0, 0, 0);
        //newPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void LoadRandomWeapons()
    {
        var rnd = new System.Random();

        if (weaponsObjects.Count >= 1)
        {
            weaponsObject = weaponsObjects[rnd.Next(0, weaponsObjects.Count)];
        }

        GameObject newPrefab = Instantiate(weaponsObject, transform, true);
        newPrefab.transform.localPosition = new Vector3(0, 0, 0);
    }
}
