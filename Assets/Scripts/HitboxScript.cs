using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class HitboxScript : NetworkBehaviour
{
    [SerializeField] GameObject PlayerObject;
    private void OnTriggerEnter(Collider collider)
    {
        if(!hasAuthority){ return;}
        
        if (collider.gameObject.CompareTag("Weapon"))
        {
            PlayerObject.GetComponent<Health>().TakeHit(gameObject.tag);
        }
    }
}
