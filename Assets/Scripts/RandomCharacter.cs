using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomCharacter : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        materials.Add(Resources.Load<Material>("CharacterColor/Yellow"));
        materials.Add(Resources.Load<Material>("CharacterColor/Blue"));
        materials.Add(Resources.Load<Material>("CharacterColor/Pink"));
        materials.Add(Resources.Load<Material>("CharacterColor/Red"));
        materials.Add(Resources.Load<Material>("CharacterColor/Purple"));
        materials.Add(Resources.Load<Material>("CharacterColor/Green"));
        materials.Add(Resources.Load<Material>("CharacterColor/Orange"));

        if (materials.Count >= 1)
        {
            rend.sharedMaterial = materials[rnd.Next(0, materials.Count)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
