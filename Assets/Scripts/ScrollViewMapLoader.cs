using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Transform scrollViewContent;

    [SerializeField] private GameObject prefab;

    void Start()
    {
        coopMaps.Add(new Map("Arena", "CoopArena", Resources.Load<Sprite>("MapImages/Arena"), 1));
        coopMaps.Add(new Map("Mystic Forest", "MysticForest", Resources.Load<Sprite>("MapImages/MysticForest"), 1));

        loadCoopMaps();


        versusMaps.Add(new Map("Arena", "VersusArena", Resources.Load<Sprite>("MapImages/Arena"), 3));
    }

    void loadCoopMaps()
    {
        foreach(Map map in coopMaps)
        {
            GameObject newMap = Instantiate(prefab, scrollViewContent);
            if(newMap.TryGetComponent<MapScrollViewItem>(out MapScrollViewItem item))
            {
                item.ChangeImage(map.sprite);
                item.ChangeText(map.name);
            }
        }
    }

    void loadVersusMaps()
    {

    }
}
