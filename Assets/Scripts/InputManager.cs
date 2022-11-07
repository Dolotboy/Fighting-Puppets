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
        RaycastHit hit;
        var camTrasform = cam.transform;
        Ray ray = new Ray(camTrasform.position, camTrasform.forward);
 
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.CompareTag("DroppedWeapon") && Input.GetKeyDown(interactInput))
            {
                if (isClient)
                {
                    if(!hasAuthority) { return; }
                    EquipWeapon(hit.transform);
                }
            }
        }
        
        
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

    public void EquipWeapon(Transform hit)
    {
        CmdEquipWeapon(hit);
    }

    private void CmdEquipWeapon(Transform hit)
    {
        GameObject weapon = Instantiate(hit.transform.GetComponent<DroppedWeaponScript>().GetPrefab(), weaponHolder.transform, true);

        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localScale = new Vector3(1f, 1f, 1f);
        weapon.transform.localRotation =
            hit.transform.GetComponent<DroppedWeaponScript>().GetPrefab().transform.rotation;
                
        Destroy(hit.transform.gameObject);
    }
}
