using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsSpawner : MonoBehaviour
{

    [SerializeField] private bool isRandom;

    [SerializeField] private bool isRespawningGoods;

    public bool haveObjectInIt = true;

    [SerializeField] private List<GameObject> goodsObjects;

    [SerializeField] private int goodsNbrInList;

    [SerializeField] private GameObject goodsObject;
    
    
    void Start()
    {
        if(isRandom)
        {
            LoadRandomGoods();
        }
        else
        {
            LoadGoods();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    public void TakeObjectInIt()
    {
        haveObjectInIt = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

    void LoadGoods()
    {
        goodsObject = goodsObjects[goodsNbrInList];

        GameObject newPrefab = Instantiate(goodsObject, transform, true);
        //newPrefab.AddComponent<NetworkIdentity>();
        newPrefab.transform.localPosition = new Vector3(0, 0, 0);
        //newPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void LoadRandomGoods()
    {
        var rnd = new System.Random();

        if (goodsObjects.Count >= 1)
        {
            goodsObject = goodsObjects[rnd.Next(0, goodsObjects.Count)];
        }

        GameObject newPrefab = Instantiate(goodsObject, transform, true);
        //newPrefab.AddComponent<NetworkIdentity>();
        newPrefab.transform.localPosition = new Vector3(0, 0, 0);
    }
}
