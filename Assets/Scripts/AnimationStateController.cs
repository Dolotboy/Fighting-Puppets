using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator anim;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool isWalking = anim.GetBool(IsWalking);
        
        if (!isWalking && forwardPressed) {anim.SetBool(IsWalking,true);}
        
        if (isWalking && !forwardPressed) {anim.SetBool(IsWalking,false);}
    }
}
