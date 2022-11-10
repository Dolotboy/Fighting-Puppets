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
            Debug.Log("Test");
            PlayerObject.GetComponent<Health>().TakeHit(gameObject.tag,collider.GetComponent<WeaponScript>().GetDamageModifier());
            collider.GetComponent<WeaponScript>().GetDamageDone(gameObject.tag);
        }
    }
}
