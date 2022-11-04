using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PlayerMovementController : NetworkBehaviour
{
    public float Speed = 0.1f;
    public GameObject PlayerModel;

    private void Start()
    {
        PlayerModel.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (PlayerModel.activeSelf == false)
            {
                SetPosition();
                PlayerModel.SetActive(true);
            }

            if (hasAuthority)
            {
                Movement();
            }
        }
    }

    public void SetPosition()
    {
        var rnd = new Random();
        transform.position = new Vector3(rnd.Next(-5,5), 1, rnd.Next(-5,5));
    }

    public void Movement()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

        transform.position += moveDirection * Speed;
    }
}
