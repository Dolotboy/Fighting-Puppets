using System;
using System.Collections;
using System.Collections.Generic;
using BrettArnett;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private double damageModifier;

    public string WeaponType = "1Handed";
    private static readonly int IsDamaged = Animator.StringToHash("isDamaged");

    private GameObject currentPlayer;

    private void OnTransformParentChanged()
    {
        if (gameObject.GetComponentInParent(typeof(GamePlayer)))
        {
            currentPlayer = gameObject.GetComponentInParent(typeof(GamePlayer)).gameObject;
        }
        else
        {
            currentPlayer = null;
        }
    }

    public double GetDamageModifier()
    {
        return damageModifier;
    }

    public void SendDamageToUI(string message)
    {
        if (!currentPlayer) return;
        currentPlayer.GetComponent<Health>().UIMessage.text = message + "\n" + currentPlayer.GetComponent<Health>().UIMessage.text;
        Invoke(nameof(ResetDamageUI),2f);
        
    }

    public void ResetDamageUI()
    {
        currentPlayer.GetComponent<Health>().UIMessage.text = "";
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Hitbox_Head":
            {
                SendDamageToUI($"+{10 * damageModifier} Head ");
                Debug.Log($"Head done to Hips {10 * damageModifier}");
            } break;
            case "Hitbox_Torso":
            {
                SendDamageToUI($"+{7 * damageModifier} Torso ");
                Debug.Log($"Torso done to Hips {7 * damageModifier}");

            } break;
            case "Hitbox_Hips":
            {
                SendDamageToUI($"+{4 * damageModifier} Hips ");
                Debug.Log($"Hit done to Hips {4 * damageModifier}");
            } break;
            case "Hitbox_LeftLeg":
            {
                SendDamageToUI($"+{2 * damageModifier} Left Leg ");
                Debug.Log($"Hit done to Left Leg {2 * damageModifier}");
            } break;
            case "Hitbox_RightLeg":
            {
                SendDamageToUI($"+{2 * damageModifier} Right Leg ");
                Debug.Log($"Hit done to Right Leg {2 * damageModifier}");
            } break;
            case "Hitbox_LeftArm":
            {
                SendDamageToUI($"+{1 * damageModifier} Left Arm ");
                Debug.Log($"Hit done to Left Arm {1 * damageModifier}");
            } break;
            case "Hitbox_RightArm":
            {
                SendDamageToUI($"+{1 * damageModifier} Right Arm ");
                Debug.Log($"Hit done to Right Arm {1 * damageModifier}");
            } break;
            case "Hitbox_LeftForeArm":
            {
                SendDamageToUI($"+{1 * damageModifier} Left Forearm ");
                Debug.Log($"Hit done to Left Forearm {1 * damageModifier}");
            } break;
            case "Hitbox_RightForeArm":
            {
                SendDamageToUI($"+{1 * damageModifier} Right Forearm ");
                Debug.Log($"Hit done to Right Forearm {1 * damageModifier}");
            } break;
        }
    }
}
