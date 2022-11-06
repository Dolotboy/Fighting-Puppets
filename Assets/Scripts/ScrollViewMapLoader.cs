using BrettArnett;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map 
{
    public string name;
    public string scene;
    public Sprite sprite;
    public int starNbr;

    public Map(string name, string scene, Sprite sprite, int starNbr)
    {
        this.name = name;
        this.scene = scene;
        this.sprite = sprite;
        this.starNbr = starNbr;
    }
} 

public class ScrollViewMapLoader : MonoBehaviour
{
    public List<Map> coopMaps = new List<Map>();

    public List<Map> versusMaps = new List<Map>();

    public Dropdown gamemodeDropdown;

    [SerializeField] private Transform scrollViewContent;

    [SerializeField] private GameObject prefab;

    public string selectedMap;

    public GameObject previouslyClickedItem;

    void Start()
    {
        //Add listener for when the value of the Dropdown changes, to take action
        gamemodeDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(gamemodeDropdown);
        });

        coopMaps.Add(new Map("Arena", "CoopArena", Resources.Load<Sprite>("MapImages/Arena"), 1));
        coopMaps.Add(new Map("Mystic Forest", "CoopMysticForest", Resources.Load<Sprite>("MapImages/MysticForest"), 1));


        versusMaps.Add(new Map("Arena", "VersusArena", Resources.Load<Sprite>("MapImages/Arena"), 3));
        versusMaps.Add(new Map("SpaceShip", "VersusSpaceShip", Resources.Load<Sprite>("MapImages/SpaceShip"), 3));

        loadCoopMaps();
    }

    void loadCoopMaps()
    {
        destroyChild();

        foreach (Map map in coopMaps)
        {
            GameObject newMap = Instantiate(prefab, scrollViewContent);
            if(newMap.TryGetComponent<MapScrollViewItem>(out MapScrollViewItem item))
            {
                if(map.sprite == null)
                {
                    map.sprite = Resources.Load<Sprite>("MapImages/MapImageNotFound");
                }
                item.ChangeImage(map.sprite);
                item.ChangeText(map.name);
                item.ChangeScene(map.scene);
            }
        }
    }

    void loadVersusMaps()
    {
        destroyChild();

        foreach (Map map in versusMaps)
        {
            GameObject newMap = Instantiate(prefab, scrollViewContent);
            if (newMap.TryGetComponent<MapScrollViewItem>(out MapScrollViewItem item))
            {
                if (map.sprite == null)
                {
                    map.sprite = Resources.Load<Sprite>("MapImages/MapImageNotFound");
                }
                item.ChangeImage(map.sprite);
                item.ChangeText(map.name);
                item.ChangeScene(map.scene);
            }
        }
    }

    void DropdownValueChanged(Dropdown change)
    {
        if(change.value == 0) // COOP
        {
            SteamLobby.instance.gamemode = "COOP";
            loadCoopMaps();
        }
        else
        {
            SteamLobby.instance.gamemode = "Versus";
            loadVersusMaps();
        }
    }

    private void destroyChild()
    {
        foreach (Transform child in transform) // For each child in current gameObject
        {
            GameObject.Destroy(child.gameObject); // Destroy the child
        }
    }
}
