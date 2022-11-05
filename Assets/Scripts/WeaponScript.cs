using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private double damageModifier;

    public double GetDamageModifier()
    {
        return damageModifier;
    }
}
