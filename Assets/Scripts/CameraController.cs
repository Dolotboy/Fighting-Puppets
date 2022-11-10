using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;
    public GameObject UI;
    public override void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
        if (SceneManager.GetActiveScene().name != "Scene_Lobby" && SceneManager.GetActiveScene().name != "Scene_Steamworks")
        {
            UI.SetActive(true);
        }
    }
}
