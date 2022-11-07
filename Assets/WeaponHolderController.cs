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
         transform.GetComponentInChildren<Transform>().GetComponentInChildren<Collider>().enabled = true;
      }
   }
}
