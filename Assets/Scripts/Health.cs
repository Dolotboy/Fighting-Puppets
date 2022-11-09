using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    [SerializeField] private double healthPoint;
    public double HealthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; }
    }

    [SerializeField] private double armorPoint;

    public double ArmorPoint
    {
        get { return armorPoint;  }
        set { armorPoint = value; }
    }

    [SerializeField] private Text hpTxt;
    [SerializeField] private Text armorTxt;

    [Header("UI Elements")]
    [SerializeField] private GameObject Head;
    [SerializeField] private GameObject Torso;
    [SerializeField] private GameObject Hips;
    [SerializeField] private GameObject RightArm;
    [SerializeField] private GameObject RightForeArm;
    [SerializeField] private GameObject LeftArm;
    [SerializeField] private GameObject LeftForeArm;
    [SerializeField] private GameObject RightLeg;
    [SerializeField] private GameObject LeftLeg;

    private void Start()
    {
        UpdateHpTxt();
        UpdateArmorTxt();
    }

    void takeDamage(double damage)
    {
        if (healthPoint <= 0)
        {
            Debug.Log("Dead");
        }
        
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

    public void IncreaseArmorPoint(double armorPoint)
    {
        this.armorPoint += armorPoint;
        UpdateArmorTxt();
    }

    void decreaseArmorPoint(double damage)
    {
        armorPoint -= damage;
        UpdateArmorTxt();
        Debug.Log($"Took {damage} Damage to Armor");
    }

    void decreaseHealthPoint(double damage)
    {
        healthPoint -= damage;
        UpdateHpTxt();
        Debug.Log($"Took {damage} Damage to Health");
    }

    void UpdateHpTxt()
    {
        hpTxt.text = Convert.ToString(healthPoint);
    }

    void UpdateArmorTxt()
    {
        armorTxt.text = Convert.ToString(armorPoint);
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
                Head.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_Torso":
            {
                Debug.Log("Hit in Torso");
                takeDamage(7 * damageModifier);                
                Torso.GetComponent<Image>().color = new Color32(255,0,0,100);

            } break;
            case "Hitbox_Hips":
            {
                Debug.Log("Hit in Hips");
                takeDamage(4 * damageModifier);                
                Hips.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_LeftLeg":
            {
                Debug.Log("Hit in Left Leg");
                takeDamage(2 * damageModifier);                
                LeftLeg.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_RightLeg":
            {
                Debug.Log("Hit in Right Leg");
                takeDamage(2 * damageModifier);                
                RightLeg.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_LeftArm":
            {
                Debug.Log("Hit in Left Arm");
                takeDamage(1 * damageModifier);                
                LeftArm.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_RightArm":
            {
                Debug.Log("Hit in Right Arm");
                takeDamage(1 * damageModifier);                
                RightArm.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_LeftForeArm":
            {
                Debug.Log("Hit in Left Forearm");
                takeDamage(1 * damageModifier);                
                LeftForeArm.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
            case "Hitbox_RightForeArm":
            {
                Debug.Log("Hit in Right Forearm");
                takeDamage(1 * damageModifier);                
                RightForeArm.GetComponent<Image>().color = new Color32(255,0,0,100);
            } break;
        }
        
        Debug.Log($"{healthPoint}");
    }

}
