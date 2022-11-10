using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsSpawner : NetworkBehaviour
{

    [SerializeField] private bool isRandom;

    [SerializeField] private bool isRespawningGoods;

    [SerializeField] private float goodsRespawnTime;

    public bool haveObjectInIt = false;

    [SerializeField] private List<GameObject> goodsObjects;

    [SerializeField] private int goodsNbrInList;

    [SerializeField] private GameObject goodsObject;
    
    
    void Start()
    {
        haveObjectInIt = false;
        if(isRandom)
        {
            if(isRespawningGoods)
            {
                InvokeRepeating("LoadRandomGoods", goodsRespawnTime, goodsRespawnTime);
            }
            else
            {
                LoadRandomGoods();
            }
        }
        else
        {
            if (isRespawningGoods)
            {
                InvokeRepeating("LoadGoods", goodsRespawnTime, goodsRespawnTime);
            }
            else
            {
                LoadGoods();
            }
        }
    }

    [ClientRpc]
    public void TakeObjectInIt()
    {
        haveObjectInIt = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

    [ClientRpc]
    void LoadGoods()
    {
        if (!haveObjectInIt)
        {
            goodsObject = goodsObjects[goodsNbrInList];

            GameObject newPrefab = Instantiate(goodsObject, transform, true);
            newPrefab.transform.localPosition = new Vector3(0, 0, 0);

            haveObjectInIt = true;
        }
    }

    [ClientRpc]
    void LoadRandomGoods()
    {
        if (!haveObjectInIt)
        {
            var rnd = new System.Random();

            if (goodsObjects.Count >= 1)
            {
                goodsObject = goodsObjects[rnd.Next(0, goodsObjects.Count)];
            }

            GameObject newPrefab = Instantiate(goodsObject, transform, true);
            newPrefab.transform.localPosition = new Vector3(0, 0, 0);

            haveObjectInIt = true;
        }
    }
}
