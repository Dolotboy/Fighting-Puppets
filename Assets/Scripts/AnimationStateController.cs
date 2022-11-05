using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator anim;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsPunching = Animator.StringToHash("isPunching");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        //Walking Animations
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool hit = Input.GetMouseButton(0);
        bool isWalking = anim.GetBool(IsWalking);
        
        if (!isWalking && (y != 0 || x != 0)) {anim.SetBool(IsWalking,true);}
        if (isWalking && y == 0 && x == 0) {anim.SetBool(IsWalking,false);}
        
        if(hit){anim.SetBool(IsPunching,true);}
        else{anim.SetBool(IsPunching,false);}
    } 
}
