using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image childImage;

    [SerializeField] private GameObject childText;
    
    public void ChangeImage(Sprite image)
    {
        childImage.sprite = image;
    }

    public void ChangeText(string text)
    {
        childText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Image clicked");
    }
}
