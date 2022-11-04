using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private double healthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; }
    }
    [SerializeField] private double armorPoint
    {
        get { return armorPoint; }
        set { armorPoint = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void takeDamage()
    {
        if (armorPoint > 0)
        {

        }
    }

}
