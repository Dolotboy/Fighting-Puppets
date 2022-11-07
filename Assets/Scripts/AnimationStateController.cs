using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class AnimationStateController : NetworkBehaviour
{
    [SerializeField]private Animator anim;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsPunching = Animator.StringToHash("isPunching");
    private static readonly int IsTwerking = Animator.StringToHash("isTwerking");
    private static readonly int IsSwinging = Animator.StringToHash("isSwinging");

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) { return;}
        CheckInputs();
    }

    private void CheckInputs()
    {
        
        CheckEmotes();
        //Walking Animations
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool hit = Input.GetMouseButton(0);
        bool swing = Input.GetMouseButton(1);
        
        bool isWalking = anim.GetBool(IsWalking);
        
        if (!isWalking && (y != 0 || x != 0)) {anim.SetBool(IsWalking,true);}
        if (isWalking && y == 0 && x == 0) {anim.SetBool(IsWalking,false);}
        
        if(hit){anim.SetBool(IsPunching,true);}
        else{anim.SetBool(IsPunching,false);}
        
        if(swing){anim.SetBool(IsSwinging,true);}
        else{anim.SetBool(IsSwinging,false);}


    }

    private void CheckEmotes()
    {
        bool emote9 = Input.GetKey("9");
        bool isTwerking = anim.GetBool(IsTwerking);
        
        if (!isTwerking && emote9) {anim.SetBool(IsTwerking,true);}
        if (isTwerking) {anim.SetBool(IsTwerking,false);}
    }

    public void EnableWeapons(Transform holder)
    {
        if(!hasAuthority) {return;}
        
        if (transform.childCount > 0)
        {
            Debug.Log("Yes");
            holder.GetComponentInChildren<Transform>().GetComponentInChildren<Collider>().enabled = true;
        }
    }

    public void DisableWeapons(Transform holder)
    {
        if(!hasAuthority) {return;}
        if (transform.childCount > 0)
        {
            Debug.Log("No");
            holder.GetComponentInChildren<Transform>().GetComponentInChildren<Collider>().enabled = false;
        }
    }
}
