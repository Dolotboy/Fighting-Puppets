using BrettArnett;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        Debug.Log("HJFB WGNVHVF");
        LobbyManager.instance.PlayerQuitLobby();
    }
}
