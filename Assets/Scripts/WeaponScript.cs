using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private double damageModifier;

    public string WeaponType = "1Handed";
    
    public double GetDamageModifier()
    {
        return damageModifier;
    }

    public void GetDamageDone(string tag)
    {
        switch (tag)
        {
            case "Hitbox_Head":
            {
                Debug.Log($"Hit done to Head {10 * damageModifier}");
            } break;
            case "Hitbox_Torso":
            {
                Debug.Log($"Hit done to Torso {7 * damageModifier}"); 

            } break;
            case "Hitbox_Hips":
            {
                Debug.Log($"Hit done to Hips {4 * damageModifier}");
            } break;
            case "Hitbox_LeftLeg":
            {
                Debug.Log($"Hit done to Left Leg {2 * damageModifier}");
            } break;
            case "Hitbox_RightLeg":
            {
                Debug.Log($"Hit done to Right Leg {2 * damageModifier}");
            } break;
            case "Hitbox_LeftArm":
            {
                Debug.Log($"Hit done to Left Arm {1 * damageModifier}");
            } break;
            case "Hitbox_RightArm":
            {
                Debug.Log($"Hit done to Right Arm {1 * damageModifier}");
            } break;
            case "Hitbox_LeftForeArm":
            {
                Debug.Log($"Hit done to Left Forearm {1 * damageModifier}");
            } break;
            case "Hitbox_RightForeArm":
            {
                Debug.Log($"Hit done to Right Forearm {1 * damageModifier}");
            } break;
        }
    }
}
