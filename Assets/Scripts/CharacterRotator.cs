using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;

    public GameObject character;

    private float rotation = 0.1F;

    public void RotateLeft()
    {
        character.transform.localRotation = Quaternion.Euler(character.transform.rotation.eulerAngles.x, character.transform.rotation.eulerAngles.y + (rotation * speed), character.transform.rotation.eulerAngles.z);
    }

    public void RotateRight()
    {
        character.transform.localRotation = Quaternion.Euler(character.transform.rotation.eulerAngles.x, character.transform.rotation.eulerAngles.y - (rotation * speed), character.transform.rotation.eulerAngles.z);
    }
}
