using BrettArnett;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardMenu : MonoBehaviour
{
    public Text scoreboardTitle;

    public List<PlayerListItem> playerList = new List<PlayerListItem>();

    [SerializeField] private GameObject ContentPanel;
    [SerializeField] private GameObject PlayerListItemPrefab;

    //public List<PlayerListItem> playerList = new List<PlayerListItem>();

    // Start is called before the first frame update
    void OnEnable()
    {
        scoreboardTitle.text = LobbyManager.instance.lobbyName;
        playerList = LobbyManager.instance.PlayerListItems;
        PopulateScoreboardMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PopulateScoreboardMenu()
    {
        Debug.Log("NOMBRE DE JOUEUR: " + playerList.Count);

        /*for (int i = 0; i < playerList.Count; i++)
        {
            Debug.Log("+1 Joueur trouvé: " + playerList[i].playerName);

            GameObject newPlayerListItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem newPlayerListItemScript = newPlayerListItem.GetComponent<PlayerListItem>();

            newPlayerListItemScript.playerName = playerList[i].playerName;
            newPlayerListItemScript.ConnectionId = playerList[i].ConnectionId;
            newPlayerListItemScript.playerSteamId = playerList[i].playerSteamId;
            newPlayerListItemScript.SetPlayerListItemValues();

            newPlayerListItem.transform.SetParent(ContentPanel.transform);
            newPlayerListItem.transform.localScale = Vector3.one;
        }*/

        foreach(PlayerListItem player in playerList)
        {
            Debug.Log("+1 Joueur trouvé: " + player.playerName);

            GameObject newPlayerListItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem newPlayerListItemScript = newPlayerListItem.GetComponent<PlayerListItem>();

            newPlayerListItemScript.playerName = player.playerName;
            newPlayerListItemScript.ConnectionId = player.ConnectionId;
            newPlayerListItemScript.playerSteamId = player.playerSteamId;
            newPlayerListItemScript.SetPlayerListItemValues();

            newPlayerListItem.transform.SetParent(ContentPanel.transform);
            newPlayerListItem.transform.localScale = Vector3.one;
        }
    }
}
