using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class InputManager : NetworkBehaviour
{
    public KeyCode escapeMenuInput = KeyCode.Escape;

    public GameObject escapeMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
