﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Steamworks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BrettArnett
{
    public class GamePlayer : NetworkBehaviour
    {
        [Header("GamePlayer Info")]
        [SyncVar(hook = nameof(HandlePlayerNameUpdate))] public string playerName;
        [SyncVar] public int ConnectionId;
        [SyncVar] public int playerNumber;

        [Header("Game Info")]
        [SyncVar] public bool IsGameLeader = false;
        [SyncVar] public ulong playerSteamId;

        [Header("Player Tag Text")] [SerializeField]
        private TMP_Text playerTag;

        private MyNetworkManager game;
        private MyNetworkManager Game
        {
            get
            {
                if (game != null)
                {
                    return game;
                }
                return game = MyNetworkManager.singleton as MyNetworkManager;
            }
        }
        
        

        public override void OnStartAuthority()
        {
            CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
            gameObject.name = "LocalGamePlayer";
            LobbyManager.instance.FindLocalGamePlayer();
            LobbyManager.instance.UpdateLobbyName();
        }

        [Command]
        private void CmdSetPlayerName(string playerName)
        {
            Debug.Log("CmdSetPlayerName: Setting player name to: " + playerName);
            this.HandlePlayerNameUpdate(this.playerName, playerName);
        }

        public override void OnStartClient()
        {
            Game.GamePlayers.Add(this);
            LobbyManager.instance.UpdateLobbyName();
            LobbyManager.instance.UpdateUI();
        }

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void HandlePlayerNameUpdate(string oldValue, string newValue)
        {
            Debug.Log("Player name has been updated for: " + oldValue + " to new value: " + newValue);
            if (isServer)
                this.playerName = newValue;
            if (isClient)
            {
                LobbyManager.instance.UpdateUI();
            }
            
            
            playerTag.SetText(playerName);
        }

        public void QuitLobby()
        {
            if (hasAuthority)
            {
                if (IsGameLeader)
                {
                    Game.StopHost();
                }
                else
                {
                    Game.StopClient();
                }
            }
        }

        private void OnDestroy()
        {
            if (hasAuthority)
            {
                LobbyManager.instance.DestroyPlayerListItems();
                SteamMatchmaking.LeaveLobby((CSteamID)LobbyManager.instance.currentLobbyId);
            }
            Debug.Log("LobbyPlayer destroyed. Returning to main menu.");
        }

        public override void OnStopClient()
        {
            Debug.Log(playerName + " is quiting the game.");
            Game.GamePlayers.Remove(this);
            Debug.Log("Removed player from the GamePlayer list: " + this.playerName);
            LobbyManager.instance.UpdateUI();
        }


        public void CanStartGame(string SceneName)
        {
            if (hasAuthority)
            {
                CmdCanStartGame(SceneName);
            }
        }
        
        [Command]
        public void CmdCanStartGame(string SceneName)
        {
            Game.StartGame(SceneName);
        }
    }
}