using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;
    
    public override void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
    }
}
