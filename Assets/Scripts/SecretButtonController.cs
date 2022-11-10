using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretButtonController : MonoBehaviour
{
    [SerializeField] private Animator activatedObject;
    private static readonly int IsActivated = Animator.StringToHash("isActivated");

    public void Activate()
    {
        activatedObject.SetBool(IsActivated,true);
        Invoke(nameof(Deactivate), 2.0f);
    }

    public void Deactivate()
    {
        activatedObject.SetBool(IsActivated,false);
    }
    
}
