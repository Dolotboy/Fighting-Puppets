using System;
using System.Collections;
using System.Collections.Generic;
using BrettArnett;
using UnityEngine;

public class RifleScript : MonoBehaviour
{
   [SerializeField] private GameObject Canon;
   [SerializeField] private GameObject Projectile;
   [SerializeField] private float speed;
   
   public void Fire()
   {
      if (!gameObject.GetComponentInParent(typeof(GamePlayer))) { return; }
      
      RaycastHit hit;
      var camTrasform =  gameObject.GetComponentInParent(typeof(PlayerMovementController)).GetComponent<PlayerMovementController>().playerCam.transform;
      Ray ray = new Ray(camTrasform.position, camTrasform.forward);
      
      Debug.DrawRay(camTrasform.position,camTrasform.forward);
      
      if (Physics.Raycast(ray, out hit))
      {
         if (hit.collider.GetComponent<HitboxScript>())
         {
            hit.collider.GetComponent<HitboxScript>().GetHitFromRay(transform.GetComponentInChildren<WeaponScript>().GetDamageModifier());
         }
      }
      //GameObject instanciatedProjectile = Instantiate(Projectile, Canon.transform.position, gameObject.GetComponentInParent(typeof(PlayerMovementController)).GetComponent<PlayerMovementController>().playerCam.rotation);

      /*instanciatedProjectile.GetComponent<Rigidbody>().velocity =
         transform.TransformDirection(new Vector3(0, 0, speed));*/
   }
   
}
