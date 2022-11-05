using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerObject;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            PlayerObject.GetComponent<Health>().TakeHit(gameObject.tag,collider.GetComponent<WeaponScript>().GetDamageModifier());
        }
    }
}
