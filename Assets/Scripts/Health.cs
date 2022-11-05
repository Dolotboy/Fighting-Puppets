using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private double healthPoint;

    [SerializeField] private double armorPoint;
    
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
        Debug.Log($"Took {damage} Damage to Armor");
    }

    void decreaseHealthPoint(double damage)
    {
        healthPoint -= damage;
        Debug.Log($"Took {damage} Damage to Health");
    }

    void die()
    {
        Destroy(gameObject);
    }

    public void TakeHit(string tag,double damageModifier)
    {
        if(!hasAuthority) { return;}
        
        switch (tag)
        {
            case "Hitbox_Head":
            {
                Debug.Log("Hit in Head");
                takeDamage(10 * damageModifier);
            } break;
            case "Hitbox_Torso":
            {
                Debug.Log("Hit in Torso");
                takeDamage(7 * damageModifier);
            } break;
            case "Hitbox_Hips":
            {
                Debug.Log("Hit in Hips");
                takeDamage(4 * damageModifier);
            } break;
            case "Hitbox_LeftLeg":
            {
                Debug.Log("Hit in Left Leg");
                takeDamage(2 * damageModifier);
            } break;
            case "Hitbox_RightLeg":
            {
                Debug.Log("Hit in Right Leg");
                takeDamage(2 * damageModifier);
            } break;
            case "Hitbox_LeftArm":
            {
                Debug.Log("Hit in Left Arm");
                takeDamage(1 * damageModifier);
            } break;
            case "Hitbox_RightArm":
            {
                Debug.Log("Hit in Right Arm");
                takeDamage(1 * damageModifier);
            } break;
            case "Hitbox_LeftForeArm":
            {
                Debug.Log("Hit in Left Forearm");
                takeDamage(1 * damageModifier);
            } break;
            case "Hitbox_RightForeArm":
            {
                Debug.Log("Hit in Right Forearm");
                takeDamage(1 * damageModifier);
            } break;
        }
        
        Debug.Log($"{healthPoint}");
    }

}
