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

    void takeDamage(double damage)
    {
        if (armorPoint > 0)
        {
            decreaseArmorPoint(damage);
            if(armorPoint < 0)
            {
                healthPoint += armorPoint;
                armorPoint = 0;
            }
        }
        else
        {
            decreaseHealthPoint(damage);
        }
    }

    void decreaseArmorPoint(double damage)
    {
        armorPoint -= damage;
    }

    void decreaseHealthPoint(double damage)
    {
        healthPoint -= damage;
    }

    void die()
    {
        Destroy(gameObject);
    }

    public void TakeHit(string tag)
    {
        switch (tag)
        {
            case "Hitbox_Head":
            {
                Debug.Log("Hit in Head");
            } break;
            case "Hitbox_Torso":
            {
                Debug.Log("Hit in Torso");
            } break;
            case "Hitbox_Hips":
            {
                Debug.Log("Hit in Hips");
            } break;
            case "Hitbox_LeftLeg":
            {
                Debug.Log("Hit in Left Leg");
            } break;
            case "Hitbox_RightLeg":
            {
                Debug.Log("Hit in RightLeg");
            } break;
            case "Hitbox_LeftArm":
            {
                Debug.Log("Hit in Left Arm");
            } break;
            case "Hitbox_RightArm":
            {
                Debug.Log("Hit in Right Arm");
            } break;
            case "Hitbox_LeftForeArm":
            {
                Debug.Log("Hit in Left Forearm");
            } break;
            case "Hitbox_RightForeArm":
            {
                Debug.Log("Hit in Right Forearm");
            } break;
        }
    }

}
