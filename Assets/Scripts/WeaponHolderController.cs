using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderController : MonoBehaviour
{
   private void OnEnable()
   {
      if (transform.childCount > 0)
      {
         Debug.Log("Yes");
         transform.GetComponentInChildren<Transform>().GetComponentInChildren<Collider>().enabled = true;
      }
   }

   private void OnDisable()
   {
      if (transform.childCount > 0)
      {
         Debug.Log("No");
         transform.GetComponentInChildren<Transform>().GetComponentInChildren<Collider>().enabled = false;
      }
   }
}
