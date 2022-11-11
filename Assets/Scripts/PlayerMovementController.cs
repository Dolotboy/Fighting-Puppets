using System;
using System.Collections;
using System.Collections.Generic;
using BrettArnett;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Random = System.Random;
using TMPro;

public class PlayerMovementController : NetworkBehaviour
{
    //Animator
    public Animator anim;
    
    public float Speed = 0.1f;
    public GameObject PlayerModel;

    public GameObject WeaponHolder;

    public GameObject PuppetUI;
    
    public Transform playerCam;
    //public Transform orientation;

    public Camera fpsCam;
    public Camera tpsCam;

    //Other
    private Rigidbody rb;

    //Rotation and look
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    //Movement
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public bool grounded;
    public LayerMask whatIsGround;

    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;

    //Input
    float x, y;
    bool jumping, sprinting, crouching;

    //Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    private GameObject[] spawnPoints;

    private bool hasWeaponEquiped;
    
    


    //private MyNetworkManager networkManager;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        PlayerModel.SetActive(false);
        hasWeaponEquiped = false;
        //networkManager = GetComponent<MyNetworkManager>();
    }

    private void FixedUpdate()
    { 
        if (SceneManager.GetActiveScene().name != "Scene_Lobby" && SceneManager.GetActiveScene().name != "Scene_Steamworks")
        {
            if (hasAuthority)
            {
                Movement();
            }
        }
    }

    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Scene_Lobby" && SceneManager.GetActiveScene().name != "Scene_Steamworks")
        {
            if (PlayerModel.activeSelf == false)
            {
                spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
                SetSpawningPosition(spawnPoints);
                PlayerModel.SetActive(true);
                if(hasAuthority)
                {
                    PuppetUI.SetActive(true);
                }
                playerScale = transform.localScale;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (hasAuthority)
            {
                MyInput();
                Look();
                Interact();
                Drop(transform);
            }
        }
    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftControl);
        

        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
            anim.SetBool(IsRolling,true);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
            anim.SetBool(IsRolling,false);
        }
    }

    private void StartCrouch()
    {
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(transform.forward * slideForce);
            }
        }
    }
    
    
    private void StopCrouch()
    {
    }

    public void SetSpawningPosition(GameObject[] spawnPoints)
    {
        if (spawnPoints.Length == 0)
        {
            SetPosition();
        }
        else
        {
            var rnd = new Random();
            int index = rnd.Next(0, spawnPoints.Length);

            transform.position = spawnPoints[index].transform.position;
        }
    }
    
    public void SetPosition()
    {
        var rnd = new Random();
        transform.position = new Vector3(0.323f, 1.119f,0.306f);
    }

    private void Interact()
    {
        RaycastHit hit;
        var camTrasform = playerCam.transform;
        Ray ray = new Ray(camTrasform.position, camTrasform.forward);
 
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("DroppedWeapon") && Input.GetKeyDown(KeyCode.E) && !hasWeaponEquiped)
            {

                CmdSpawn(hit.transform);
                //networkManager.Destroy(hit.transform.gameObject);

            }
            if (hit.transform.childCount > 0)
            {
                //Debug.Log(hit.transform);
                //Debug.Log(hit.transform.childCount);
                //Debug.Log(hit.transform.GetChild(0).gameObject.tag); 
                if (hit.transform.GetChild(0).gameObject.tag == "DroppedArmor" && hit.transform.GetComponent<GoodsSpawner>().haveObjectInIt && Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<Health>().IncreaseArmorPoint(hit.transform.GetChild(0).GetComponent<Armor>().ArmorPoint);
                    hit.transform.GetComponent<GoodsSpawner>().TakeObjectInIt();
                }
            }

            if (hit.transform.CompareTag("SecretActivator") && Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.GetComponent<SecretButtonController>().Activate();
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            SwitchPlayerCam();
        }
    }

    private void SwitchPlayerCam()
    {
        if(fpsCam.gameObject.activeInHierarchy)
        {
            tpsCam.gameObject.SetActive(true);
            fpsCam.gameObject.SetActive(false);
        }
        else if(tpsCam.gameObject.activeInHierarchy)
        {
            fpsCam.gameObject.SetActive(true);
            tpsCam.gameObject.SetActive(false);
        }
    }
    
    [Command(requiresAuthority = false)]
    private void CmdSpawn(Transform hit)
    {
        RPCSpawn(hit);
    }

    [ClientRpc]
    private void RPCSpawn(Transform hit)
    {
        GameObject weapon = hit.transform.GetChild(0).gameObject;
        weapon.transform.parent = WeaponHolder.transform;
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localScale = new Vector3(1f, 1f, 1f);
        weapon.transform.localRotation =
            hit.GetComponent<DroppedWeaponScript>().GetPrefab().transform.rotation;

        hit.tag = "DroppedWeapon_Empty";
        CheckWeaponType(weapon);
        
        
        
        //NetworkServer.Spawn(weapon);

        //Destroy(hit.gameObject);
        //NetworkServer.Destroy(hit.gameObject);

        hasWeaponEquiped = true;
    }

    private void CheckWeaponType(GameObject weapon)
    {
        switch (weapon.GetComponentInChildren<WeaponScript>().WeaponType)
        {
            case "1Handed":
            {
                anim.SetBool(IsOneHanded,true);
                anim.SetBool(IsTwoHanded,false);
            } break;
            case "2Handed":
            {
                anim.SetBool(IsOneHanded,false);
                anim.SetBool(IsTwoHanded,true);
            } break;
        }
        
    }

    private void Drop(Transform _transform)
    {
        if (Input.GetKeyDown(KeyCode.Q) && hasWeaponEquiped)
        {
            CmdDropWeapon(_transform);
        }
    }

    public void DropWeaponOnDeath(Transform _transform)
    {
        CmdDropWeapon(_transform);
    }

    [Command]
    private void CmdDropWeapon(Transform _transform)
    {
        if(!hasWeaponEquiped)
        {
            return;
        }
        RPCDropWeapon(_transform);
    }

    [ClientRpc]
    private void RPCDropWeapon(Transform _transform)
    {
        GameObject weapon = GameObject.FindWithTag("DroppedWeapon_Empty");

        if (weapon.transform.childCount == 0)
        {
            Debug.Log("Weapon position:" + transform.position);
            //weapon.transform.parent.transform.position = transform.position;
            //weapon.transform.position = transform.localPosition;
            weapon.transform.parent.transform.position = _transform.position;
            weapon.transform.position = _transform.localPosition;
            WeaponHolder.transform.GetChild(0).parent = weapon.transform;
            hasWeaponEquiped = false;
            weapon.tag = "DroppedWeapon";
        }

        ResetWeaponType();

    }

    private void ResetWeaponType()
    {
        anim.SetBool(IsOneHanded,true);
        anim.SetBool(IsTwoHanded,false);
    }
    
    private void Movement()
    {
        //Extra gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        //If holding jump && ready to jump, then jump
        if (readyToJump && jumping) Jump();

        //Set max speed
        float maxSpeed = this.maxSpeed;

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // Movement while sliding
        if (grounded && crouching) multiplierV = 0f;

        
        //Apply forces to move player
        rb.AddForce(transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }
    
    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            //Add jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private float desiredX;
    public void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0 , desiredX, 0);
    }
    
    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        //Slow down sliding
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        //Counter movement
        if (System.Math.Abs(mag.x) > threshold && System.Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (System.Math.Abs(mag.y) > threshold && System.Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    private static readonly int IsRolling = Animator.StringToHash("isRolling");
    private static readonly int IsOneHanded = Animator.StringToHash("isOneHanded");
    private static readonly int IsTwoHanded = Animator.StringToHash("isTwoHanded");

    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other)
    {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }
}
