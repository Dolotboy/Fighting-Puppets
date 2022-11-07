using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderController : MonoBehaviour
{

   [SerializeField] private GameObject GamePlayer;
   private void OnEnable()
   {
      GamePlayer.GetComponent<AnimationStateController>().EnableWeapons();
   }

   private void OnDisable()
   {
      GamePlayer.GetComponent<AnimationStateController>().DisableWeapons();
   }
}
