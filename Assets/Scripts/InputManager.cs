using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class InputManager : NetworkBehaviour
{
    public KeyCode escapeMenuInput = KeyCode.Escape;
    public KeyCode interactInput = KeyCode.E;
    public KeyCode forwardInput = KeyCode.W;
    public KeyCode backwardInput = KeyCode.S;
    public KeyCode leftInput = KeyCode.A;
    public KeyCode rightInput = KeyCode.D;
    public KeyCode dropWeaponInput = KeyCode.Q;
    public KeyCode jumpInput = KeyCode.Space;
    public KeyCode switchCamInput = KeyCode.C;

    public Camera cam;

    public GameObject escapeMenu;
    public GameObject weaponHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Escape Menu code is here because it is toggle disable by default, so it need an object to support it for itself
        if(!hasAuthority) { return; }
        if(Input.GetKeyDown(escapeMenuInput) && SceneManager.GetActiveScene().name != "Scene_Lobby" && SceneManager.GetActiveScene().name != "Scene_Steamworks")
        {
            Cursor.visible = !Cursor.visible;

            if (escapeMenu.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                escapeMenu.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                escapeMenu.SetActive(true);
            }
        }
    }
}
