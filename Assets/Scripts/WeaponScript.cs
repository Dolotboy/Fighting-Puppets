using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private double damageModifier;

    public string WeaponType = "1Handed";
    
    public double GetDamageModifier()
    {
        return damageModifier;
    }

    public void GetDamageDone()
    {
        Debug.Log("Damage Done to Enemy");
    }
}
