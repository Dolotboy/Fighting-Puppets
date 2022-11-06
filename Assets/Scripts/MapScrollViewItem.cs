using BrettArnett;
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

    [SerializeField] private string mapName;
    [SerializeField] private string mapScene;

    private GameObject parentObject;

    void Start()
    {
        //parentObject = gameObject.transform.parent.gameObject;
    }
    
    public void ChangeImage(Sprite image)
    {
        childImage.sprite = image;
    }

    public void ChangeText(string text)
    {
        mapName = text;
        childText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void ChangeScene(string scene)
    {
        mapScene = scene;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(mapName + " selected, loading the scene: " + mapScene);
        SteamLobby.instance.selectedMap = mapScene; 
        //parentObject.GetComponent<ScrollViewMapLoader>().selectedMap = mapScene;
    }
}
