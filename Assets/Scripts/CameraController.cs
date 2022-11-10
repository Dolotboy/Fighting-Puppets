using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;
    public GameObject PuppetUI;
    public override void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
        PuppetUI.SetActive(true);
    }
}
