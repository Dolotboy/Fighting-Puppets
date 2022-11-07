using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrabbingScript : MonoBehaviour
{
    public Camera cam;

    public GameObject weaponHolder;
    
    public KeyCode interactInput = KeyCode.E;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var camTrasform = cam.transform;
        Ray ray = new Ray(camTrasform.position, camTrasform.forward);
 
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.CompareTag("DroppedWeapon") && Input.GetKeyDown(interactInput))
            {
               
                GameObject weapon = Instantiate(hit.transform.GetComponent<DroppedWeaponScript>().GetPrefab(), weaponHolder.transform, true);

                weapon.transform.localPosition = new Vector3(0, 0, 0);
                weapon.transform.localScale = new Vector3(1f, 1f, 1f);
                weapon.transform.localRotation =
                    hit.transform.GetComponent<DroppedWeaponScript>().GetPrefab().transform.rotation;
                
                Destroy(hit.transform.gameObject);
                
            }
        }
    }
}
