using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private double damageModifier;

    public string WeaponType = "1Handed";
    
    public double GetDamageModifier()
    {
        return damageModifier;
    }

    public void SendDamageToUI(string message)
    { 
        GameObject.FindWithTag("UIMessage").GetComponent<Text>().text = message;
        GameObject.FindWithTag("UIMessage").transform.parent.GetComponent<Animator>().Play("PointsUI");
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Hitbox_Head":
            {
                SendDamageToUI($"Head +{10 * damageModifier}");
                Debug.Log($"Head done to Hips {10 * damageModifier}");
            } break;
            case "Hitbox_Torso":
            {
                SendDamageToUI($"Torso +{7 * damageModifier}");
                Debug.Log($"Torso done to Hips {7 * damageModifier}");

            } break;
            case "Hitbox_Hips":
            {
                SendDamageToUI($"Hips +{4 * damageModifier}");
                Debug.Log($"Hit done to Hips {4 * damageModifier}");
            } break;
            case "Hitbox_LeftLeg":
            {
                SendDamageToUI($"Left Leg +{2 * damageModifier}");
                Debug.Log($"Hit done to Left Leg {2 * damageModifier}");
            } break;
            case "Hitbox_RightLeg":
            {
                SendDamageToUI($"Right Leg +{2 * damageModifier}");
                Debug.Log($"Hit done to Right Leg {2 * damageModifier}");
            } break;
            case "Hitbox_LeftArm":
            {
                SendDamageToUI($"Left Arm +{1 * damageModifier}");
                Debug.Log($"Hit done to Left Arm {1 * damageModifier}");
            } break;
            case "Hitbox_RightArm":
            {
                SendDamageToUI($"Right Arm +{1 * damageModifier}");
                Debug.Log($"Hit done to Right Arm {1 * damageModifier}");
            } break;
            case "Hitbox_LeftForeArm":
            {
                SendDamageToUI($"Left Forearm +{1 * damageModifier}");
                Debug.Log($"Hit done to Left Forearm {1 * damageModifier}");
            } break;
            case "Hitbox_RightForeArm":
            {
                SendDamageToUI($"Right Forearm +{1 * damageModifier}");
                Debug.Log($"Hit done to Right Forearm {1 * damageModifier}");
            } break;
        }
    }
}
