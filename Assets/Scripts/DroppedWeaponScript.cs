using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private void Start()
    {
        GameObject newPrefab = Instantiate(prefab, transform, true);
        newPrefab.transform.localPosition = new Vector3(0, 0, 0);
        newPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
