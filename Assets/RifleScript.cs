using System;
using System.Collections;
using System.Collections.Generic;
using BrettArnett;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RifleScript : MonoBehaviour
{
   [SerializeField] private GameObject Canon;
   [SerializeField] private GameObject Projectile;
   [SerializeField] private float speed;
   
   public void Fire()
   {
      if (!gameObject.GetComponentInParent(typeof(GamePlayer))) { return; }
      
      
      //GameObject instanciatedProjectile = Instantiate(Projectile, Canon.transform.position, gameObject.GetComponentInParent(typeof(PlayerMovementController)).GetComponent<PlayerMovementController>().playerCam.rotation);

      /*instanciatedProjectile.GetComponent<Rigidbody>().velocity =
         transform.TransformDirection(new Vector3(0, 0, speed));*/
   }
   
}
